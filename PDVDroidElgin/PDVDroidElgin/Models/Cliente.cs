using System;
using System.Collections.Generic;
using System.Text;

namespace PDVDroidElgin.Models
{
    public class Cliente
    {
        public Cliente(string nome, string cpfCnpj)
        {
            Nome = nome;
            CpfCnpj = cpfCnpj;
        }

        public Cliente()
        {

        }

        public int Id { get; private set; }
        public string Nome { get; set; }
        public string CpfCnpj { get; set; }

        public string xlgrdest { get; set; }
        public string nrodest { get; set; }
        public string xcpldest { get; set; }
        public string xbairrodest { get; set; }
        public string cmundest { get; set; }
        public string xmundest { get; set; }
        public string ufdest { get; set; }
        public string cepdest { get; set; }
        public string cpaisdest { get; set; }
        public string xpaisdest { get; set; }
        public string fonedest { get; set; }
        public string indiedest { get; set; }
        public string iedest { get; set; }
        public string isuf { get; set; }
        public string imdest { get; set; }
        public string email { get; set; }
    }
}
