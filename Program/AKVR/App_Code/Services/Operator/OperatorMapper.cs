using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Net;

namespace AKVR.Services.Operator
{
    public class OperatorMapper : BaseMapper
    {
        public OperatorMapper(WebClient webClient, LocalClient localClient, SessionClient sessionClient) : base(webClient, localClient, sessionClient)
        {
        }

        public List<OperatorModel> SelectAll()
        {
            List<OperatorModel> Operators;
            
            try
            {
                string json = this.getJSON("metadata/operators", true, 3600);
                Operators = JsonConvert.DeserializeObject<List<OperatorModel>>(json);
            }
            catch (Exception ex)
            {
                Operators = new List<OperatorModel>();
                Debug.WriteLine("AKVR:SelectAll FAILED: " + ex.Message);
            }
            
            return Operators;
        }

    }
}