using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Entities
{
    public class AppUser:IdentityUser<int>
    {
        public string NameSurname { get; set; }
        public string CompanyName { get; set; }
        public bool Status { get; set; }
        public string TaskNo { get; set; }
        public string Adress { get; set; }
        //public List<Category> Categories { get; set; }
        public List<Product> Product { get; set; }
        public List<StockCategory> StockCategories { get; set; }
        public List<Stock> Stock { get; set; }

    }
}
