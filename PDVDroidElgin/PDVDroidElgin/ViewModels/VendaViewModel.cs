using PDVDroidElgin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PDVDroidElgin.ViewModels
{
    public class VendaViewModel
    {
        public int VendaId { get; private set; }
        public bool Efetivada { get; private set; }
        public string Status { get; private set; }
        public string Numero { get; private set; }
        public string DataHora { get; private set; }
        public string NomeCliente { get; private set; }
        public string CpfCnpjCliente { get; private set; }
        public string ValorVenda { get; private set; }
        public string ValorPago { get; private set; }

        public decimal TotalVendaNumber { get; private set; }

        public VendaViewModel(Venda venda)
        {
            TotalVendaNumber = venda.vNF;
            VendaId = venda.Id;
            Efetivada = venda.Efetivada;
            Status = (Efetivada ? "Venda Concluída" : "Venda em aberto");
            Numero = venda.Id.ToString().PadLeft(5, '0');
            DataHora = venda.DhEmi.ToString("dd/MM/yyyy HH:mm:ss");

            Cliente cliente = venda.Cliente;
            if (cliente != null)
            {
                NomeCliente = cliente.Nome;
                CpfCnpjCliente = $"CPF/CNPJ: {cliente.CpfCnpj}";
            }
            else
            {
                NomeCliente = "Cliente Padrão";
                CpfCnpjCliente = $"CPF/CNPJ:";
            }

            ValorVenda = $"Valor da Venda: R$ {venda.vNF:N2}";

            List<PagamentoVenda> pagamentos = venda.Pagamentos.ToList();
            if (pagamentos.Count > 0)
                ValorPago = $"Valor Pago: R$ {pagamentos[0].ValorPagamento:N2}";
            else 
                ValorPago = "Valor Pago: R$ 0,00";
        }
    }
}
