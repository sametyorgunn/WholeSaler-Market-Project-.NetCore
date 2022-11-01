using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class StockManager : IStockService
    {
        IStockDal _stockDal;

        public StockManager(IStockDal stockDal)
        {
            _stockDal = stockDal;
        }

        public List<Stock> GetList()
        {
            return _stockDal.GetListAll();
        }

        public List<Stock> GetListAll(Expression<Func<Stock, bool>> filter)
        {
            return _stockDal.GetListAll(filter);
        }

        public void TAdd(Stock t)
        {
            _stockDal.Insert(t);
        }

        public void TDelete(Stock t)
        {
            _stockDal.Delete(t);
        }

        public Stock TGetById(int id)
        {
            return _stockDal.GetById(id);
        }

        public void TUpdate(Stock t)
        {
            _stockDal.Update(t);
        }
    }
}
