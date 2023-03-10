using AppControleEstoque.Regras.Models;
using AppControleEstoque.Regras.Services;
using AppControleEstoque.Service.API.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Essentials;

namespace AppControleEstoque.Service.API.Implementations
{
    public class EstoqueService : IEstoqueService
    {
        readonly IAPIClient _apiClient;
        public EstoqueService(IAPIClient apiClient)
        {
            _apiClient = apiClient;
        }
        public bool AlterarItem(EstoqueModel item)
        {
            try
            {
                if (ExisteConexaoComAInternet ( ))
                {
                    ResponseModelPadrao<EstoqueResponseModel> respostaApi = _apiClient.PutEstoque ( MapperEstoqueModelToEstoqueRequestModel ( item ) );
                    return respostaApi.IsSucces;
                }
                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
        }

        public bool CadastrarItem(EstoqueModel item)
        {
            try
            {
                if (ExisteConexaoComAInternet ( ))
                {
                    ResponseModelPadrao<EstoqueResponseModel> respostaApi = _apiClient.CadastroEstoque ( MapperEstoqueModelToEstoqueRequestModel ( item ) );
                    return respostaApi.IsSucces;
                }
                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
        }

        public bool ExcluirItem(int codigo)
        {
            try
            {
                if (ExisteConexaoComAInternet ( ))
                {
                    ResponseModelPadrao<EstoqueResponseModel> respostaApi = _apiClient.DropItem ( codigo );
                    return respostaApi.IsSucces;
                }
                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
        }

        public EstoqueModel ObtemItemFiltrado ( int codigo )
        {
            try
            {
                ResponseModelPadrao<EstoqueResponseModel> respostaApi = _apiClient.GetEstoquePeloId ( codigo );
                return MapperListEstoqueResponseToListEstoque ( respostaApi.Data ).FirstOrDefault ( );
            }
            catch (Exception ex)
            {
                return new EstoqueModel();
            }
        }

        public List<EstoqueModel> ObtemListaEstoque()
        {
            try
            {
                if (ExisteConexaoComAInternet ( ))
                {
                    ResponseModelPadrao<EstoqueResponseModel> respostaApi = _apiClient.GetListaEstoque ( );
                    return MapperListEstoqueResponseToListEstoque ( respostaApi.Data );
                }
                return new List<EstoqueModel> ( );
            }
            catch (Exception ex)
            {

                return new List<EstoqueModel>();
            }
        }
        #region
        public static bool ExisteConexaoComAInternet ( )
        {
            NetworkAccess current = Connectivity.NetworkAccess;

            if (current == NetworkAccess.Internet)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region Mappers
        private List<EstoqueModel> MapperListEstoqueResponseToListEstoque( List<EstoqueResponseModel> listaDeItens )
        {
            List<EstoqueModel> listaEstoque = new List<EstoqueModel> ( );
            foreach (EstoqueResponseModel item in listaDeItens)
            {
                listaEstoque.Add ( new EstoqueModel ( ) 
                {
                    Id = item.Id,
                    Nome = item.Name,
                    Quantidade = item.Quantidade
                } );

            }
            return listaEstoque;
        }
        private EstoqueRequestModel MapperEstoqueModelToEstoqueRequestModel ( EstoqueModel item)
        {
            return new EstoqueRequestModel ( )
            {
                Id = item.Id,
                Name = item.Nome,
                Quantidade = item.Quantidade
            };
        }
        #endregion
    }
}
