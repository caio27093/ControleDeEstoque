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
    public class ResponseModelPadrao<T>
    {
        public bool IsSucces { get; set; }

        public string Status { get; set; }

        public List<T> Data { get; set; }
    }
}
