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
    public partial class Menu : ContentPage
    {
        public Menu()
        {
            InitializeComponent();
        }

        private void btConsultaProdutos_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ConsultaProdutos());
        }

        private void btConsultaVendas_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ConsultaVendas());
        }

        private void btControleCaixa_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ControleCaixa());
        }

        private void ImageButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new VerEmpresa());
        }

        private void btFormasPag_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ConsultaFormasPg());
        }

        private void btClientes_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ConsultaClientes());
        }
    }
}