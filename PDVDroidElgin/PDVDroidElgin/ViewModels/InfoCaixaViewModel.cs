using PDVDroidElgin.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;

namespace PDVDroidElgin.ViewModels
{
    public class InfoCaixaViewModel
    {

        public InfoCaixaViewModel(Usuario operador, Caixa caixa)
        {
            if (caixa != null)
                if (caixa.DHFechamento == null)
                    CaixaAberto = true;

            Operador = $"Operador: {operador.Nome}";
            Terminal = $"Terminal: {DeviceInfo.Name}";
            DataAbertura = $"Data Abertura: ";
            ValorAbertura = $"Valor Abertura: R$ 0,00";
            ValorFechamento = $"Valor Fechamento: R$ 0,00";
            DataFechamento = $"Data Fechamento: R$ 0,00";

            if (caixa != null)
            {
                DataAbertura = $"Data Abertura: {caixa.DHAbertura.ToString("dd/MM/yyyy HH:mm")}";
                ValorAbertura = $"Valor Abertura: R$ {caixa.ValorAbertura:N2}";
                ValorFechamento = $"Valor Fechamento: R$ {caixa.ValorFechamento:N2}";
                DataFechamento = $"Data Fechamento: {(caixa.DHFechamento == null ? string.Empty : caixa.DHFechamento.Value.ToString("dd/MM/yyyy HH:mm"))}";
            }
        }

        public bool CaixaAberto { get; private set; }
        public string Operador { get; private set; }
        public string Terminal { get; private set; }

        public string DataAbertura { get; private set; }
        public string ValorAbertura { get; private set; }
        public string DataFechamento { get; private set; }
        public string ValorFechamento { get; private set; }
    }
}
