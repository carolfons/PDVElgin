using PDVDroidElgin.Models;
using PDVDroidElgin.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace PDVDroidElgin.Contracts
{
    public interface IControladorPDV
    {
        InfoCaixaViewModel ObterInfoCaixa(Usuario operador);
        void AbrirCaixa(Usuario operador, decimal valor);
        void FecharCaixa(Usuario operador, decimal valor);

        IReadOnlyCollection<VendaViewModel> ObterVendas();
        IReadOnlyCollection<ItemVendaViewModel> ObterItensVendaAtual();
        ResumoVendaViewModel ObterResumo();
        bool VendaEmAndamento();
        void AbreCupom(string cpfCnpj);
        void EfetuaDesconto(decimal valorDesconto);
        void EfetuaPagamento(FormaPagamento fpgDinheiro, decimal valorPago);
        void AdicionaItem(ProdutoViewModel item, int quantidade);
        void RemoveItem();
        void FechaCupom();
        int GetVendaIdEmAndamento();
        void SetVendaIdEmAberto(int vendaIdEmAberto);
        List<ProdutoViewModel> BuscarProdutos(string busca);
        void ImprimirEmTexto(int vendaId);
        void ImprimirXML(int vendaId);
    }
}
