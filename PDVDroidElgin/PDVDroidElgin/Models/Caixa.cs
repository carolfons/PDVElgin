using System;
using System.Collections.Generic;
using System.Text;

namespace PDVDroidElgin.Models
{
    public class Caixa
    {
        public Caixa(Usuario operador, string terminal, decimal valorAbertura)
        {
            UsuarioId = operador.Id;
            Terminal = terminal;
            ValorAbertura = valorAbertura;
            DHAbertura = DateTime.Now;
        }

        public Caixa()
        {

        }

        public int Id { get; set; }
        public int UsuarioId { get; private set; }
        public string Terminal { get; private set; }
        public DateTime DHAbertura { get; private set; }
        public decimal ValorAbertura { get; private set; }
        public DateTime? DHFechamento { get; private set; }
        public decimal ValorFechamento { get; private set; }
        public decimal Diferenca { get; private set; }

        public void FecharCaixa(decimal valorFechamento)
        {
            DHFechamento = DateTime.Now;
            ValorFechamento = valorFechamento;

            Diferenca = 0; // = ??? - implementar;
        }
    }
}
