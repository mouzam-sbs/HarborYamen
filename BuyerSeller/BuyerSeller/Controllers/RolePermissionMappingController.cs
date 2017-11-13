using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BuyerSeller.Models;
using BuyerSeller.RoleMapping;
using System.Web.Mvc.Html;
using BuyerSeller.Common;

using BOLHelpers.Enums;

namespace BuyerSeller.Controllers
{
    public class RolePermissionMappingController : BaseController
    {
        private BuyerSellerModel db = new BuyerSellerModel();
        private RolePermissionMappingModel _rolepermissionmapping;
        // GET: RolePermissionMapping

        public RolePermissionMappingController()
        {
            _rolepermissionmapping = new RolePermissionMappingModel();
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Role_PermissionMapping(int? RoleId)
        {
            if (TempData["message"] != null)
            {
                TempData["msg"] = TempData["message"];
            }
        ViewBag.RoleList = db.Roles.ToList();
            //List<Role_PermissionMapping> obj = new RolePermissionMap().RolePermissionMappedList();

            return View();
        }

        public ActionResult GetPermission(int? RoleId, Role_PermissionMapping model)
        {

            if (RoleId != null && RoleId != 0)
            {
                _rolepermissionmapping.PermissionRoleMappingList = db.Role_PermissionMapping.Where(x => x.RoleId == RoleId).ToList();

            }
            var enumList = EnumHelper.GetSelectList(typeof(Permissionsss));
            if (enumList != null && enumList.Any())
            {
                _rolepermissionmapping.PermissionList = (from @item in enumList
                                                         select new Permission
                                                         {
                                                             Id = Convert.ToInt32(@item.Value),
                                                             Name = @item.Text,
                                                             Status = _rolepermissionmapping.PermissionRoleMappingList == null ? false : _rolepermissionmapping.PermissionRoleMappingList.Where(m => m.PermissionId == Convert.ToInt32(@item.Value)).Any()
                                                         }).ToList();
            }
            return PartialView("_PermissionsPartial", _rolepermissionmapping);
        }

        public ActionResult SavePermissions(Role_PermissionMapping model, List<int> CheckedValues)
        {
            var res = db.Role_PermissionMapping.Where(x=>x.RoleId==model.RoleId).ToList();
            foreach (var item in res)
            {
                db.Role_PermissionMapping.Remove(item);
                db.SaveChanges();
            }
           
            foreach (var item in CheckedValues)
            {
                model.PermissionId = item;
                Role_PermissionMapping obj = new Models.Role_PermissionMapping();
                obj.RoleId = model.RoleId;
                obj.PermissionId = item;
                obj.CreatedDate = DateTime.Now;
                db.Role_PermissionMapping.Add(obj);
                db.SaveChanges();
                TempData["message"] = "Role Permission Updated Successfully";
            }
            return RedirectToAction("Role_PermissionMapping");
        }

    }
}