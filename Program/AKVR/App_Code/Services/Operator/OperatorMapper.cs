using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;


namespace AKVR.Services.Operator
{
    public class OperatorMapper : BaseMapper
    {

        // heitetään factoryltä tulevat clientit basemapperille
        public OperatorMapper(WebClient webClient, LocalClient localClient, SessionClient sessionClient) : base(webClient, localClient, sessionClient)
        {
        }

        
        // Model-listana kaikki mitä vr:ltä löytyy
        public List<OperatorModel> SelectAll()
        {
            // palautusarvo
            List<OperatorModel> Operators;
            
            try
            {
                // get json string
                string json = this.getJSON("metadata/operators", true, 3600);
               
                // YRitetään deserialisoida
                Operators = JsonConvert.DeserializeObject<List<OperatorModel>>(json);
            }
            catch (Exception ex)
            {
                // palautetaan ainakin tyhjä lista
                Operators = new List<OperatorModel>();
                Debug.WriteLine("AKVR:SelectAll FAILED: " + ex.Message);
            }
            
            return Operators;
        }

    }
}