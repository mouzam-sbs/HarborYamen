using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BuyerSeller.Common
{
    public class LanguageFormModel
    {
        public LanguageFormModel()
        {
            LanguageList = new List<LanguageFormModel>();
        }
        public long ID { get; set; }
        public string Value { get; set; }
        public string ArabicValue { get; set; }
        public string Key { get; set; }
        public int LanguageId { get; set; }
        public DateTime? CreatedOn { get; set; }
        public long? CreatedBy { get; set; }
        public bool? IsActive { get; set; }

        public List<LanguageFormModel> LanguageList { get; set; }
    }
}