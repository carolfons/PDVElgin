                          using PDVDroidElgin.Contracts;
using PDVDroidElgin.Hardware;
using PDVDroidElgin.Models;
using PDVDroidElgin.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace PDVDroidElgin.Data
{
    internal class ControladorPDVImpl : IControladorPDV
    {
        private int vendaIdEmAndamento = 0;

        public IReadOnlyCollection<VendaViewModel> ObterVendas()
        {
            using (PdvContext db = new PdvContext())
            {
                List<Venda> vendas = db.Vendas.ToList();
                List<VendaViewModel> viewModels = new List<VendaViewModel>();
                vendas.ForEach(v => viewModels.Add(
                         new VendaViewModel(venda: v)
                    ));

                return viewModels.AsReadOnly();
            }
        }

        public void AbreCupom(string cpfCnpj)
        {
            using (PdvContext db = new PdvContext())
            {
                Empresa empresa = db.Empresas.Find(1);
                Cliente cliente = db.Clientes.FirstOrDefault(c => c.CpfCnpj.Equals(cpfCnpj));
                Usuario usuarioLogado = Login.UsuarioLogado;

                Venda venda = new Venda(
                        empresa,
                        usuarioLogado,
                        cliente);

                db.Vendas.Add(venda);
                db.SaveChanges();

                vendaIdEmAndamento = venda.Id;
            }
        }

        public void AdicionaItem(ProdutoViewModel item, int quantidade)
        {
            using (PdvContext db = new PdvContext())
            {
                Venda venda = db.Vendas.Find(vendaIdEmAndamento);
                if (venda == null) throw new Exception($"Venda N° {vendaIdEmAndamento} não foi localizada!");

                Produto produto = db.Produtos.Find(item.Id);
                if (produto == null) throw new Exception($"Produto {item.Id} não foi localizado!");

                venda.AdicionaItem(produto, quantidade);

                db.Vendas.Update(venda);
                db.SaveChanges();
            }
        }



        public void EfetuaPagamento(FormaPagamento fpg, decimal valorPago)
        {
            using (PdvContext db = new PdvContext())
            {
                Venda venda = db.Vendas.Find(vendaIdEmAndamento);
                if (venda == null) throw new Exception($"Venda N° {vendaIdEmAndamento} não foi localizada!");

                venda.EfetuaPagamento(fpg, valorPago);

                db.Vendas.Update(venda);
                db.SaveChanges();

                vendaIdEmAndamento = 0;
            }
        }

        public void EfetuaDesconto(decimal valorDesconto)
        {

        }

        public void RemoveItem()
        {

        }

        public void FechaCupom()
        {

        }

        public List<ProdutoViewModel> BuscarProdutos(string busca)
        {
            busca = busca.ToLower();
            using (PdvContext db = new PdvContext())
            {
                int idBusca = 0;
                int.TryParse(busca, out idBusca);

                List<Produto> produtos = db.Produtos
                    .Where(p =>
                        p.Descricao.ToLower().Contains(busca) ||
                        p.Id == idBusca)
                    .ToList();

                List<ProdutoViewModel> result = ModelMap.Map<List<ProdutoViewModel>>(produtos);
                return result;
            }
        }

        public IReadOnlyCollection<ItemVendaViewModel> ObterItensVendaAtual()
        {
            using (PdvContext db = new PdvContext())
            {
                Venda venda = db.Vendas.Find(vendaIdEmAndamento);
                if (venda == null) return new List<ItemVendaViewModel>();

                List<ItemVenda> itens = venda.ItensVenda.ToList();
                List<ItemVendaViewModel> result = new List<ItemVendaViewModel>();

                itens.ForEach(i => result.Add(new ItemVendaViewModel(i)));
                return result;
            }
        }

        public ResumoVendaViewModel ObterResumo()
        {
            using (PdvContext db = new PdvContext())
            {
                Venda venda = db.Vendas.Find(vendaIdEmAndamento);
                if (venda == null) return new ResumoVendaViewModel();
                return new ResumoVendaViewModel(venda);
            }
        }

        public bool VendaEmAndamento()
        {
            return vendaIdEmAndamento > 0;
        }

        public void SetVendaIdEmAberto(int vendaIdEmAberto)
        {
            vendaIdEmAndamento = vendaIdEmAberto;
        }

        public InfoCaixaViewModel ObterInfoCaixa(Usuario operador)
        {
            using (PdvContext db = new PdvContext())
            {
                Caixa caixa = db.Caixas
                    .OrderByDescending(c => c.DHAbertura)
                    .FirstOrDefault(
                        c => c.UsuarioId.Equals(operador.Id)
                    );

                return new InfoCaixaViewModel(operador, caixa);
            }
        }

        public void AbrirCaixa(Usuario operador, decimal valor)
        {
            using (PdvContext db = new PdvContext())
            {
                string terminal = DeviceInfo.Name;
                Caixa caixa = new Caixa(operador, terminal, valor);

                db.Caixas.Add(caixa);
                db.SaveChanges();
            }
        }

        public void FecharCaixa(Usuario operador, decimal valor)
        {
            using (PdvContext db = new PdvContext())
            {
                Caixa caixa = db.Caixas
                    .OrderByDescending(c => c.DHAbertura)
                    .FirstOrDefault(
                        c => c.UsuarioId.Equals(operador.Id) && c.DHFechamento == null
                    );

                if (caixa == null) return;

                caixa.FecharCaixa(valor);

                db.Caixas.Update(caixa);
                db.SaveChanges();
            }
        }

        public int GetVendaIdEmAndamento()
        {
            return vendaIdEmAndamento;
        }

        public void ImprimirEmTexto(int vendaId)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("VENDA ESTABELECIMENTO");
                sb.AppendLine("");

                using (PdvContext ctx = new PdvContext())
                {
                    Empresa emp = ctx.Empresas.Find(1);
                    Venda venda = ctx.Vendas.Find(vendaId);

                    sb.AppendLine($"N°: {venda.Id}");
                    sb.AppendLine($"Data/Hora: {venda.DhEmi.ToString("dd/MM/yyyy HH:mm")}");
                    sb.AppendLine($"Vendedor: {venda.Usuario.Nome}");
                    sb.AppendLine($"CPF Cliente: SEM CPF");
                    sb.AppendLine("");
                    sb.AppendLine("");

                    sb.AppendLine("Produto   QTD.    VUnit.   Total");

                    foreach (ItemVenda item in venda.ItensVenda)
                    {
                        sb.AppendLine(item.Produto.Descricao);
                        sb.AppendLine($"    {item.Quant}{item.Produto.UN} x R$ {item.ValorUnit:N2} = R$ {item.VTotalProd:N2}");
                        sb.AppendLine("");
                    }

                    sb.AppendLine("");
                    sb.AppendLine("");

                    sb.AppendLine($"TOTAL                  R$ {venda.vNF:N2}");

                    sb.AppendLine("PAGAMENTO");
                    sb.Append("");

                    foreach (PagamentoVenda pag in venda.Pagamentos)
                    {
                        sb.AppendLine($"DINHEIRO              R$ {pag.ValorPagamento:N2}");
                        sb.AppendLine("");
                    }
                }

                string nf = sb.ToString();

                DependencyService.Get<IPrinter>().ImprimeTexto(new PrinterCommand(text: nf));

                int avancaLinhas = DependencyService.Get<IPrinter>().AvancaLinhas(10);
                int cutPaperResult = DependencyService.Get<IPrinter>().CutPaper(10);


            }
            catch (Exception ex)
            {
                DependencyService.Get<IToastFacade>().ShortAlert($"*** ERRO DE IMPRESSAO ***");
                DependencyService.Get<IToastFacade>().LongAlert(ex.Message);
            }
        }

        public void ImprimirXML(int vendaId)
        {

            string xmlPartial = Resources.xmlnfce;
            string xmlItemPartial = Resources.detalhesNFCe;

            string xmlNFCe = string.Empty;
            using (PdvContext ctx = new PdvContext())
            {
                Venda venda = ctx.Vendas.Find(vendaId);

                xmlNFCe = xmlPartial
                    .Replace("@num", venda.Id.ToString())
                    .Replace("@data", $"{venda.DhEmi:yyyy-MM-dd}T{venda.DhEmi:HH-mm-ss}-03:00")
                    .Replace("@vNF", venda.vNF.ToString())
                    .Replace("@valorPag", venda.Pagamentos.ToList()[0].ValorPagamento.ToString());

                string itens = string.Empty;

                foreach (ItemVenda item in venda.ItensVenda)
                {
                    itens += xmlItemPartial
                        .Replace("@produto", item.Produto.Descricao)
                        .Replace("@quant", item.Quant.ToString())
                        .Replace("@totalItem", item.VTotalProd.ToString())
                        .Replace("@vUnit", item.ValorUnit.ToString());
                }

                xmlNFCe = xmlNFCe.Replace("@itens", itens);
            }

            try
            {
                IPrinter printer = DependencyService.Get<IPrinter>();
                printer.ImprimeXMLNFCe(xmlNFCe, 1, "CODIGO-CSC-CONTRIBUINTE-36-CARACTERES", 0);
                Thread.Sleep(2000);
                printer.CutPaper(30);
            }
            catch (Exception ex)
            {
                DependencyService.Get<IToastFacade>().ShortAlert($"*** ERRO DE IMPRESSAO ***");
                DependencyService.Get<IToastFacade>().LongAlert(ex.Message);
            }
        }
    }
}
