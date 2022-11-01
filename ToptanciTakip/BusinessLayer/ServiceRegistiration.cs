using BusinessLayer.Abstract;
using BusinessLayer.Concrete;
using DataAccessLayer.Abstract;
using DataAccessLayer.EntityFramework;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public static class ServiceRegistiration
    {
        public static void AddServiceRouting(this IServiceCollection services)
        {
            services.AddScoped<IProductService, ProductManager>();
            services.AddScoped<IProductDal, EfProductRepository>();

            services.AddScoped<ICategoryService, CategoryManager>();
            services.AddScoped<ICategoryDal, EfCategoryRepository>();

            services.AddScoped<IStockService, StockManager>();
            services.AddScoped<IStockDal, EfStockRepository>();

            services.AddScoped<IStockCategoryService, StockCategoryManager>();
            services.AddScoped<IStockCategoryDal, EfStockCategoryRepository>();

            services.AddScoped<ISucribeMailService, SucribeMailManager>();
            services.AddScoped<ISucribeMailDal, EfSucribeMailRepository>();

            services.AddScoped<IProductRequestService, ProductRequestManager>();
            services.AddScoped<IProductRequestDal, EfProductRequestRepository>();

        }
    }
}
