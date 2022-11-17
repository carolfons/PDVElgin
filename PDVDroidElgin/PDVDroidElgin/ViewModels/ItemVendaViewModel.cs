using PDVDroidElgin.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PDVDroidElgin.ViewModels
{
    public class ItemVendaViewModel
    {
        public string Codigo { get; set; }
        public string Descricao { get; set; }
        public string UN { get; set; }
        public decimal Quantidade { get; set; }
        public decimal ValorUnit { get; set; }
        public decimal Desconto { get; set; }
        public decimal Total { get; set; }

        public string QuantidadeStr { get; set; }
        public string TotalStr { get; set; }
        public string DescontoStr { get; set; }
        public string ValorUnitStr { get; set; }


        public ItemVendaViewModel(string codigo,
            string descricao,
            string un,
            decimal quantidade,
            decimal valorUnit,
            decimal desconto)
        {
            Codigo = codigo;
            Descricao = descricao;
            Quantidade = quantidade;
            ValorUnit = valorUnit;
            Desconto = desconto;
            UN = un;
            Total = (valorUnit * quantidade) - desconto;

            FillTotaisStr();
        }

        private void FillTotaisStr()
        {
            ValorUnitStr = $"R$ {ValorUnit.ToString("N2")}";
            DescontoStr = $"R$ {Desconto.ToString("N2")}";
            TotalStr = $"R$ {Total.ToString("N2")}";
            QuantidadeStr = $"{Quantidade} {UN}";
        }

        public ItemVendaViewModel(ItemVenda i)
        {
            Codigo = i.ProdutoId.ToString();
            Descricao = i.Produto.Descricao;
            Quantidade = i.Quant;
            ValorUnit = i.ValorUnit;
            Desconto = i.VDesconto;
            Total = i.VTotalProd;
            UN = i.Produto.UN;

            FillTotaisStr();
        }

        public ItemVendaViewModel()
        {

        }

    }
}
