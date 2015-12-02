using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Net;

namespace AKVR.Services.Assembly
{
    public class AssemblyMapper : BaseMapper
    {

        // heitetään factoryltä tulevat clientit basemapperille
        public AssemblyMapper(WebClient webClient, LocalClient localClient, SessionClient sessionClient) : base(webClient, localClient, sessionClient)
        {
        }



        // hakee yhden mallin junanumeron ja lähtöpäivän mukaan
        public AssemblyModel SelectByTrainNumber(int trainNumber, string date)
        {
            // palautusarvo
            AssemblyModel assembly;
            try
            {
                // Haetaan json
                string json = this.getJSON("compositions/" + trainNumber.ToString() + "?departure_date=" + date, false, 600);
                // yritetään deserialisoida
                assembly = JsonConvert.DeserializeObject<AssemblyModel>(json);

            }
            catch (Exception ex)
            {
                Debug.WriteLine("AKVR:SelectByTrainNumber FAILED: " + ex.Message);
                // palautetaan ainakin tyhjä sitten..
                assembly = new AssemblyModel();
            }
            return assembly;
        }
        
    }
}