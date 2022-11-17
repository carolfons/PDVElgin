using PDVDroidElgin.Data;
using PDVDroidElgin.Models;
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
    public partial class ConsultaClientes : ContentPage
    {
        public ConsultaClientes()
        {
            InitializeComponent();
            Buscar();
        }

        private async void Buscar()
        {
            string busca = txBusca.Text??String.Empty;

            List<Cliente> clientes = await Task.Run(() =>
            {
                using(PdvContext ctx = new PdvContext())
                {
                    return ctx.Clientes
                    .Where(c => c.Nome.ToLower().Contains(busca))
                    .ToList();
                }
            });

            listClientes.ItemsSource = clientes;
        }

        private void txBusca_TextChanged(object sender, TextChangedEventArgs e)
        {
            Buscar();
        }
    }
}