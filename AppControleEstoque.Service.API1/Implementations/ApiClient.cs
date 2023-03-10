using AppControleEstoque.Service.API.Interface;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using System.IO;
using System.Threading.Tasks;

namespace AppControleEstoque.Service.API.Implementations
{
    public class ApiClient : IAPIClient
    {
        #region Variaveis
        static string _baseUrl = "A base da sua url vai aqui";
        #endregion
        #region Dados da API
        public ResponseModelPadrao<T> PutPostDeleteAPI<T>(object dados, string endpoint, HttpMethod tipo)
        {
            ResponseModelPadrao<T> result;
            JsonSerializer serializer = new JsonSerializer();
            HttpClient apiClient = new HttpClient();
            dados = dados ?? new { };

            try
            {

                StringContent content = new StringContent ( JsonConvert.SerializeObject ( dados ), Encoding.UTF8, @"application/json" );
                HttpRequestMessage request = new HttpRequestMessage(tipo, _baseUrl + endpoint);

                request.Content = content;

                using (HttpResponseMessage response = apiClient.SendAsync(request).GetAwaiter().GetResult())
                {
                    if (response.IsSuccessStatusCode)
                    {
                        using (Stream stream = response.Content.ReadAsStreamAsync().GetAwaiter().GetResult())
                        using (StreamReader reader = new StreamReader(stream))
                        using (JsonTextReader json = new JsonTextReader(reader))
                        {
                            result = serializer.Deserialize<ResponseModelPadrao<T>> (json);
                        }
                    }
                    else
                    {
                        result = (ResponseModelPadrao<T>)Activator.CreateInstance(typeof( ResponseModelPadrao<T> ) );
                    }
                }
            }
            catch (Exception ex)
            {
                result = (ResponseModelPadrao<T>)Activator.CreateInstance(typeof( ResponseModelPadrao<T> ) );
            }
            return result;
        }
        public ResponseModelPadrao<T> GetDataFromAPI<T> ( string endpoint )
        {
            ResponseModelPadrao<T> result;
            JsonSerializer serializer = new JsonSerializer ( );
            try
            {
                //para passar quando for um localhost
                #if DEBUG
                    HttpClientHandler insecureHandler = GetInsecureHandler ( );
                    HttpClient apiClient = new HttpClient ( insecureHandler );
                #else
                    HttpClient apiClient = new HttpClient();
                #endif

                HttpRequestMessage request = new HttpRequestMessage ( HttpMethod.Get, _baseUrl + endpoint );
                using (HttpResponseMessage response = apiClient.SendAsync ( request ).GetAwaiter ( ).GetResult ( ))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        using (Stream stream = response.Content.ReadAsStreamAsync ( ).GetAwaiter ( ).GetResult ( ))
                        using (StreamReader reader = new StreamReader ( stream ))
                        using (JsonTextReader json = new JsonTextReader ( reader ))
                        {
                            result = serializer.Deserialize<ResponseModelPadrao<T>> ( json );
                        }
                    }
                    else
                    {
                        result = (ResponseModelPadrao<T>)Activator.CreateInstance ( typeof ( ResponseModelPadrao<T> ) );
                    }
                }
            }
            catch (Exception ex)
            {
                result = (ResponseModelPadrao<T>)Activator.CreateInstance ( typeof ( ResponseModelPadrao<T> ) );
            }

            return result;
        }
        #endregion
        #region Conexão com localHost
        private HttpClientHandler GetInsecureHandler ( )
        {
            HttpClientHandler handler = new HttpClientHandler ( );
            handler.ServerCertificateCustomValidationCallback = ( message, cert, chain, errors ) =>
            {
                if (cert.Issuer.Equals ( "CN=localhost" ))
                    return true;
                return errors == System.Net.Security.SslPolicyErrors.None;
            };
            return handler;
        }
        #endregion
        #region MÉTODOS IMPLEMENTADOS
        public ResponseModelPadrao<EstoqueResponseModel> DropItem ( int pk )
        {
            return PutPostDeleteAPI<EstoqueResponseModel> ( new { id = pk }, "api/Estoque/DelEstoque", HttpMethod.Delete );
        }
        public ResponseModelPadrao<EstoqueResponseModel> GetListaEstoque ( )
        {
            return GetDataFromAPI<EstoqueResponseModel> ("api/Estoque/ListEstoque");
        }

        public ResponseModelPadrao<EstoqueResponseModel> GetEstoquePeloId ( int pk )
        {
            return PutPostDeleteAPI<EstoqueResponseModel> ( new { id = pk }, "api/Estoque/GetEstoqueById", HttpMethod.Post );
        }
        public ResponseModelPadrao<EstoqueResponseModel> CadastroEstoque ( EstoqueRequestModel estoque )
        {
            return PutPostDeleteAPI<EstoqueResponseModel> ( estoque, "api/Estoque/CadEstoque", HttpMethod.Post );
        }
        public ResponseModelPadrao<EstoqueResponseModel> PutEstoque ( EstoqueRequestModel estoque )
        {
            return PutPostDeleteAPI<EstoqueResponseModel> ( estoque, "api/Estoque/AltEstoque", HttpMethod.Put );
        }
        #endregion
    }
}
