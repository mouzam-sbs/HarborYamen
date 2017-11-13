using BuyerSeller.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BuyerSeller.Common
{
    public class RolePermissionMappingModel
    {
        public int Id { get; set; }

        public int? PermissionId { get; set; }

        public int? RoleId { get; set; }

        public bool Status { get; set; }

        public bool isChecked { get; set; }

        public List<Role> RoleList { get; set; }

        public List<Permission> PermissionList { get; set; }

        public List<Role_PermissionMapping> PermissionRoleMappingList { get; set; }
    }
}