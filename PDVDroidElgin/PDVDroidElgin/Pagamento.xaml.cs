
using PDVDroidElgin.Contracts;
using PDVDroidElgin.Data;
using PDVDroidElgin.Hardware;
using PDVDroidElgin.Models;
using PDVDroidElgin.ViewModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PDVDroidElgin
{
    public partial class Pagamento : Popup
    {
        private readonly int vendaId;
        private readonly IControladorPDV controlador;
        private readonly MainPage mainPage;

        public Pagamento(MainPage mp, IControladorPDV controlador)
        {
            InitializeComponent();
            this.vendaId = controlador.GetVendaIdEmAndamento();
            this.controlador = controlador;
            this.mainPage = mp;

            ResumoVendaViewModel resumo = controlador.ObterResumo();
            txTotalVenda.Text = resumo.TotalNumber.ToString("N2");
        }

        private async void btConfirmar_Clicked(object sender, EventArgs e)
        {
            btConfirmar.IsEnabled = false;
            btConfirmar.Text = "Aguarde...";
            bool pagamentoEfetuado = await EfetuaPagamento();
            btConfirmar.IsEnabled = true;
            btConfirmar.Text = "Finalizar";

            if (pagamentoEfetuado)
            {
                mainPage.LimpaTela();
                Dismiss(null);
            }
        }

        private async Task<bool> EfetuaPagamento()
        {
            return await Task<bool>.Run(() =>
            {
                try
                {
                    using (PdvContext ctx = new PdvContext())
                    {
                        FormaPagamento fpgDinheiro = ctx.FormasPagamento.Find(1); //default dinheiro quando criou o banco

                        decimal valorPago = 0;
                        bool numeroEmFormatoCorreto = decimal.TryParse(txValorPagamento.Text, out valorPago);
                        if (numeroEmFormatoCorreto == false)
                            throw new Exception($"O número '{txValorPagamento.Text}' é inválido");

                        int vendaId = controlador.GetVendaIdEmAndamento();
                        controlador.EfetuaPagamento(fpgDinheiro, valorPago);
                        controlador.ImprimirEmTexto(vendaId);
                        return true;
                    }

                }
                catch (Exception ex)
                {
                    DependencyService.Get<IToastFacade>().ShortAlert(ex.Message);
                    return false;
                }
            });
        }

        private void btCancelar_Clicked(object sender, EventArgs e)
        {
            Dismiss(null);
        }

        private void btCorrige_Clicked(object sender, EventArgs e)
        {
            txValorPagamento.Text = "";
        }

        private void txValorPagamento_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (txValorPagamento.Text.Contains(","))
                    txValorPagamento.Text = txValorPagamento.Text.Replace(",", ".");
            }
            catch { }
        }
    }
}