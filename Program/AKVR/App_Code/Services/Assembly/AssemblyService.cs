using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace AKVR.Services.Assembly
{
    public class AssemblyService
    {
        // from here...
        private AssemblyMapper Mapper { get; set; }

        public AssemblyService(AssemblyMapper mapper)
        {
            this.Mapper  = mapper;
        }
        // ...to here should be in every service class


        public AssemblyModel SelectByTrainNumber(int trainNumber, string date)
        {
            return this.Mapper.SelectByTrainNumber(trainNumber, date);
        }



    }
}