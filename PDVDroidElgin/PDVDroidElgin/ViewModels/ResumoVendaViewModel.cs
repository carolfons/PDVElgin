using PDVDroidElgin.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PDVDroidElgin.ViewModels
{
    public class ResumoVendaViewModel
    {
        public ResumoVendaViewModel(
            Venda venda)
        {

            Cliente cliente = venda.Cliente;
            string nome = string.Empty;
            string cpfCnpj = string.Empty;
            string email = string.Empty;

            if (cliente != null)
            {
                nome = cliente.Nome;
                cpfCnpj = cliente.CpfCnpj;
                email = cliente.email;
            }

            Cliente = $"Cliente: {nome}";
            CpfCnpj = $"CPF/CNPJ: {cpfCnpj}";
            Email = $"Email: {email}";
            Vendedor = $"Vendedor: {venda.Usuario.Nome}";
            Desconto = $"Desconto: R$ {venda.vDesc.ToString("N2")}";
            Total = $"Total: R$ {venda.vNF.ToString("N2")}";
            Atendimento = $"Atendimento: N°{venda.Id}";

            TotalNumber = venda.vNF;
        }

        public ResumoVendaViewModel()
        {
            Atendimento = "Atendimento: ";
            Cliente = $"Cliente:";
            CpfCnpj = $"CPF/CNPJ:";
            Email = $"Email:";
            Vendedor = $"Vendedor:";
            Desconto = $"Desconto: R$ 0,00";
            Total = $"Total: R$ 0,00";
        }

        public string Atendimento { get; private set; }
        public string Cliente { get; private set; }
        public string CpfCnpj { get; private set;}
        public string Email { get; private set; }
        public string Vendedor { get; private set; }
        public string Desconto { get; private set; }

        /// <summary>
        /// total string para bind de tela
        /// </summary>
        public string Total { get; private set; }

        /// <summary>
        /// total numerico para tratamento geral
        /// </summary>
        public decimal TotalNumber { get; private set; }
    }
}
