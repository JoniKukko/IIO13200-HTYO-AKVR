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



        // haetaan aseman lyhenteen perusteella saapuvat, saapuneet, lähtevät ja lähteneet junat
        public List<TrainModel> SelectByStationShortCode(string shortcode, int arrived_trains = 10, int arriving_trains = 10, int departed_trains = 10, int departing_trains = 10)
        {
            // haetaan koko lista
            var trainList = this.Mapper.SelectByStationShortCode(shortcode, arrived_trains, arriving_trains, departed_trains, departing_trains);

            try
            {
                // järjestetään sen mukaan milloin on parametrinä annetun aseman luona
                trainList = trainList.OrderBy(
                    train => (train.timeTableRows.Find(
                        row => row.stationShortCode == shortcode
                        ).scheduledTime)
                ).ToList();


            } catch (Exception ex)
            {
                // Luultavasti linq kaatui
                // trainList on kuitenkin olemassa mapperilta tulleena
                Debug.WriteLine("AKVR: OrderbyShortCode FAILED: " + ex.Message);
            }

            return trainList;
        }



        // haetaan myöhästymisien syyt annetun päivän mukaan
        public List<TrainModel> SelectCausesByDate(DateTime datetime)
        {
            // vr ei tarjoa kuin kokonaisen historian
            var trainList = this.Mapper.SelectAllFromHistory(datetime);

            // joten poistetaan kokonaisesta listasta ne
            // asemat joilla ei ole myöhästymissyytä
            // ne junat joilla ei ole yhtään asemaa jolla olisi myöhästymissyy
            trainList = trainList.FindAll(
                train => (train.timeTableRows = train.timeTableRows.FindAll(
                    row => row.causes != null
                    )).Count != 0
                );

            // täten palautetaan ainoastaan tarpeellinen data
            return trainList;
        }



        // haetaan kaikki myöhästymiset annettujen päivämäärien väliltä
        public List<TrainModel> SelectDelaysBetweenDates(DateTime from, DateTime to)
        {
            // palautusarvo
            List<TrainModel> trainList = new List<TrainModel>();

            // haetaan loopissa kaikki päivät erikseen ja lisätään palautusarvoon
            for (DateTime date = from; date <= to; date = date.AddDays(1))
            {
                Debug.WriteLine("Date: " + date.ToString("yyyy-MM-dd"));
                trainList.AddRange(this.Mapper.SelectAllFromHistory(date));
            }

            // lasketaan juna-malleilta puuttuvat AverageDelay- ja MaxDelay-arvot
            trainList.ForEach(
                train => {
                    train.AverageDelay = (int)train.timeTableRows.Average(
                        row => row.differenceInMinutes
                        );
                    train.MaxDelay = (int)train.timeTableRows.Max(
                        row => row.differenceInMinutes
                        );
                });

            // järjestetään lista AverageDelay mukaan
            trainList = trainList.OrderByDescending(train => train.AverageDelay).ToList();


            return trainList;
        }

        
    }
}
