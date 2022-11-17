using PDVDroidElgin.Contracts;
using PDVDroidElgin.Data;
using PDVDroidElgin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PDVDroidElgin
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VerEmpresa : ContentPage
    {
        public VerEmpresa()
        {
            InitializeComponent();

        }

        private void ContentPage_Appearing(object sender, EventArgs e)
        {
            using (PdvContext ctx = new PdvContext())
            {
                Empresa emp = ctx.Empresas.Find(1);
                this.BindingContext = emp;
            }
        }
    }
}