using Microsoft.EntityFrameworkCore;
using PDVDroidElgin.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using static System.Environment;

namespace PDVDroidElgin.Data
{
    internal class PdvContext : DbContext
    {
        public PdvContext()
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var dbPath = Path.Combine(GetFolderPath(SpecialFolder.Personal), "PDVElgin.sqlite3");
            optionsBuilder.UseSqlite($"Filename={dbPath}");
            optionsBuilder.UseLazyLoadingProxies();
        }


        private static bool createCheckd = false;
        public void CheckCreated(Func<string, int> fNotificacaoCriarBanci)
        {
            if (createCheckd) return;
            Database.EnsureCreated();

            Empresa empresa = Empresas.Find(1);
            if (empresa == null)
            {
                fNotificacaoCriarBanci("Inicializando banco de dados. Não encerre o aplicativo.");



                empresa = new Empresa(
                        nome: "ELGIN SA",
                        cnpj: "52556578000122",
                        ie: "00000000")
                    .AlterarEndereco(
                        logradouro: "Av Ver Dante Jordao Stopa - 47",
                        bairro: "Jd Cintia",
                        cidade: "Mogi Das Cruzes",
                        uf: "Mogi Das Cruzes",
                        cep: "08.820-390",
                        codIBGE: "04875508")
                    .DefineNumeracaoFiscal(
                        serie: 1,
                        ultimaNF: 0);

                Empresas.Add(empresa);

                Usuario usuario = new Usuario(
                        nome: "Admin",
                        senha: "1234");

                //adicionando no banco
                Usuarios.Add(usuario);


                List<FormaPagamento> formasPg = new List<FormaPagamento>();
                formasPg.Add(new FormaPagamento("DINHEIRO", "01"));
                formasPg.Add(new FormaPagamento("CHEQUE", "02"));
                formasPg.Add(new FormaPagamento("CARTÃO DE CRÉDITO", "03"));
                formasPg.Add(new FormaPagamento("CARTÃO DE DÉBITO", "04"));
                formasPg.Add(new FormaPagamento("CRÉDITO LOJA", "05"));
                formasPg.Add(new FormaPagamento("VALE ALIMENTAÇÃO", "10"));
                formasPg.Add(new FormaPagamento("VALE REFEIÇÃO", "11"));
                formasPg.Add(new FormaPagamento("VALE PRESENTE", "12"));
                formasPg.Add(new FormaPagamento("VALE COMBUSTÍVEL", "13"));
                formasPg.Add(new FormaPagamento("OUTROS", "99"));

                //adicionando no banco
                formasPg.ForEach(fpg => FormasPagamento.Add(fpg));

                string[] fotos = FotosCargaInicial.Get();

                List<Produto> prods = new List<Produto>();
                prods.Add(new Produto("Fox Consulta", "Auto Atendimento", "SEM GTIN", "UN", 59.99m, "NCM", "CEST", "5102", fotos[0]));
                prods.Add(new Produto("Fox Light", "Auto Atendimento", "SEM GTIN", "UN", 59.99m, "NCM", "CEST", "5102", fotos[1]));
                prods.Add(new Produto("Fox Light com Dispensador de Gel", "Auto Atendimento", "SEM GTIN", "UN", 5.99m, "NCM", "CEST", "5102", fotos[2]));
                prods.Add(new Produto("Fox Pagamento", "Auto Atendimento", "SEM GTIN", "UN", 59.99m, "NCM", "CEST", "5102", fotos[3]));
                prods.Add(new Produto("Fox Senha", "Auto Atendimento", "SEM GTIN", "UN", 59.99m, "NCM", "CEST", "5102", fotos[4]));
                prods.Add(new Produto("Fox Teclado", "Auto Atendimento", "SEM GTIN", "UN", 59.99m, "NCM", "CEST", "5102", fotos[5]));
                prods.Add(new Produto("Self Checkout", "Auto Atendimento", "SEM GTIN", "UN", 59.99m, "NCM", "CEST", "5102", fotos[6]));
                prods.Add(new Produto("Self Checkout Lumiere", "Auto Atendimento", "SEM GTIN", "UN", 59.99m, "NCM", "CEST", "5102", fotos[7]));
                prods.Add(new Produto("Terminal DUO", "Auto Atendimento", "SEM GTIN", "UN", 59.99m, "NCM", "CEST", "5102", fotos[8]));

                //adicionando no banco
                prods.ForEach(p => Produtos.Add(p));

                List<Cliente> clientes = new List<Cliente>();
                clientes.Add(new Cliente("Flávio José", "14500209904"));
                clientes.Add(new Cliente("Márcia Pereira", "02314790806"));
                clientes.Add(new Cliente("Angelo da Silva", "89800244704"));
                clientes.Add(new Cliente("Emmanuel Luxemburgo", "30250807789"));

                //adicionando no banco
                clientes.ForEach(c => Clientes.Add(c));

                fNotificacaoCriarBanci("Só mais um momento, estamos terminando a configuração inicial...");
                SaveChanges();
                fNotificacaoCriarBanci("Obrigado por aguardar. Vamos começar!");
                Thread.Sleep(1000);
            }

            createCheckd = true;
        }

        public virtual DbSet<Caixa> Caixas { get; set; }
        public virtual DbSet<Cliente> Clientes { get; set; }
        public virtual DbSet<Empresa> Empresas { get; set; }
        public virtual DbSet<FormaPagamento> FormasPagamento { get; set; }
        public virtual DbSet<ItemVenda> ItensVenda { get; set; }
        public virtual DbSet<Produto> Produtos { get; set; }
        public virtual DbSet<Usuario> Usuarios { get; set; }
        public virtual DbSet<Venda> Vendas { get; set; }
    }
}
