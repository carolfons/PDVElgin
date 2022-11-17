using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PDVDroidElgin.Models
{
    public class Empresa
    {
        internal int cUF;

        public Empresa()
        {

        }

        public Empresa(string nome, string cnpj, string ie, int crt = 3)
        {
            int[] crtsValidos = new int[] { 1, 2, 3 };
            if (!crtsValidos.Any(c => c.Equals(crt))) throw new Exception("CRT informado não existe");
            if (string.IsNullOrEmpty(nome))  throw new Exception("O nome é obrigatório");
            if(string.IsNullOrEmpty(cnpj)) throw new Exception("O CNPJ é obrigatório");
            if (cnpj.Length != 14) throw new Exception("Formato do CNPJ é inválido");
            if (string.IsNullOrEmpty(ie)) throw new Exception("A IE é obrigatória");

            Nome = nome;
            Cnpj = cnpj;
            IE = ie;
            CRT = crt;
        }

        public Empresa AlterarEndereco(string logradouro, string bairro, string cidade, 
            string uf,  string cep, string codIBGE)
        {
            Logradouro = logradouro;
            Bairro = bairro;
            Cidade = cidade;
            UF = uf;
            CodIBGE = codIBGE;
            CEP = cep;
            return this;
        }

        public Empresa DefineNumeracaoFiscal(int serie, int ultimaNF)
        {
            Serie = serie;
            UltimaNF = ultimaNF;
            return this;
        }

        public int Id { get; set; }
        public string Nome { get; private set; }
        public string Cnpj { get; private set; }
        public string IE { get; private set; }
        public string Logradouro { get; private set; }
        public string Numero { get; private set; }
        public string Bairro { get; private set; }
        public string Cidade { get; private set; }
        public string UF { get; private set; }
        public string CodIBGE { get; private set; }
        public string CEP { get; private set; }
        public int CRT { get; private set; }
        public int FormaEmissao { get; private set; }
        public int Serie { get; private set; }
        public int UltimaNF { get; private set; }
    }
}
