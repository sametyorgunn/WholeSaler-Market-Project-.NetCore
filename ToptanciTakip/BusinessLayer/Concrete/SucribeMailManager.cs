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
	public class SucribeMailManager : ISucribeMailService
	{
		ISucribeMailDal _sucribeMailDal;

		public SucribeMailManager(ISucribeMailDal sucribeMailDal)
		{
			_sucribeMailDal = sucribeMailDal;
		}

		public List<SucribeMail> GetList()
		{
			return _sucribeMailDal.GetListAll();
		}

        public List<SucribeMail> GetListAll(Expression<Func<SucribeMail, bool>> filter)
        {
			return _sucribeMailDal.GetListAll(filter);
		}

        public void TAdd(SucribeMail t)
		{
			_sucribeMailDal.Insert(t);
		}

		public void TDelete(SucribeMail t)
		{
			_sucribeMailDal.Delete(t);
		}

		public SucribeMail TGetById(int id)
		{
			return _sucribeMailDal.GetById(id);
		}

		public void TUpdate(SucribeMail t)
		{
			_sucribeMailDal.Update(t);
		}
	}
}
