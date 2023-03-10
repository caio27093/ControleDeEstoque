using System;
using System.Collections.Generic;
using System.Text;

namespace AppControleEstoque.Service.API.Interface
{
    //vai servir como uma classe para implementar todas as chamadas para a api pensando que essa aplicação poderia crescer
    public interface IAPIClient
    {
        string DropItem ( );
    }
}
