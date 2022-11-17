using PDVDroidElgin.Contracts;
using PDVDroidElgin.Data;
using PDVDroidElgin.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PDVDroidElgin
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ConsultaProdutos : ContentPage
    {
        public ConsultaProdutos()
        {
            InitializeComponent();
        }

        private async void Buscar()
        {
            string busca = txBusca.Text ??"";
            IControladorPDV ct = DependencyService.Get<IControladorPDV>();
            List<ProdutoViewModel> produtos = await Task.Run(() => ct.BuscarProdutos(busca));
            listProdutos.ItemsSource = produtos;
        }

        private void txBusca_TextChanged(object sender, TextChangedEventArgs e)
        {
            Buscar();
        }

        private void ContentPage_Appearing(object sender, EventArgs e)
        {

            Buscar();
        }
    }
}