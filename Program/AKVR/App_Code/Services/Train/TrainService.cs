using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace AKVR.Services.Train
{


    public class TrainService
    {
        // from here...
        private TrainMapper Mapper { get; set; }

        public TrainService(TrainMapper mapper)
        {
            this.Mapper = mapper;
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



        public List<TrainModel> SelectByStationShortCode(string shortcode, string dateTime = "dd.mm.yyyy hh:ii:ss", int arrived_trains = 5, int arriving_trains = 5, int departed_trains = 5, int departing_trains = 5)
        {
            var trainList = this.Mapper.SelectByStationShortCode(shortcode, arrived_trains, arriving_trains, departed_trains, departing_trains);

            try
            {
                trainList = trainList.OrderBy(
                    train => (train.timeTableRows.Find(
                        row => row.stationShortCode == shortcode
                        ).scheduledTime)
                ).ToList();
            } catch (Exception ex)
            {
                Debug.WriteLine("AKVR: OrderbyShortCode FAILED: " + ex.Message);
            }

            return trainList;
        }


        public List<TrainModel> SelectCausesByDate(DateTime datetime)
        {
            var trainList = this.Mapper.SelectAllFromHistory(datetime);

            trainList = trainList.FindAll(
                train => (train.timeTableRows = train.timeTableRows.FindAll(
                    row => row.causes != null
                    )).Count != 0
                );

            return trainList;
        }


        public List<TrainModel> SelectDelaysBetweenDates(DateTime from, DateTime to)
        {
            List<TrainModel> trainList = new List<TrainModel>();

            for (DateTime date = from; date <= to; date = date.AddDays(1))
            {
                Debug.WriteLine("Date: " + date.ToString("yyyy-MM-dd"));
                trainList.AddRange(this.Mapper.SelectAllFromHistory(date));
            }

            trainList.ForEach(
                train => {
                    train.AverageDelay = (int)train.timeTableRows.Average(
                        row => row.differenceInMinutes
                        );
                    train.MaxDelay = (int)train.timeTableRows.Max(
                        row => row.differenceInMinutes
                        );
                });

            trainList = trainList.OrderByDescending(train => train.AverageDelay).ToList();


            return trainList;
        }




    }


}
