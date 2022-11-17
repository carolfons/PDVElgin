using PDVDroidElgin.Contracts;
using PDVDroidElgin.Models;
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
    public partial class ControleCaixa : ContentPage
    {
        private readonly IControladorPDV pdv;
        public ControleCaixa()
        {
            InitializeComponent();
            pdv = DependencyService.Get<IControladorPDV>();
        }

        private void AtualizaInfo()
        {
            this.BindingContext = pdv.ObterInfoCaixa(
                    operador: Login.UsuarioLogado
                );
        }

        private void ContentPage_Appearing(object sender, EventArgs e)
        {
            AtualizaInfo();
        }

        private void btAbrirFecharCaixa_Clicked(object sender, EventArgs e)
        {
            painelAbrirFechar.IsVisible = true;
        }

        private void btConfirmarAbrirFecharCx_Clicked(object sender, EventArgs e)
        {
            try
            {
                decimal valor = decimal.Parse(txValorAberturaFechamentoCx.Text);
                InfoCaixaViewModel info = (InfoCaixaViewModel)this.BindingContext;
                Usuario usuarioLogado = Login.UsuarioLogado;

                if (info.CaixaAberto)
                    pdv.FecharCaixa(usuarioLogado, valor);
                else
                    pdv.AbrirCaixa(usuarioLogado, valor);


                this.BindingContext = pdv.ObterInfoCaixa(usuarioLogado);
                painelAbrirFechar.IsVisible = false;
            }
            catch(Exception ex)
            {
                IToastFacade toast = DependencyService.Get<IToastFacade>();
                toast.ShortAlert($"Erro: {ex.Message}");
            }
        }
    }
}