using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using sahalBuyerSeller;
using BuyerSeller.Models;

namespace BuyerSeller.Controllers
{
    public class ModelsController : BaseController
    {
        private BuyerSellerModel db = new BuyerSellerModel();

        // GET: Models
        public ActionResult ModelIndex()
        {
           
            return View(db.Models.ToList());
        }

        // GET: Models/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Model model = db.Models.Find(id);
            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        // GET: Models/Create
        public ActionResult Create()
        {
            var brands = db.Brands.ToList();
            ViewBag.Brands = brands.Select(x => new SelectListItem {
                Text=x.Name,
                Value=Convert.ToString(x.ID)
            });
            return View();
        }

        // POST: Models/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,BrandID")] Model model)
        {
            if (ModelState.IsValid)
            {
                model.CreatedOn = DateTime.Now;
                db.Models.Add(model);
                db.SaveChanges();
                return RedirectToAction("ModelIndex");
            }

            return View(model);
        }

        // GET: Models/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Model model = db.Models.Find(id);
            if (model == null)
            {
                return HttpNotFound();
            }
            var brands = db.Brands.ToList();
            ViewBag.Brands = brands.Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = Convert.ToString(x.ID),
                Selected = model.BrandID == x.ID ? true : false,
            });
            return View(model);
        }

        // POST: Models/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,BrandID")] Model model)
        {
            if (ModelState.IsValid)
            {
                db.Entry(model).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ModelIndex");
            }
            return View(model);
        }

        // GET: Models/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Model model = db.Models.Find(id);
            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        // POST: Models/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Model model = db.Models.Find(id);
            db.Models.Remove(model);
            db.SaveChanges();
            return RedirectToAction("ModelIndex");
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
