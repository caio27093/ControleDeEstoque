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
    public class EstoqueResponseModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Quantidade { get; set; }
    }
}
