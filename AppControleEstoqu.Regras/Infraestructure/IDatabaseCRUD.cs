using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace AppControleEstoque.Regras.Infraestructure
{
    public interface IDatabaseCRUD<T> where T : new()
    {
        int CountReg();
        void DeleteAll();
        void DropTable();
        void DeleteOne(string pk);
        void DeleteOne(int pk);
        int Insert(T obj);
        int InsertOnly(T obj);
        List<T> SelectAll(Expression<Func<T, bool>> predExpr = null);
        T SelectOne(Expression<Func<T, bool>> predExpr = null);
        void Update(T obj);
    }
}
