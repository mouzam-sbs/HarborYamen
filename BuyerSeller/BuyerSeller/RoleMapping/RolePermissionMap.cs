using BuyerSeller.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BuyerSeller.RoleMapping
{
    public class RolePermissionMap
    {
        BuyerSellerModel db = new BuyerSellerModel();
        public List<Permission> PermissionList()
        {
            List<Permission> _List = new List<Permission>();
            var List = db.Permissions.ToList();
             
            return _List;
        }

        public List<Role_PermissionMapping> RolePermissionMappedList()
        {
            List<Role_PermissionMapping> _List = new List<Role_PermissionMapping>();
            var List = db.Role_PermissionMapping.ToList();             
            return _List;
        }
        public List<Role_PermissionMapping> RolePermissionMappedListById(Role_PermissionMapping model)
        {
            List<Role_PermissionMapping> _List = new List<Role_PermissionMapping>();
            var List = db.Role_PermissionMapping.Where(x=>x.RoleId==model.RoleId).ToList();
            
            return _List;
        }
        //public int Save(PermissionModel model)
        //{
        //    Permission obj = new Permission(model);
        //    if (model.Id != 0)
        //    {
        //        _Permission.Update(obj);

        //    }
        //    else
        //    {
        //        obj = _Permission.Insert(obj);
        //    }

        //    return obj.Id;
        //}

        //public int SavePermissions(RolePermissionMappingModel model)
        //{
        //    Role_PermissionMapping obj = new Role_PermissionMapping(model);
        //    if (model != null)
        //    {
        //        obj = _PermissionRoleMapping.Insert(obj);

        //    }
        //    return obj.Id;
        //}

        public void DeleteAllPermissions(int RoleId)
        {
            var res = db.Role_PermissionMapping.Where(x => x.RoleId == RoleId).ToList();

            if (res != null)
            {
                foreach (var item in res)
                {
                    db.Role_PermissionMapping.Remove(item);
                    db.SaveChanges();

                }
            }
        }
    }
}