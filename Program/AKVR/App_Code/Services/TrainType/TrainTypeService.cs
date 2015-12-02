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



        // Haetaan yksittäinen malli nimen perusteella
        public TrainTypeModel SelectByName(string name)
        {
            // palautusarvo
            TrainTypeModel TrainType = null;

            // haetaan listana kaikki ja etsitään kokonaista nimeä vastaava malli
            List<TrainTypeModel> list = this.Mapper.SelectAll();
            TrainType = list.Find(m => m.name == name);

            // jos kokonaisella nimellä ei löydy niin koetetaan nimen alulla
            if (TrainType == null)
            {
                Debug.WriteLine("TrainType not found with full name - trying with \"StartsWith\"");
                TrainType = list.Find(m => m.name.StartsWith(name));
            } else
            {
                Debug.WriteLine("TrainType found with full name");
            }

            // jos nimen alullakaan ei löydy niin palautetaan ainakin tyhjä
            return (TrainType == null)
                ? new TrainTypeModel()
                : TrainType;
        }


        // haetaan kaikki mallit listana mitkä alkaa annetulla nimellä
        public List<TrainTypeModel> SelectListByName(string name)
        {
            List<TrainTypeModel> TrainTypes = this.Mapper.SelectAll();
            return TrainTypes.FindAll(m => m.name.StartsWith(name));
        }


        // Nothing special to do, passing call to mapper
        public List<TrainTypeModel> SelectAll()
        {
            return this.Mapper.SelectAll();
        }

    }
}