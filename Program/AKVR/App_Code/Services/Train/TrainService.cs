using System;
using System.Collections.Generic;
using System.Diagnostics;


namespace AKVR.Services.Train
{


    public class TrainService
    {
        // from here...
        private TrainMapper Mapper { get; set; }

        public TrainService(TrainMapper mapper)
        {
            this.Mapper  = mapper;
        }
        // ...to here should be in every service class





        // nothing special to do..
        // passing method call to mapper
        public TrainModel SelectByTrainNumber(int trainNumber)
        {
            return this.Mapper.SelectByTrainNumber(trainNumber);
        }
        


        // nothing special to do..
        // passing method call to mapper
        public List<TrainModel> SelectAll()
        {
            return this.Mapper.SelectAll();
        }



        public List<TrainModel> SelectByStationShortCode(string shortcode, string dateTime = "dd.mm.yyyy hh:ii:ss",  int arrived_trains = 5, int arriving_trains = 5, int departed_trains = 5, int departing_trains = 5)
        {
            return this.Mapper.SelectByStationShortCode(shortcode, arrived_trains, arriving_trains, departed_trains, departing_trains);
        }



    }


}
