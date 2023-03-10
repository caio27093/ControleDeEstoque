using AppControleEstoque.Regras.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppControleEstoque.Regras.Services
{
    public interface IEstoqueService
    {
        List<EstoqueModel> ObtemListaEstoque();
        EstoqueModel ObtemItemFiltrado (int codigo);
        bool CadastrarItem(EstoqueModel item);
        bool AlterarItem(EstoqueModel item);
        bool ExcluirItem(int codigo);
    }
}
