using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;


namespace AKVR.Services.TrainType
{
    public class TrainTypeService
    {
        // from here...
        private TrainTypeMapper Mapper { get; set; }

        public TrainTypeService(TrainTypeMapper mapper)
        {
            this.Mapper  = mapper;
        }
        // ...to here should be in every service class



        public TrainTypeModel SelectByName(string name)
        {
            TrainTypeModel TrainType = null;
            List<TrainTypeModel> list = this.Mapper.SelectAll();
            TrainType = list.Find(m => m.name == name);

            if (TrainType == null)
            {
                Debug.WriteLine("TrainType not found with full name - trying with \"StartsWith\"");
                TrainType = list.Find(m => m.name.StartsWith(name));
            } else
            {
                Debug.WriteLine("TrainType found with full name");
            }

            return (TrainType == null)
                ? new TrainTypeModel()
                : TrainType;
        }



        public List<TrainTypeModel> SelectListByName(string name)
        {
            List<TrainTypeModel> TrainTypes = this.Mapper.SelectAll();
            return TrainTypes.FindAll(m => m.name.StartsWith(name));
        }



        public List<TrainTypeModel> SelectAll()
        {
            return this.Mapper.SelectAll();
        }

    }
}