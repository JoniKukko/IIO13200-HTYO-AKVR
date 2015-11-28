using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;


namespace AKVR.Services.ReasonCategory
{
    public class ReasonCategoryService
    {
        // from here...
        private ReasonCategoryMapper Mapper { get; set; }

        public ReasonCategoryService(ReasonCategoryMapper mapper)
        {
            this.Mapper  = mapper;
        }
        // ...to here should be in every service class


        public List<ReasonCategoryModel> SelectAll()
        {
            return this.Mapper.SelectAll();
        }

    }
}