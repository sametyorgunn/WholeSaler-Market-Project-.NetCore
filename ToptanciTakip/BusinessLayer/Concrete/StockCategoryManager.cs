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
    public class StockCategoryManager : IStockCategoryService
    {
        IStockCategoryDal _stockCategoryDal;

        public StockCategoryManager(IStockCategoryDal stockCategoryDal)
        {
            _stockCategoryDal = stockCategoryDal;
        }

        public List<StockCategory> GetList()
        {
            return _stockCategoryDal.GetListAll();
        }

        public List<StockCategory> GetListAll(Expression<Func<StockCategory, bool>> filter)
        {
            return _stockCategoryDal.GetListAll(filter);
        }

        public void TAdd(StockCategory t)
        {
            _stockCategoryDal.Insert(t);
        }

        public void TDelete(StockCategory t)
        {
            _stockCategoryDal.Delete(t);
        }

        public StockCategory TGetById(int id)
        {
            return _stockCategoryDal.GetById(id);
        }

        public void TUpdate(StockCategory t)
        {
            _stockCategoryDal.Update(t);
        }
    }
}
