using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BuyerSeller.Models
{
    public class OrderDetailModel: Common
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public decimal Rating { get; set; }
        public string Comment { get; set; }
        public long SellerId { get; set; }
    }
}