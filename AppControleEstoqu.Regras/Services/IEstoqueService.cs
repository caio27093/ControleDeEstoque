using System;
using System.Collections.Generic;
using System.Text;

namespace AppControleEstoque.Regras.Services
{
    public interface IEstoqueService
    {
        List<object> ObtemListaEstoque();
        object ObtemItemFiltrado(string codigo = null, string descricao = null);
        string CadastrarItem(object item);
        string AlterarItem(object item);
        string ExcluirItem(string codigo);
    }
}
