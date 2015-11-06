using Newtonsoft.Json;
using System;
using System.Net;

namespace AKVR.Services.Train
{


    public class Mapper : BaseJsonMapper
    {
        public Mapper(LocalClient client) : base(client) { }
        

        
        public Model Select(int trainNumber)
        {
            // palautusarvo
            Model train;
            string json;

            try
            {
                // Haetaan json
                json = this.getJSON("live-trains/" + trainNumber.ToString());

                // poistetaan taulukko ympäriltä
                json = this.stripArray(json);

                // yritetään deserialisoida
                train = JsonConvert.DeserializeObject<Model>(json);
                
            }
            catch (Exception ex)
            {
                // palautetaan ainakin tyhjä sitten..
                train = new Model();
            }


            return train;
        }



    }


}
