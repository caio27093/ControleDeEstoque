using AppControleEstoque.Core;
using AppControleEstoque.Regras.Infraestructure;
using AppControleEstoque.Regras.Models;
using AppControleEstoque.Regras.Services;
using Autofac;
using System;
using System.Collections.Generic;
using System.Text;
using static AppControleEstoque.Core.AppSetup;

namespace AppControleEstoque.ViewModels
{
    public class DetalheProdutoEstoqueViewModel
    {
        #region Variaveis
        private IDatabaseCRUD<EstoqueModel> _estoqueSqlite;
        private IEstoqueService _estoqueService;
        #endregion

        #region Construtor
        public DetalheProdutoEstoqueViewModel()
        {
            _estoqueSqlite = AppContainer.Container.Resolve<IDatabaseCRUD<EstoqueModel>>();
            _estoqueService = AppContainer.Container.Resolve<IEstoqueService>();
        }
        #endregion

        #region Membros
        public EstoqueModel Produto { get; set; }
        #endregion

        #region Métodos
        public bool ValidadorDeDados(bool isInserting)
        {
            if (Produto.Quantidade <= 0 && isInserting)
                return false;
            if (string.IsNullOrEmpty(Produto.Nome) || Produto.Nome.Length > 200)
                return false;

            return true;
        }

        public bool CadastrarProduto()
        {
            try
            {
                _estoqueSqlite.InsertOnly(Produto);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool AlterarProduto()
        {
            try
            {
                _estoqueSqlite.Update(Produto);
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion
    }
}
