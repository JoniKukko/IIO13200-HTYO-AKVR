using System.Collections.Generic;


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


        // Nothing special to do, passing call to mapper
        public List<ReasonCodeModel> SelectAll()
        {
            return this.Mapper.SelectAll();
        }

    }
}