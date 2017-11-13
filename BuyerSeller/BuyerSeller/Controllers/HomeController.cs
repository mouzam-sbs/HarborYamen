using BuyerSeller.Helpers;
using BuyerSeller.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace BuyerSeller.Controllers
{
    public class HomeController : BaseController
    { 
        private BuyerSellerModel db = new BuyerSellerModel();

        public ActionResult Index()
        {
            var userID = Convert.ToInt64(Session["User"]);
            var orders = db.Orders.Where(x => x.CreatedBy == userID).ToList();
            if (orders == null)
                return View();
            ViewBag.CompletedOrders = orders.Count(x => x.Status == 2);
            ViewBag.PostedOrders = orders.Count(x => x.Status == 1);
            ViewBag.OffersList = db.SellerAdvertises.ToList();
            return View();
        }

        public ActionResult Main()
        {
            return View();
        }

        public ActionResult Request()
        {
            var brands = db.Brands.ToList();
            ViewBag.Brands = brands;
            //ViewBag.Brands = brands.Select(x => new SelectListItem
            //{
            //    Text = x.Name,
            //    Value = Convert.ToString(x.ID)
            //});
            var category = db.Categories.ToList();
            ViewBag.Category = category;
            //ViewBag.Category = category.Select(x => new SelectListItem
            //{
            //    Text = x.Name,
            //    Value = Convert.ToString(x.ID)
            //});
            return View();
        }

        [HttpPost]
        public ActionResult Request(FormCollection form)
        {
            var keys = form.AllKeys.Where(x => x.StartsWith("BrandID")).ToList();

            Order obj = new Order();
            foreach (var item in keys)
            {
                var currentKeyNum = item.Replace("BrandID", "");
                if (currentKeyNum.Contains("IncrementNumber"))
                {
                    
                }
                else
                {
                    obj.BrandID = Convert.ToUInt32(form["BrandID" + currentKeyNum]);
                    obj.ModelID = Convert.ToUInt32(form["ModelID" + currentKeyNum]);
                    obj.CategoryID = Convert.ToUInt32(form["CategoryID" + currentKeyNum]);
                    obj.PartName = form["Name" + currentKeyNum];
                    obj.Qty = Convert.ToInt32(form["Qty" + currentKeyNum]);
                    obj.Status = 1;
                    obj.CreatedBy = Convert.ToInt64(Session["User"]);
                    obj.Createdon = DateTime.Now;


                    db.Orders.Add(obj);
                    db.SaveChanges();
                }
            }

            return RedirectToAction("Index");
        }

        public ActionResult RequestedOrders()
        {
            var userID = Convert.ToInt64(Session["User"]);
            return View(db.Orders.Where(x => x.CreatedBy == userID && x.Status == 1).ToList());
        }

        public ActionResult BuyerRegistration()
        {
            return View();
        }

        public ActionResult BuyersList()
        {
            return View(db.Users.Where(x=>x.RoleID==2).ToList().OrderByDescending(x=>x.ID));
        }

        public ActionResult CompletedOrders()
        {
            var userID = Convert.ToInt64(Session["User"]);
            var completedOrdersDetailsID = db.Buyer_OrderDetailsMapping.Where(x => x.BuyerID == userID).Select(x => x.OrderDetailID).ToList();
            var completedOrders = db.OrderDetails.Where(x => completedOrdersDetailsID.Contains(x.ID)).ToList();
            ViewBag.CompletedOrders = completedOrders;
           
            return View();
        }

        public JsonResult ModelByBrandID(Int64 id)
        {
            var modelsByBrandID = db.Models.Where(x => x.BrandID == id).Select(x => new
            {

                id = x.ID,
                name = x.Name,
            }).ToList();
            return Json(modelsByBrandID, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult BrandByCountryId(int id)
        {
            var modelsByBrandID = db.Brands.Where(x => x.CountryId == id).Select(x => new
            {

                id = x.ID,
                name = x.Name,
            }).ToList();
            return Json(modelsByBrandID, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Sellers(Int64 id)
        {
            var orderDetails = db.OrderDetails.Where(x => x.OrderID == id).ToList();
            return View(orderDetails);
        }

        public ActionResult ViewOrderDetails(OrderDetail model)
        {
            var orderDetail = db.OrderDetails.Where(x => x.ID == model.ID).FirstOrDefault();
            return PartialView("_OrderViewDetails", orderDetail);
        }

        public ActionResult SellersById(Int64 id)
        {
            var orderDetails = db.OrderDetails.Where(x => x.ID == id).ToList();
            ViewBag.orderDetails = orderDetails;
            return View();
        }

        public ActionResult CompleteOrder(Int64 id)
        {
            var userID = Convert.ToInt64(Session["User"]);
            var orderDetail = db.OrderDetails.Where(x => x.ID == id).FirstOrDefault();
            var order = orderDetail.Order;
            var completeOrder = new Buyer_OrderDetailsMapping
            {

                OrderDetailID = id,
                BuyerID = userID,
                CreatedOn = DateTime.Now                
            };
            order.Status = 2;
            order.UpdatedOn = DateTime.Now;
            order.Updatedby = userID;
            db.Buyer_OrderDetailsMapping.Add(completeOrder);
            db.SaveChanges();
            
            return RedirectToAction("ReviewSeller",new { sellerId= orderDetail.SellerID });
        }

        public ActionResult ReviewSeller(long sellerId)
        {
            ViewBag.SellerId = sellerId;
            return View();
        }

        public ActionResult SellerReview(OrderDetailModel model)
        {
            return PartialView("_SellerReview", model);
        }

        public ActionResult SellerReviewSave(OrderDetailModel model)
        {
            var userID = Convert.ToInt64(Session["User"]);
            Review review = new Review();
            review.Rating = model.Rating;
            review.Comment = model.Comment;
            review.UserId = userID;
            review.SellerId = model.SellerId;
            review.CreatedOn = DateTime.Now;
            db.Reviews.Add(review);
            if (model != null && model.Id == 0)
            {
                db.SaveChanges();
            }
            else if (model != null && model.Id != 0)
            {
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetLatLong(long SellerId)
        {
            UserLatLong res = new UserLatLong();
            res.lat = db.Users.Where(x => x.ID == SellerId).FirstOrDefault().Latitude;
            res.Long = db.Users.Where(x => x.ID == SellerId).FirstOrDefault().Longitude;


            return Json(res, JsonRequestBehavior.AllowGet);
        }

        class UserLatLong
        {
            public string lat { get; set; }
            public string Long { get; set; }
        }

        [HttpGet]
        public ActionResult GetBuyerDetailsById(long UserId)
        {
            var res = db.Users.Find(UserId);

            return PartialView("_getBuyerDetailsId", res);
        }
    }
}