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
        public T GetDadosDaAPI<T>(object dados, string endpoint, HttpMethod tipo)
        {
            T result;
            JsonSerializer serializer = new JsonSerializer();
            HttpClient apiClient = new HttpClient();
            dados = dados ?? new { };
            try
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(dados), System.Text.Encoding.UTF8, @"application/json"); HttpRequestMessage request = new HttpRequestMessage(tipo, "https://localhost:44311/" + endpoint); request.Content = content; using (HttpResponseMessage response = apiClient.SendAsync(request).GetAwaiter().GetResult())
                {
                    if (response.IsSuccessStatusCode)
                    {
                        using (Stream stream = response.Content.ReadAsStreamAsync().GetAwaiter().GetResult())
                        using (StreamReader reader = new StreamReader(stream))
                        using (JsonTextReader json = new JsonTextReader(reader))
                        {
                            result = serializer.Deserialize<T>(json);
                        }
                    }
                    else
                    {
                        result = (T)Activator.CreateInstance(typeof(T));
                        //erro na requisicao
                    }
                }
            }
            catch (Exception ex)
            {
                result = (T)Activator.CreateInstance(typeof(T));
            }
            return result;
        }

        public string DropItem ( )
        {
            throw new NotImplementedException ( );
        }

    }
}
