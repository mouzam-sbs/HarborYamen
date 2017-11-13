using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BuyerSeller.Models;

namespace BuyerSeller.Controllers
{
    public class SellerAdvertisesController : BaseController
    {
        private BuyerSellerModel db = new BuyerSellerModel();

        // GET: SellerAdvertises
        public ActionResult Index()
        {
            // var sellerAdvertises = db.SellerAdvertises.Include(s => s.Brand).Include(s => s.Category).Include(s => s.Model);
            var sellerAdvertises = db.SellerAdvertises.ToList().Where(x=>x.UserId==Convert.ToInt32(Session["UserId"]));
            return View(sellerAdvertises.ToList());
        }

        // GET: SellerAdvertises/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SellerAdvertise sellerAdvertise = db.SellerAdvertises.Find(id);
            if (sellerAdvertise == null)
            {
                return HttpNotFound();
            }
            return View(sellerAdvertise);
        }

        // GET: SellerAdvertises/Create
        public ActionResult Create()
        {
            ViewBag.BrandId = new SelectList(db.Brands, "ID", "Name");
            ViewBag.CategoryId = new SelectList(db.Categories, "ID", "Name");
            ViewBag.ModelId = new SelectList(db.Models, "ID", "Name");
            return View();
        }

        // POST: SellerAdvertises/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,BrandId,ModelId,CategoryId,Name,ActualPrice,DiscountPrice,SellerId,Image,CreatedDate,FromDate,ToDate,Status")] SellerAdvertise sellerAdvertise,HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                if (file.ContentLength>0&&file!=null)
                {
                     var ImageUpload1 = new byte[file.ContentLength];
                    file.InputStream.Read(ImageUpload1, 0, file.ContentLength);
                    sellerAdvertise.Image = ImageUpload1;
                }
                sellerAdvertise.UserId = Convert.ToInt64(Session["UserId"]);
                sellerAdvertise.Status = true;
                sellerAdvertise.CreatedDate = DateTime.Now;
                db.SellerAdvertises.Add(sellerAdvertise);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BrandId = new SelectList(db.Brands, "ID", "Name", sellerAdvertise.BrandId);
            ViewBag.CategoryId = new SelectList(db.Categories, "ID", "Name", sellerAdvertise.CategoryId);
            ViewBag.ModelId = new SelectList(db.Models, "ID", "Name", sellerAdvertise.ModelId);
            return View(sellerAdvertise);
        }

        // GET: SellerAdvertises/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SellerAdvertise sellerAdvertise = db.SellerAdvertises.Find(id);
            if (sellerAdvertise == null)
            {
                return HttpNotFound();
            }
            ViewBag.BrandId = new SelectList(db.Brands, "ID", "Name", sellerAdvertise.BrandId);
            ViewBag.CategoryId = new SelectList(db.Categories, "ID", "Name", sellerAdvertise.CategoryId);
            ViewBag.ModelId = new SelectList(db.Models, "ID", "Name", sellerAdvertise.ModelId);
            return View(sellerAdvertise);
        }

        // POST: SellerAdvertises/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,BrandId,ModelId,CategoryId,Name,ActualPrice,DiscountPrice,SellerId,Image,CreatedDate,FromDate,ToDate,Status")] SellerAdvertise sellerAdvertise)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sellerAdvertise).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BrandId = new SelectList(db.Brands, "ID", "Name", sellerAdvertise.BrandId);
            ViewBag.CategoryId = new SelectList(db.Categories, "ID", "Name", sellerAdvertise.CategoryId);
            ViewBag.ModelId = new SelectList(db.Models, "ID", "Name", sellerAdvertise.ModelId);
            return View(sellerAdvertise);
        }

        // GET: SellerAdvertises/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SellerAdvertise sellerAdvertise = db.SellerAdvertises.Find(id);
            if (sellerAdvertise == null)
            {
                return HttpNotFound();
            }
            return View(sellerAdvertise);
        }

        // POST: SellerAdvertises/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SellerAdvertise sellerAdvertise = db.SellerAdvertises.Find(id);
            db.SellerAdvertises.Remove(sellerAdvertise);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
