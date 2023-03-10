using AppControleEstoque.Core;
using AppControleEstoque.Regras.Infraestructure;
using AppControleEstoque.Regras.Models;
using AppControleEstoque.Regras.Services;
using AppControleEstoque.Service.API.Implementations;
using Autofac;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using static AppControleEstoque.Core.AppSetup;

namespace AppControleEstoque.ViewModels
{
    public class MainPageViewModel
    {
        #region Variaveis
        private IDatabaseCRUD<EstoqueModel> _estoqueSqlite;
        private IEstoqueService _estoqueService;
        #endregion

        #region Construtor
        public MainPageViewModel()
        {
            _estoqueSqlite = AppContainer.Container.Resolve<IDatabaseCRUD<EstoqueModel>>();
            _estoqueService = AppContainer.Container.Resolve<IEstoqueService>();
            ListaDeProdutosExibida = new ObservableCollection<EstoqueModel>();
        }
        #endregion

        #region Membros
        public ObservableCollection<EstoqueModel> ListaDeProdutosExibida { get; set;}
        public List<EstoqueModel> ListaDeProdutosCompleta { get; set; }
        #endregion

        #region Métodos

        public void CarregarEstoque()
        {
            ListaDeProdutosCompleta = _estoqueSqlite.SelectAll ( );
            //fica presumido que os dados do sqlite sempre vão estar na frente dos da api visto que o usuário poderia fazer alguma atualização off então aqui farei a atualização dos dados
            MesclarDados ( ListaDeProdutosCompleta, _estoqueService.ObtemListaEstoque ( ) );
        }
        
        private void MesclarDados(List<EstoqueModel> listaSqlite,List<EstoqueModel> listaApi )
        {
            foreach (EstoqueModel itemSql in listaSqlite)
            {
                //atualiza oq for igual
                if (itemSql.Id == ( from itemApi in listaApi where itemApi.Id == itemSql.Id select itemApi.Id).FirstOrDefault())
                {
                    _estoqueService.AlterarItem ( itemSql );
                }
                //adiciona oq estiver a mais
                else
                {
                    _estoqueService.CadastrarItem ( itemSql );
                }

            }
        }

        public void PesquisaProduto( int codigo)
        {
            LimparListaExibicao();
            ListaDeProdutosExibida.Add(DevolveProdutoPeloCodigo(codigo));
        }
        public void ExibirListaCompleta()
        {
            LimparListaExibicao();
            foreach (EstoqueModel item in ListaDeProdutosCompleta)
                ListaDeProdutosExibida.Add(item);
        }
        private void LimparListaExibicao()
        {
            if (ListaDeProdutosExibida != null && ListaDeProdutosExibida.Any())
                ListaDeProdutosExibida.Clear();
        }
        public void DeletarProduto(int id)
        {
            _estoqueSqlite.DeleteOne(id);

            CarregarEstoque();
            ExibirListaCompleta();
            _estoqueService.ExcluirItem ( id );
        }
        public EstoqueModel DevolveProdutoPeloCodigo(int codigo)
        {
            return ListaDeProdutosCompleta.Where(produto => produto.Id == codigo).FirstOrDefault();
        }
        #endregion
    }
}
