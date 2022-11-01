using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Entities
{
    public class ProductRequests
    {
        [Key]
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int WholesalerId { get; set; }
        public int MarketId { get; set; }
        public string MarketName { get; set; }
        public string MarketPhone { get; set; }
        public string MarketAdress { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public string ImagePath { get; set; }
        public bool Status { get; set; }
        public int Price { get; set; }
    }
}
