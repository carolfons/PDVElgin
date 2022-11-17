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
    public partial class ConsultaFormasPg : ContentPage
    {
        public ConsultaFormasPg()
        {
            InitializeComponent();
        }

        private async void Buscar()
        {
            string busca = txBusca.Text??string.Empty;

            using (PdvContext ctx = new PdvContext())
            {
                List<FormaPagamento> formasPg = await Task.Run(() => ctx.FormasPagamento
                    .Where(fpg => fpg.Descricao.Contains(busca))
                    .ToList());

                listFormasPg.ItemsSource = formasPg;
            }
        }

        private void ContentPage_Appearing(object sender, EventArgs e)
        {
            Buscar();
        }

        private void txBusca_TextChanged(object sender, TextChangedEventArgs e)
        {
            Buscar();
        }
    }
}