using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Entities
{
    public class Stock
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        //public string Description { get; set; }
        public string Quantity { get; set; }
        public string Price { get; set; }
        //public string ImagePath { get; set; }
        //[NotMapped]
        //public IFormFile Image { get; set; }
        public bool Status { get; set; }
        public int StocKCategoryId { get; set; }
        public StockCategory StockCategory { get; set; }
        public int AppUserId { get; set; }
        public AppUser AppUser { get; set; }
    }
}
