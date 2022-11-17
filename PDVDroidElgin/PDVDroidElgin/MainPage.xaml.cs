using PDVDroidElgin.Contracts;
using PDVDroidElgin.Data;
using PDVDroidElgin.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PDVDroidElgin
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        private readonly IControladorPDV controladorPdv;

        public MainPage(int vendaIdEmAberto = 0)
        {
            InitializeComponent();

            controladorPdv = DependencyService.Get<IControladorPDV>();
            controladorPdv.SetVendaIdEmAberto(vendaIdEmAberto);

            BuscarProdutos("");
            ListaItensVenda();
        }

        public void LimpaTela()
        {
            listItems.ItemsSource = null;
            gridResumo.BindingContext = new ResumoVendaViewModel();
        }

        private async void ListaItensVenda()
        {
            listItems.ItemsSource = await Task.Run(() => controladorPdv.ObterItensVendaAtual());
            gridResumo.BindingContext = await Task.Run(() => controladorPdv.ObterResumo());
        }

        private async void BuscarProdutos(string busca)
        {
            var itens = await Task.Run(() => controladorPdv.BuscarProdutos(busca));
            listProdutos.ItemsSource = itens;
        }

        private void txPesquisa_TextChanged(object sender, TextChangedEventArgs e)
        {
            BuscarProdutos(txPesquisa.Text);
        }

        private void btFinalizar_Clicked(object sender, EventArgs e)
        {
            if (controladorPdv.ObterResumo().TotalNumber == 0)
                return;

            Pagamento pag = new Pagamento(this, controladorPdv);
            Navigation.ShowPopup(pag);
            //App.Current.MainPage = pag;
        }

        private async void listProdutos_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            ProdutoViewModel item = (ProdutoViewModel)listProdutos.SelectedItem;
            if (item == null) return;

            if (controladorPdv.VendaEmAndamento() == false)
                controladorPdv.AbreCupom("");

            await Task.Run(() => controladorPdv.AdicionaItem(item, quantidade: 1));
            
            ListaItensVenda();
        }
    }
}