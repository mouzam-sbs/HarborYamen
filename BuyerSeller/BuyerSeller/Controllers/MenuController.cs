using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BuyerSeller.Models;

namespace BuyerSeller.Controllers
{
    public class MenuController : BaseController
    {
        // GET: Menu
        private BuyerSellerModel db = new BuyerSellerModel();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Menu()
        {
            
           Role_PermissionMapping _PermissionRoleMapping = new Role_PermissionMapping();

            long RoleId = Convert.ToInt64(Session["Role"]);
            _PermissionRoleMapping.RoleId = (RoleId != 0) ? Convert.ToInt64(RoleId) : 0;
            List<Role_PermissionMapping> _list = db.Role_PermissionMapping.Where(x => x.RoleId == RoleId).ToList();
            return PartialView("_Menu", _list);
        }
    }
}