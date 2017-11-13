namespace BuyerSeller.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Notification
    {
        public int Id { get; set; }

        public string Message { get; set; }

        public long? UserId { get; set; }

        public DateTime? CreatedDate { get; set; }

        public virtual User User { get; set; }
    }
}
