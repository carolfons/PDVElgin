using System;
using System.Collections.Generic;
using System.Text;

namespace PDVDroidElgin.Models
{
    public class Produto
    {
        public Produto()
        {

        }

        public Produto(string descricao, 
            string grupo,
            string gtin, 
            string un,
            decimal preco,
            string ncm, 
            string cest,
            string cfop, 
            string base64Img = null)
        {
            Descricao = descricao;
            Grupo = grupo;
            GTIN = gtin;
            UN = un;
            Preco = preco;
            NCM = ncm;
            CEST = cest;
            CFOP = cfop;
            Origem = "0";
            Base64Img = base64Img;
        }

        public int Id { get; set; }
        public string Descricao { get; set; }
        public string GTIN { get; set; }
        public string UN { get; set; }
        public string Grupo { get; set; }
        public decimal Preco { get; set; }
        public string NCM { get; set; }
        public string CEST { get; set; }
        public string CFOP { get; set; }
        public string Origem { get; set; }

        public string CstPIS { get; set; }
        public decimal AliqPIS { get; set; }

        public string CstCOFINS { get; set; }
        public decimal AliqCofins { get; set; }

        public string CstIcms { get; set; }
        public decimal AliqIcms { get; set; }

        public decimal AliqIbptFederal { get; set; }
        public decimal AliqIbptEstadual { get; set; }
        public decimal AliqIbptMunicipal { get; set; }

        public string Base64Img { get; private set; }
    }
}
