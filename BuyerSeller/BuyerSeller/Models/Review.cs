namespace BuyerSeller.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Review
    {
        public int Id { get; set; }

        public decimal? Rating { get; set; }

        public string Comment { get; set; }

        public long? UserId { get; set; }

        public DateTime? CreatedOn { get; set; }

        public long SellerId { get; set; }

        public virtual User User { get; set; }
    }
}
