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
    public class ProductRequestManager : IProductRequestService
    {
        IProductRequestDal _productRequestDal;

        public ProductRequestManager(IProductRequestDal productRequestDal)
        {
            _productRequestDal = productRequestDal;
        }

        public List<ProductRequests> GetList()
        {
            return _productRequestDal.GetListAll();
        }

        public List<ProductRequests> GetListAll(Expression<Func<ProductRequests, bool>> filter)
        {
          return  _productRequestDal.GetListAll(filter);
        }

        public void TAdd(ProductRequests t)
        {
            _productRequestDal.Insert(t);
        }

        public void TDelete(ProductRequests t)
        {
            _productRequestDal.Delete(t);
        }

        public ProductRequests TGetById(int id)
        {
            return _productRequestDal.GetById(id);
        }

        public void TUpdate(ProductRequests t)
        {
            _productRequestDal.Update(t);
        }
    }
}
