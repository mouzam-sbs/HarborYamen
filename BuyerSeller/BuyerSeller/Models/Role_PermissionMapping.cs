namespace BuyerSeller.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Role_PermissionMapping
    {
        public int Id { get; set; }

        public int? PermissionId { get; set; }

        public long? RoleId { get; set; }

        public DateTime? CreatedDate { get; set; }

        public virtual Permission Permission { get; set; }

        public virtual Role Role { get; set; }
    }
}
