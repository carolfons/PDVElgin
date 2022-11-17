using System;
using System.Collections.Generic;
using System.Text;

namespace PDVDroidElgin.Models
{
    public class FormaPagamento
    {
        public FormaPagamento()
        {

        }
        public FormaPagamento(string descricao, string codSefaz)
        {
            if (string.IsNullOrEmpty(descricao)) throw new Exception("A descrição é obrigatória");
            if (string.IsNullOrEmpty(codSefaz)) throw new Exception("O Código SEFAZ da forma de pagamento é obrigatório");

            Descricao = descricao;
            CodSefaz = codSefaz;
        }

        public int Id { get; private set; }
        public string Descricao { get; private set; }
        public string CodSefaz { get; private set; }
    }
}
