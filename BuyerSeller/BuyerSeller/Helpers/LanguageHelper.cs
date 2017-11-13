using BuyerSeller.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web;
using System.Web.Routing;

namespace BuyerSeller.Helpers
{
    public abstract class LanguageHelper<T> : WebViewPage<T>
    {
        private BuyerSellerModel db = new BuyerSellerModel();



        public List<LanguageForm> GetListKeyValue()
        {
            //var language = (ILanguageService)
            //    DependencyResolver.Current.GetService(typeof(ILanguageService));
            //return  language.GetKeyValueList();

            var langage = db.LanguageForms.ToList();
            return langage;
        }

        public string GetValueByKey(string key)
        {

            string cultureName = Url.RequestContext.RouteData.Values["culture"] as string;
            var languageID = string.Equals(cultureName, "en-us", StringComparison.CurrentCultureIgnoreCase) ? 1 : 2;
            return GetListKeyValue().Where(x => x.Key == key && x.LanguageId == languageID).Select(x => x.Value).FirstOrDefault();
        }
        //public string GetValueByKey(string key)
        //{
        //    return string.Empty;
        //}
    }
}