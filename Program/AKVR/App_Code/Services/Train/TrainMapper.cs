using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;

namespace AKVR.Services.Train
{
    public class TrainMapper : BaseMapper
    {

        // heitetään factoryltä tulevat clientit basemapperille
        public TrainMapper(WebClient webClient, LocalClient localClient, SessionClient sessionClient) : base(webClient, localClient, sessionClient)
        {
        }



        // Palauttaa mallin junanumeron perusteella
        public TrainModel SelectByTrainNumber(int trainNumber)
        {
            // palautusarvo
            TrainModel train;
            try
            {
                Debug.WriteLine("AKVR:TrainMapper:SelectByTrainNumber(" + trainNumber.ToString() + ")");
                // Haetaan json
                string json = this.getJSON("live-trains/" + trainNumber.ToString(), false);
                // yritetään deserialisoida
                train = JsonConvert.DeserializeObject<TrainModel>(json);

            }
            catch (Exception ex)
            {
                Debug.WriteLine("AKVR:TrainMapper:SelectByTrainNumber(" + trainNumber.ToString() + ") - FAILED: " + ex.Message);
                // palautetaan ainakin tyhjä sitten..
                train = new TrainModel();
            }
            return train;
        }



        // haetaan kaikki mitä vr:ltä löytyy
        public List<TrainModel> SelectAll()
        {
            // palautusarvo
            List<TrainModel> trains;

            try
            {
                Debug.WriteLine("AKVR:SelectAll");
                // Haetaan json
                string json = this.getJSON("live-trains", true, 600);
                // yritetään deserialisoida
                trains = JsonConvert.DeserializeObject<List<TrainModel>>(json);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("AKVR:SelectAll FAILED: " + ex.Message);
                // palautetaan ainakin tyhjä jos epäonnistutaan
                trains = new List<TrainModel>();
            }

            return trains;
        }



        // hakee kaiken annetun päivän mukaan
        public List<TrainModel> SelectAllFromHistory(DateTime datetime)
        {
            // palautusarvo
            List<TrainModel> trains;

            try
            {
                Debug.WriteLine("AKVR:SelectAllFromHistory");
                // Haetaan json
                string json = this.getJSON("history?date="+datetime.ToString("yyyy-MM-dd"), true, 3600);
                // yritetään deserialisoida
                trains = JsonConvert.DeserializeObject<List<TrainModel>>(json);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("AKVR:SelectAllFromHistory FAILED: " + ex.Message);
                // palautetaan ainakin tyhjä jos epäonnistuu
                trains = new List<TrainModel>();
            }

            return trains;
        }



        // haetaan aseman lyhenteen perusteella saapuvat, saapuneet, lähtevät ja lähteneet junat
        public List<TrainModel> SelectByStationShortCode(string shortcode, int arrived_trains, int arriving_trains, int departed_trains, int departing_trains)
        {
            List<TrainModel> trains;
            try
            {
                // Haetaan json
                string json = this.getJSON("live-trains?station=" + shortcode + "&arrived_trains="+ arrived_trains + "&arriving_trains="+ arriving_trains + "&departed_trains="+ departed_trains + "&departing_trains="+ departed_trains, true);
                // yritetään deserialisoida
                trains = JsonConvert.DeserializeObject<List<TrainModel>>(json);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("AKVR:SelectByStationShortCode FAILED: " + ex.Message);
                // palautetaan ainakin tyhjä lista..
                trains = new List<TrainModel>();
            }

            return trains;
        }
        
    }
}
