using BuyerSeller.Common;
using BuyerSeller.Helpers;
using BuyerSeller.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BuyerSeller.Controllers
{
    public class DashBoardController : BaseController
    {
        private BuyerSellerModel db = new BuyerSellerModel();

        public ActionResult Index()
        {
            var userID = Convert.ToInt64(Session["User"]);
            ViewBag.PostedOrders = db.Orders.Count();
            ViewBag.NewOrder = db.Orders.Where(x => x.Status == 1).Count();
            ViewBag.Sellers = db.Users.Where(x=>x.RoleID==3).Count();
            ViewBag.Buyers= db.Users.Where(x => x.RoleID == 2).Count();
            return View();
        }

        [HttpGet]
        public ActionResult Request()
        {
            List<Int64> orderIds = new List<long>();
            var userID = Convert.ToInt64(Session["User"]);
           // var brandId = db.Users.Find(userID).BrandId;
            var orders = db.Orders.Where(x => x.Status == 1).ToList();
            orders.ForEach(x =>
            {
                var orderdetails = x.OrderDetails;
                if (!orderdetails.Any(z => z.SellerID == userID))
                {
                    orderIds.Add(x.ID);
                    
                }
            });
            
            ViewBag.CountryList = db.Countries.ToList();
            ViewBag.BrandList = db.Brands.ToList();
            ViewBag.ModelList = db.Models.ToList();
            ViewBag.CategoryList = db.Categories.ToList();
            return View(orders.Where(x => orderIds.Contains(x.ID)).ToList());

        }


        [HttpGet]
        public ActionResult SellerRequestFilerMyBrand()
        {

            List<Int64> orderIds = new List<long>();
            var userID = Convert.ToInt64(Session["User"]);
            var BrandId = db.Users.Where(x => x.ID == userID).FirstOrDefault().BrandId;
            var orders = db.Orders.Where(x => x.Status == 1&&x.BrandID== BrandId).ToList();
            orders.ForEach(x =>
            {
                var orderdetails = x.OrderDetails;
                if (!orderdetails.Any(z => z.SellerID == userID))
                {
                    orderIds.Add(x.ID);
                }
            });
            
            orders = orders.Where(x => orderIds.Contains(x.ID)).ToList();
            return PartialView("_BuyerRequestList", orders);

        }


        [HttpGet]
        public ActionResult SellerRequestFiler(MultiSerachModel model)
        {
            List<Int64> orderIds = new List<long>();
            var userID = Convert.ToInt64(Session["User"]);

            var orders = db.Orders.Where(x => x.Status == 1).AsQueryable().ToList();
            orders.ForEach(x =>
            {
                var orderdetails = x.OrderDetails;
                if (!orderdetails.Any(z => z.SellerID == userID))
                {
                    orderIds.Add(x.ID);
                }
            });
            if (model.CountryId != 0)
            {
                orders = orders.AsQueryable().Where(x => x.Brand.CountryId == model.CountryId).ToList();
            }
            if (model.BrandId != 0)
            {
                orders = orders.AsQueryable().Where(x => x.BrandID == model.BrandId).ToList();
            }

            if (model.ModelId != 0)
            {
                orders = orders.AsQueryable().Where(x => x.ModelID == model.ModelId).ToList();
            }
            if (model.CategoryId != 0)
            {
                orders = orders.AsQueryable().Where(x => x.CategoryID == model.CategoryId).ToList();
            }
            orders = orders.Where(x => orderIds.Contains(x.ID)).ToList();
            return PartialView("_BuyerRequestList", orders);

        }

        [HttpPost]
        public ActionResult Request(HttpPostedFileBase file, FormCollection form)
        {
            OrderDetail orderDetails = new OrderDetail();
           orderDetails.OrderID = Convert.ToUInt32(form["itemid"]);
            orderDetails.Price = Convert.ToUInt32(form["price"]);
            if (file != null)
            {
                var ext = Path.GetExtension(file.FileName);
                Random number = new Random();
                string bannerPath = "images" + number.Next(1000000000) + ext;
                string path = Server.MapPath("~/ProductImages/" + bannerPath);
                file.SaveAs(path);
                orderDetails.Image = bannerPath;
            }
            var order = db.Orders.Where(x => x.ID == orderDetails.OrderID).FirstOrDefault();
            orderDetails.TotalPrice = order.Qty * orderDetails.Price;
            orderDetails.SellerID = Convert.ToInt64(Session["User"]);
            var userDetails = db.Users.Where(x => x.ID == order.CreatedBy).FirstOrDefault();
           string res= new EmailNotification().SendEmail(userDetails.EmailID, userDetails.Name,orderDetails.OrderID.ToString());
            if (res!=null)
            {
                Notification obj = new Notification();
                obj.Message = res;
                obj.UserId = order.CreatedBy;
                obj.CreatedDate = DateTime.Now;
                db.Notifications.Add(obj);
                db.SaveChanges();
            }

            db.OrderDetails.Add(orderDetails);
            db.SaveChanges();
            return RedirectToAction("Request");

        }

        public ActionResult PostedOrders()
        {
            var userID = Convert.ToInt64(Session["User"]);
            var completedOrders = db.OrderDetails.Where(x => x.SellerID == userID).ToList();
            return View(completedOrders);
        }
    }
}