using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AppControleEstoque.DAL
{
    public static class DBConfiguration
    {
        public static string CaminhoArquivo { get; set; }
        private static string NomeDb { get; set; } = "Capta_Mobile.db3";
        private static string _caminhoDb;
        public static string CaminhoDb
        {
            get
            {
                if (_caminhoDb == null)
                {
                    _caminhoDb = Path.Combine(CaminhoArquivo, NomeDb);
                }

                return _caminhoDb;
            }
        }
    }
}
