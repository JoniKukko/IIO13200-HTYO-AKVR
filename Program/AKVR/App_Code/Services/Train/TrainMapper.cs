using Newtonsoft.Json;
using System;
using System.Diagnostics;

namespace AKVR.Services.Train
{


    public class TrainMapper : BaseMapper
    {
        // this has to be in every mapper (and BaseMapper parent also)
        public TrainMapper(bool fromWeb) : base(fromWeb) { }
        

        
        public TrainModel Select(int trainNumber)
        {
            // palautusarvo
            TrainModel train;

            try
            {
                Debug.WriteLine("AKVR:TrainMapper:Select - Trying to get train by trainNumber " + trainNumber);

                // Haetaan json
                string json = this.getJSON("live-trains/" + trainNumber.ToString());
                // poistetaan taulukko ympäriltä
                json = this.stripArray(json);
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
