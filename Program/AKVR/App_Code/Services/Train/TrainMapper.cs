using Newtonsoft.Json;
using System;
using System.Diagnostics;

namespace AKVR.Services.Train
{


    public class TrainMapper : BaseMapper
    {
        
        public TrainModel SelectByTrainNumber(int trainNumber)
        {
            // palautusarvo
            TrainModel train;

            try
            {
                Debug.WriteLine("AKVR:TrainMapper:Select - Trying to get train by trainNumber " + trainNumber);

                // Haetaan json
                string json = this.getJSON("live-trains/" + trainNumber.ToString());

                // yritetään deserialisoida
                train = JsonConvert.DeserializeObject<TrainModel>(json);
                
            }
            catch (Exception ex)
            {
                Debug.WriteLine("AKVR:TrainMapper:Select - FAILED: " + ex.Message);
                // palautetaan ainakin tyhjä sitten..
                train = new TrainModel();
            }
            
            return train;
        }

        
    }

    
}
