using BuyerSeller.Common;
using BuyerSeller.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BuyerSeller.Controllers
{
    public class LangController : BaseController
    {
        private BuyerSellerModel db = new BuyerSellerModel();
        // GET: Lang
        public ActionResult Index()
        {
            List<LanguageFormModel> _list = GetAlllanguageKeywords();
            return View(_list);
        }
        public ActionResult UpdateLang(LanguageForm model)
        {
            db.Entry(model).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public List<LanguageFormModel> GetAlllanguageKeywords()
        {
            var res = db.LanguageForms.ToList();
            List<LanguageFormModel> _list = new List<LanguageFormModel>();
            _list = (from item in res
                     select new LanguageFormModel
                     {
                         Key = item.Key,
                         Value = item.Value,
                         LanguageId = Convert.ToInt32(item.LanguageId),
                         ArabicValue = GetLangValue(2, item.Key),
                         ID = item.ID
                     }).Where(x => x.LanguageId == 1).ToList();
            return _list;
        }

        public string GetLangValue(int langId, string key)
        {
            string res = db.LanguageForms.Where(x => x.Key == key && x.LanguageId == langId).FirstOrDefault().Value;
            return res;
        }
    }
}
