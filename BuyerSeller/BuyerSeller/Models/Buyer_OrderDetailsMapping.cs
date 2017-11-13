namespace BuyerSeller.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Buyer_OrderDetailsMapping
    {
        public long ID { get; set; }

        public long? OrderDetailID { get; set; }

        public long? BuyerID { get; set; }

        public DateTime? CreatedOn { get; set; }

        public long? CreatedBy { get; set; }

        public virtual OrderDetail OrderDetail { get; set; }
    }
}
