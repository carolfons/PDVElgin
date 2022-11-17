using PDVDroidElgin.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xamarin.Forms;

namespace PDVDroidElgin.ViewModels
{
    public class ProdutoViewModel : Produto
    {
        public string PrecoStr => $"R$ {Preco:N2}";

        public ProdutoViewModel()
        {

        }
    }
}
