using System;
using System.Diagnostics;
using System.Net;
using System.Text.RegularExpressions;
using System.Web.Configuration;
using System.Text;



namespace AKVR.Services
{

    public abstract class BaseMapper : WebClient
    {
        
        private bool fromWeb = (WebConfigurationManager.AppSettings["AKVRproduction"] == "true");
        private string VRbaseurl = WebConfigurationManager.AppSettings["VRpath"];
        private string Localbaseurl = AppDomain.CurrentDomain.GetData("DataDirectory").ToString() + "\\JSON\\";

        private WebClient webClient;
        private LocalClient localClient;
        private SessionClient sessionClient;



        public BaseMapper (WebClient webClient, LocalClient localClient, SessionClient sessionClient)
        {
            this.webClient = webClient;
            this.localClient = localClient;
            this.sessionClient = sessionClient;
            
            this.webClient.Encoding = UTF8Encoding.UTF8;
        }
        


        // hakee jsonin annetusta urlista
        // voidaan määrittää palautetaanko taulukkona
        // voidaan määrittää välimuistin aika
        protected string getJSON(string address, bool returnArray = true, int cacheTime = 10)
        {

            string json = "";
            
            // Tarkistetaan mistä json pitäisi hakea
            if (this.sessionClient.Check(address)) // session
                json = this.DownloadFromSession(address);
            else if (this.fromWeb) // web
                json = this.DownloadFromWeb(address, cacheTime);
            else  // local
                json = this.DownloadFromLocal(address);
            

            // just to make sure there is something valid
            if (json.Length < 5)
            {
                Debug.WriteLine("AKVR:BaseMapper - Empty answer");
                json = "{}";
            }
            

            // palautetaan pyydetyssä muodossa
            return (returnArray) 
                ? this.addArray(json) 
                : this.stripArray(json);
        }



        // ladataan json sessionista
        private string DownloadFromSession(string path)
        {
            Debug.WriteLine("AKVR:BaseMapper - Downloading JSON from SESSION (" + path + ")");
            return this.sessionClient.DownloadString(path);
        }


        // ladataan json vr:n palvelusta
        private string DownloadFromWeb(string path, int cacheTime)
        {
            Debug.WriteLine("AKVR:BaseMapper - Downloading JSON from WEB (" + this.VRbaseurl + path + ")");
            string json = this.webClient.DownloadString(this.VRbaseurl + path);
            this.sessionClient.Add(path, json, cacheTime);
            return json;
        }


        // ladataan json localista
        private string DownloadFromLocal(string path)
        {
            path = this.Localbaseurl + (new Regex("/|=|\\?")).Replace(path, "-") + ".json";
            Debug.WriteLine("AKVR:BaseMapper - Downloading JSON from LOCAL (" + path + ")");
            return this.localClient.DownloadString(path);
        }
        


        // vr näyttää palauttavan paljon taulukkona joten
        // simppeli metodi karsimaan se YMPÄRILTÄ pois
        private string stripArray(string json)
        {
            return (json[0] == '[')
                ? json.Substring(1, json.Length - 2)
                : json;
        }


        // voidaan varmistaa että data on taulukkona jos se niin halutaan ulos tulevan
        private string addArray(string json)
        {
            return (json[0] != '[')
                ? '[' + json + ']'
                : json;
        }
    }
}
