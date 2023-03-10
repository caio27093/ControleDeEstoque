using System;
using SQLite;
using System.Collections.Generic;
using System.Text;

namespace AppControleEstoque.DAL.Tabelas
{
    [Table("TbEstoque")]
    public class TbEstoque
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Nome { get; set; }
        public int Quantidade { get; set; }
    }
}
