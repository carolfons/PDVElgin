using PDVDroidElgin.Contracts;
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
    public partial class ConsultaVendas : ContentPage
    {
        public ConsultaVendas()
        {
            InitializeComponent();

        }

        private async void ListarVendas()
        {
            IControladorPDV controlador = DependencyService.Get<IControladorPDV>();
            IReadOnlyCollection<VendaViewModel> vendas = await Task.Run(() => controlador.ObterVendas());
            listVendas.ItemsSource = vendas;
            decimal totalEfetivado = vendas.Where(v => v.Efetivada).Sum(v => v.TotalVendaNumber);
            lbTotal.Text = $"R$ {totalEfetivado:N2}";
        }

        private void ContentPage_Appearing(object sender, EventArgs e)
        {
            ListarVendas();
        }

        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MainPage());
        }

        private void listVendas_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            VendaViewModel item = (VendaViewModel)listVendas.SelectedItem;
            if (item == null) return;
            if (item.Efetivada)
            {
                IControladorPDV controlador = DependencyService.Get<IControladorPDV>();

                if (rdoImprimirTexto.IsChecked)
                    controlador.ImprimirEmTexto(item.VendaId);
                if (rdoImprimirXml.IsChecked)
                    controlador.ImprimirXML(item.VendaId);
            }
            else
                Navigation.PushAsync(new MainPage(item.VendaId));
        }
    }
}