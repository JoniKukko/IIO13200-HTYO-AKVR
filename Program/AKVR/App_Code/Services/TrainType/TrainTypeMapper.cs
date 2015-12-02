using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;

namespace AKVR.Services.TrainType
{
    public class TrainTypeMapper : BaseMapper
    {
        // heitetään factoryltä tulevat clientit basemapperille
        public TrainTypeMapper(WebClient webClient, LocalClient localClient, SessionClient sessionClient) : base(webClient, localClient, sessionClient)
        {
        }


        // haetaan kaikki mitä vr:ltä tulee
        public List<TrainTypeModel> SelectAll()
        {
            // palautusarvo
            List<TrainTypeModel> trainTypes;
            
            try
            {
                // haetana json string
                string json = this.getJSON("metadata/train-types", true, 3600);
                // koetetaan deserialisoida json string objectiksi
                trainTypes = JsonConvert.DeserializeObject<List<TrainTypeModel>>(json);
            }
            catch (Exception ex)
            {
                // jos epäonnistutaan niin palautetaan ainakin tyhjä lista
                trainTypes = new List<TrainTypeModel>();
                Debug.WriteLine("AKVR:SelectAll FAILED: " + ex.Message);
            }
            

            return trainTypes;
        }

    }
}