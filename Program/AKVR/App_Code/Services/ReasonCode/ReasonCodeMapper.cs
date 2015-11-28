using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;

namespace AKVR.Services.ReasonCode
{
    public class ReasonCodeMapper : BaseMapper
    {
        public ReasonCodeMapper(WebClient webClient, LocalClient localClient, SessionClient sessionClient) : base(webClient, localClient, sessionClient)
        {
        }

        public List<ReasonCodeModel> SelectAll()
        {
            List<ReasonCodeModel> ReasonCodes;
            
            try
            {
                string json = this.getJSON("metadata/detailed-cause-category-codes", true, 3600);
                ReasonCodes = JsonConvert.DeserializeObject<List<ReasonCodeModel>>(json);
            }
            catch (Exception ex)
            {
                ReasonCodes = new List<ReasonCodeModel>();
                Debug.WriteLine("AKVR:SelectAll FAILED: " + ex.Message);
            }
            
            return ReasonCodes;
        }

    }
}