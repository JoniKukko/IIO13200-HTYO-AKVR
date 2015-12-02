using System.Collections.Generic;


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

        

        // Nothing special to do, passing call to mapper
        public List<ReasonCategoryModel> SelectAll()
        {
            return this.Mapper.SelectAll();
        }

    }
}