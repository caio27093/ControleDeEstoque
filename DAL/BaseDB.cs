using AppControleEstoque.Regras.Infraestructure;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace AppControleEstoque.DAL
{

    public class BaseDB<T> : IDatabaseCRUD<T> where T : new()
    {
        protected static object DbLock = new object();

        public BaseDB(string caminhoDb)
        {
            try
            {
                //Monta o caminho do banco
                CaminhoDB = caminhoDb;

                //Monta a conexão
                SQLiteConnectionWithLock db = new SQLiteConnectionWithLock(new SQLiteConnectionString(CaminhoDB, true));

                lock (DbLock)
                {
                    db.CreateTable<T>();
                }
            }
            catch (Exception ex)
            {

            }
        }

        public string CaminhoDB { get; set; }

        /// <summary>
        /// Monta a conexão a partir de um caminho ao banco. Cada cliente deve fornecer seu caminho (Android, IOS, etc)
        /// </summary>
        public SQLiteConnection GetConnection()
        {
            //Monta a conexão
            SQLiteConnectionWithLock db = new SQLiteConnectionWithLock(new SQLiteConnectionString(CaminhoDB, true));

            return db;
        }

        public virtual int InsertOnly(T obj)
        {

            int r;
            lock (DbLock)
            {
                using (var db = GetConnection())
                {
                    r = db.Insert(obj);
                }
            }
            return r;
        }

        public virtual int Insert(T obj)
        {
            int r;
            lock (DbLock)
                using (var db = GetConnection())
                {
                    r = db.InsertOrReplace(obj);
                }
            return r;
        }

        public void Update(T obj)
        {
            lock (DbLock)
                using (var db = GetConnection())
                {
                    db.Update(obj);
                }
        }

        public void DeleteOne(string pk)
        {
            lock (DbLock)
                using (var db = GetConnection())
                {
                    int affectedRows = db.Delete<T>(pk);
                }
        }

        public void DeleteOne(int pk)
        {
            lock (DbLock)
                using (var db = GetConnection())
                {
                    int affectedRows = db.Delete<T>(pk);
                }
        }

        public void DeleteAll()
        {
            lock (DbLock)
                using (var db = GetConnection())
                {
                    db.DeleteAll<T>();
                }
        }

        public void DropTable()
        {
            lock (DbLock)
                using (var db = GetConnection())
                {
                    db.DropTable<T>();
                }
        }

        public int CountReg()
        {
            lock (DbLock)
                using (var db = GetConnection())
                {
                    return db.Table<T>().Count();
                }
        }

        public T SelectOne(Expression<Func<T, bool>> predExpr = null)
        {
            try
            {
                TableQuery<T> query;
                lock (DbLock)
                    using (var db = GetConnection())
                    {
                        query = db.Table<T>();

                        if (predExpr != null)
                            query = query.Where(predExpr);

                        return query.FirstOrDefault();
                    }
            }
            catch (Exception ex)
            {
                return default(T);
            }
        }

        public List<T> SelectAll(Expression<Func<T, bool>> predExpr = null)
        {
            try
            {
                TableQuery<T> query;
                lock (DbLock)
                    using (var db = GetConnection())
                    {
                        query = db.Table<T>();

                        if (predExpr != null)
                            query = query.Where(predExpr);

                        return query.ToList();
                    }
            }
            catch (Exception ex)
            {
                return new List<T>();
            }
        }

    }
}
