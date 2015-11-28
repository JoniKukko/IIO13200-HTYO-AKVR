using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Net;

namespace AKVR.Services.TrainType
{
    public class TrainTypeMapper : BaseMapper
    {
        public TrainTypeMapper(WebClient webClient, LocalClient localClient, SessionClient sessionClient) : base(webClient, localClient, sessionClient)
        {
        }

        public List<TrainTypeModel> SelectAll()
        {
            List<TrainTypeModel> trainTypes;
            
            try
            {
                string json = this.getJSON("metadata/train-types", true, 3600);
                trainTypes = JsonConvert.DeserializeObject<List<TrainTypeModel>>(json);
            }
            catch (Exception ex)
            {
                trainTypes = new List<TrainTypeModel>();
                Debug.WriteLine("AKVR:SelectAll FAILED: " + ex.Message);
            }
            
            return trainTypes;
        }

    }
}