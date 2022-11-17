using System;

namespace PDVDroidElgin.Models
{
    public class PagamentoVenda
    {
        public int Id { get; set; }
        public int FormaPagamentoId { get; private set; }
        public virtual FormaPagamento FormaPagamento { get; private set; }


        public int VendaId { get;  set; }
        public virtual Venda Venda { get; set; }

        public decimal ValorPagamento { get; private set; }
        public decimal ValorTroco { get; private set; }

        public PagamentoVenda(Venda venda,
            FormaPagamento formaPagamento,
            decimal valorPagamento,
            decimal valorTroco)
        {
            if (venda == null) throw new Exception("A venda deve ser informada");
            if (formaPagamento == null) throw new Exception("A forma de pagamento deve ser informada");

            VendaId = venda.Id;
            FormaPagamentoId = formaPagamento.Id;
            ValorPagamento = valorPagamento;
            ValorTroco = valorTroco;
        }

        public PagamentoVenda()
        {

        }
    }
}
