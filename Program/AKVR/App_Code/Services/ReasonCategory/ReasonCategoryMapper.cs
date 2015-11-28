using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;


namespace AKVR.Services.ReasonCategory
{
    public class ReasonCategoryMapper : BaseMapper
    {
        
        public List<ReasonCategoryModel> SelectAll()
        {
            List<ReasonCategoryModel> ReasonCategorys;
            
            try
            {
                string json = this.getJSON("metadata/cause-category-codes", true, 3600);
                ReasonCategorys = JsonConvert.DeserializeObject<List<ReasonCategoryModel>>(json);
            }
            catch (Exception ex)
            {
                ReasonCategorys = new List<ReasonCategoryModel>();
                Debug.WriteLine("AKVR:SelectAll FAILED: " + ex.Message);
            }
            
            return ReasonCategorys;
        }

    }
}