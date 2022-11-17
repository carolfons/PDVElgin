using System;
using System.Collections.Generic;
using System.Text;

namespace PDVDroidElgin.Models
{
    public class ItemVenda
    {

        public ItemVenda(Venda venda, Produto produto, int quantidade)
        {
            VendaId = venda.Id;
            ProdutoId = produto.Id;
            Quant = quantidade;
            ValorUnit = produto.Preco;
            VTotalProd = (Quant * ValorUnit);
        }

        public ItemVenda()
        {

        }

        public int id { get; set; }

        public int VendaId { get; set; }
        public virtual Venda Venda { get; set; }


        public int ProdutoId { get; set; }
        public virtual Produto Produto { get; set; }


        public decimal ValorUnit { get; set; }
        public int Quant { get; set; }
        public decimal VDesconto { get; set; }
        public decimal VTotalProd { get; set; }


        public string ModBCIcms { get; set; }
        public string CstICMS { get; set; }
        public string ValBaseIcms { get; set; }
        public string AliqIcms { get; set; }
        public string ValICMS { get; set; }


        public string ModBCIcmsST { get; set; }
        public string ValBaseIcmsST { get; set; }
        public string AliqIcmsST { get; set; }
        public string ValIcmsST { get; set; }


        public string ValBaseFCP { get; set; }
        public string AliqFCP { get; set; }
        public string ValFCP { get; set; }


        public string CstIpi { get; set; }
        public string ValBaseIpi { get; set; }


        public string CstPis { get; set; }
        public string ValBasePis { get; set; }
        public string AliqPis { get; set; }
        public string ValPis { get; set; }


        public string CstCofins { get; set; }
        public string ValBaseCofins { get; set; }
        public string AliqCofins { get; set; }
        public string ValCofins { get; set; }

    }
}
