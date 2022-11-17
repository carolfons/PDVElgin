using System;
using System.Collections.Generic;
using System.Text;

namespace PDVDroidElgin.Models
{
    public class Venda
    {
        public Venda(Empresa empresa, Usuario usuario, Cliente cliente = null)
        {
            cUF = empresa.cUF;
            Serie = empresa.Serie;
            cNF = empresa.UltimaNF + 1;
            Mod = 65;
            NatOpe = "VENDA NFC-e";
            DhEmi = DateTime.Now;
            DhSaiEnt = DhEmi;
            TpNF = 1;
            IdDest = 2;
            FinNFe = 1;
            IndPres = 1;
            ProcEmi = 0;
            VerProc = "4.00";
            UsuarioId = usuario.Id;

            if (cliente != null) ClienteId = cliente.Id;

            ItensVenda = new List<ItemVenda>();
            Pagamentos = new List<PagamentoVenda>();
        }

        public Venda()
        {
            ItensVenda = new List<ItemVenda>();
            Pagamentos = new List<PagamentoVenda>();
        }



        public void AdicionaItem(Produto produto, int quantidade)
        {
            ItemVenda item = new ItemVenda(
                    this,
                    produto,
                    quantidade);

            ItensVenda.Add(item);
            vNF += item.VTotalProd;
            vProd += item.VTotalProd;
        }

        public void RemoveItem(ItemVenda item)
        {
            ItensVenda.Remove(item);
            vNF -= item.VTotalProd;
            vProd -= item.VTotalProd;
        }

        public void EfetuaPagamento(FormaPagamento formaPg, decimal valorPagamento)
        {
            if (Efetivada) throw new Exception("Esta venda já foi efetivada");

            decimal troco = (valorPagamento > vNF
                ? valorPagamento - vNF
                : 0);

            PagamentoVenda pagamento = new PagamentoVenda(
                    venda: this,
                    formaPagamento: formaPg,
                    valorPagamento: valorPagamento,
                    valorTroco: troco);

            Pagamentos.Add(pagamento);
            Efetivada = true;
        }

        public int Id { get; set; }


        public int? ClienteId { get; private set; }
        public virtual Cliente Cliente { get; private set; }
        public virtual ICollection<ItemVenda> ItensVenda { get; private set; }

        public virtual ICollection<PagamentoVenda> Pagamentos { get; private set; }

        public bool Efetivada { get; private set; }

        public int UsuarioId { get; private set; }
        public virtual Usuario Usuario { get; private set; }

        public int cUF { get; private set; }

        public int cNF { get; private set; }
        public int Mod { get; private set; }
        public int Serie { get; private set; }
        public int NNf { get; private set; }

        public string NatOpe { get; private set; }
        public DateTime DhEmi { get; private set; }
        public DateTime DhSaiEnt { get; private set; }
        public int TpNF { get; private set; }
        public int IdDest { get; private set; }
        public int CMunFG { get; private set; }
        public int TpImp { get; private set; }
        public int TpEmis { get; private set; }
        public int Cdv { get; private set; }
        public int TpAmb { get; private set; }
        public int FinNFe { get; private set; }

        public int IndFinal { get; private set; }
        public int IndPres { get; private set; }
        public int ProcEmi { get; private set; }
        public string VerProc { get; private set; }
        public DateTime? DhCont { get; private set; }
        public string xJust { get; private set; }

        public decimal vBC { get; private set; }
        public decimal vICMS { get; private set; }
        public decimal vBcST { get; private set; }
        public decimal vST { get; private set; }
        public decimal vProd { get; private set; }
        public decimal vFrete { get; private set; }
        public decimal vSeg { get; private set; }
        public decimal vDesc { get; private set; }
        public decimal vII { get; private set; }
        public decimal vIPI { get; private set; }
        public decimal vPIS { get; private set; }
        public decimal vCOFINS { get; private set; }
        public decimal vOutro { get; private set; }
        public decimal vNF { get; private set; }
        public decimal vTotalTrib { get; private set; }
        public decimal vFCP { get; private set; }
        public decimal vFCPSt { get; private set; }

        public string InfoFisco { get; private set; }
        public string InfoComplementar { get; private set; }
        public string Versao { get; private set; }
        public string ChaveNF { get; private set; }
        public string StatusNFe { get; private set; }

    }
}
