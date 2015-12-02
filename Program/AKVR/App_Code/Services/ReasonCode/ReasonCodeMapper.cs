using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;

namespace AKVR.Services.ReasonCode
{
    public class ReasonCodeMapper : BaseMapper
    {
        // heitetään factoryltä tulevat clientit basemapperille
        public ReasonCodeMapper(WebClient webClient, LocalClient localClient, SessionClient sessionClient) : base(webClient, localClient, sessionClient)
        {
        }

        
        // model-listana kaikki mitä vr:ltä irti saa
        public List<ReasonCodeModel> SelectAll()
        {
            // palautusarvo
            List<ReasonCodeModel> ReasonCodes;
            
            try
            {
                // get json string
                string json = this.getJSON("metadata/detailed-cause-category-codes", true, 3600);
                // yritetään deserialisoida
                ReasonCodes = JsonConvert.DeserializeObject<List<ReasonCodeModel>>(json);
            }
            catch (Exception ex)
            {
                // jos epäonnistuiu niin palautetaan ainakin tyhjä lista
                ReasonCodes = new List<ReasonCodeModel>();
                Debug.WriteLine("AKVR:SelectAll FAILED: " + ex.Message);
            }
            
            return ReasonCodes;
        }

    }
}