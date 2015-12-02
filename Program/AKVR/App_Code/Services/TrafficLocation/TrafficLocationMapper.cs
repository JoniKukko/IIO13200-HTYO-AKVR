using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;

namespace AKVR.Services.TrafficLocation
{
    public class TrafficLocationMapper : BaseMapper
    {
        // heitetään factoryltä tulevat clientit basemapperille
        public TrafficLocationMapper(WebClient webClient, LocalClient localClient, SessionClient sessionClient) : base(webClient, localClient, sessionClient)
        {
        }


        // model-listana kaikki mitä vr:ltä löytyy
        public List<TrafficLocationModel> SelectAll()
        {
            // palautusarvo
            List<TrafficLocationModel> trafficLocations;
            
            try
            {
                // get json string
                string json = this.getJSON("metadata/stations", true, 3600);
                // yritetään deserialisoida json string
                trafficLocations = JsonConvert.DeserializeObject<List<TrafficLocationModel>>(json);
            }
            catch (Exception ex)
            {
                // jos  epäonnistuu niin palautetaan ainakin tyhjä lista
                trafficLocations = new List<TrafficLocationModel>();
                Debug.WriteLine("AKVR:SelectAll FAILED: " + ex.Message);
            }
            
            return trafficLocations;
        }

    }
}