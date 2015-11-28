using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;


namespace AKVR.Services.ReasonCode
{
    public class ReasonCodeService
    {
        // from here...
        private ReasonCodeMapper Mapper { get; set; }

        public ReasonCodeService(ReasonCodeMapper mapper)
        {
            this.Mapper  = mapper;
        }
        // ...to here should be in every service class


        public List<ReasonCodeModel> SelectAll()
        {
            return this.Mapper.SelectAll();
        }

    }
}