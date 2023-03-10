using AppControleEstoque.Regras.Services;
using AppControleEstoque.Service.API.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppControleEstoque.Service.API.Implementations
{
    public class EstoqueService : IEstoqueService
    {
        readonly IAPIClient _apiClient;
        public EstoqueService(IAPIClient apiClient)
        {
            _apiClient = apiClient;
        }
        public string AlterarItem(object item)
        {
            throw new NotImplementedException();
        }

        public string CadastrarItem(object item)
        {
            throw new NotImplementedException();
        }

        public string ExcluirItem(string codigo)
        {
            throw new NotImplementedException();
        }

        public object ObtemItemFiltrado(string codigo = null, string descricao = null)
        {
            throw new NotImplementedException();
        }

        public List<object> ObtemListaEstoque()
        {
            throw new NotImplementedException();
        }
    }
}
