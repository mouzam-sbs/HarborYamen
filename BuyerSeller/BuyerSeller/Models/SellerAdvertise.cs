namespace BuyerSeller.Models
{ 
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SellerAdvertise")]
    public partial class SellerAdvertise
    {
        public int Id { get; set; }

        public long? BrandId { get; set; }

        public long? ModelId { get; set; }

        public long? CategoryId { get; set; }

        [StringLength(150)]
        public string Name { get; set; }

        public decimal? ActualPrice { get; set; }

        public decimal? DiscountPrice { get; set; }

        public long? UserId { get; set; }

        public byte[] Image { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? FromDate { get; set; }

        public DateTime? ToDate { get; set; }

        public bool? Status { get; set; }

        public virtual Brand Brand { get; set; }

        public virtual Category Category { get; set; }

        public virtual Model Model { get; set; }

        public virtual User User { get; set; }
    }
}
