namespace BuyerSeller.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class OrderDetail
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public OrderDetail()
        {
            Buyer_OrderDetailsMapping = new HashSet<Buyer_OrderDetailsMapping>();
        }

        public long ID { get; set; }

        public long? OrderID { get; set; }

        public decimal? Price { get; set; }

        [StringLength(500)]
        public string Image { get; set; }

        public long? SellerID { get; set; }

        public decimal? TotalPrice { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Buyer_OrderDetailsMapping> Buyer_OrderDetailsMapping { get; set; }

        public virtual Order Order { get; set; }

        public virtual User User { get; set; }
    }
}
