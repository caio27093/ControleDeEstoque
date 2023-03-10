using AppControleEstoque.DAL.Tabelas;
using AppControleEstoque.Regras.Infraestructure;
using AppControleEstoque.Regras.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace AppControleEstoque.DAL.Implementation
{
    public class EstoqueCRUD : IDatabaseCRUD<EstoqueModel>
    {
        private BaseDB<TbEstoque> _baseDB;
        public EstoqueCRUD(string caminhoDB)
        {
            _baseDB = new BaseDB<TbEstoque>(caminhoDB);
        }

        public int CountReg()
        {
            return _baseDB.CountReg();
        }

        public void DeleteAll()
        {
            _baseDB.DeleteAll();
        }

        public void DeleteOne(string pk)
        {
            _baseDB.DeleteOne(pk);
        }

        public void DeleteOne(int pk)
        {
            _baseDB.DeleteOne(pk);
        }

        public void DropTable()
        {
            _baseDB.DropTable();
        }

        public int Insert(EstoqueModel obj)
        {
            return _baseDB.Insert(MapperEstoqueModelParaTbEstoque(obj));
        }

        public int InsertOnly(EstoqueModel obj)
        {
            return _baseDB.InsertOnly(MapperEstoqueModelParaTbEstoque(obj));
        }

        public List<EstoqueModel> SelectAll(Expression<Func<EstoqueModel, bool>> predExpr = null)
        {
            List<EstoqueModel> listCadLanc = MapperListTbEstoqueParaListEstoqueModel(_baseDB.SelectAll());

            IQueryable<EstoqueModel> queriable = listCadLanc.AsQueryable();

            if (predExpr != null)
                queriable = queriable.Where(predExpr);

            return queriable.ToList();
        }

        public EstoqueModel SelectOne(Expression<Func<EstoqueModel, bool>> predExpr = null)
        {
            return SelectAll(predExpr).FirstOrDefault();
        }

        public void Update(EstoqueModel obj)
        {
            _baseDB.Update(MapperEstoqueModelParaTbEstoque(obj));
        }

        #region Mappers

        private EstoqueModel MapperTbEstoqueParaEstoqueModel(TbEstoque objTbEstoque)
        {
            return new EstoqueModel()
            {
                Id = objTbEstoque.Id,
                Nome = objTbEstoque.Nome,
                Quantidade = objTbEstoque.Quantidade
            };
        }

        private TbEstoque MapperEstoqueModelParaTbEstoque(EstoqueModel objEstoqueModel)
        {
            return new TbEstoque()
            {
                Id = objEstoqueModel.Id,
                Nome = objEstoqueModel.Nome,
                Quantidade = objEstoqueModel.Quantidade
            };
        }

        private List<EstoqueModel> MapperListTbEstoqueParaListEstoqueModel(List<TbEstoque> tblCadLancs)
        {
            List<EstoqueModel> retorno = new List<EstoqueModel>();
            foreach (TbEstoque item in tblCadLancs)
                retorno.Add(MapperTbEstoqueParaEstoqueModel(item));

            return retorno;
        }
        #endregion
    }
}
