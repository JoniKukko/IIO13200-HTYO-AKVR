using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Web.Configuration;
using System.Web;

namespace AKVR.Services
{

    struct SessionStorageModel
    {
        public DateTime validTo;
        public string json;
    }

    public abstract class BaseMapper : WebClient
    {


        private bool fromWeb = (WebConfigurationManager.AppSettings["AKVRproduction"] == "true");
        private string VRbaseurl = WebConfigurationManager.AppSettings["VRpath"];
        private string Localbaseurl = AppDomain.CurrentDomain.GetData("DataDirectory").ToString() + "\\JSON\\";
        private static Dictionary<string, SessionStorageModel> SessionStorage {
            get
            {
                if (HttpContext.Current.Session["SessionStorage"] == null)
                {
                    Debug.WriteLine("AKVR:BaseMapper - New SessionStorage");
                    HttpContext.Current.Session["SessionStorage"] = new Dictionary<string, SessionStorageModel>();
                }
                return (Dictionary<string, SessionStorageModel>)HttpContext.Current.Session["SessionStorage"];
            }
        }

        

        // hakee jsonin annetusta urlista
        protected string getJSON(string address, bool returnArray = true, int cacheTime = 10)
        {
            string json;

            if (this.checkSessionStorage(address))
            {
                json = this.DownloadFromSessionStorage(address);
            }
            else if (this.fromWeb)
            {
                json = this.DownloadFromWeb(address);
                addToSessionStorage(address, json, cacheTime);
            }
            else
            {
                json = this.DownloadFromLocal(address);
            }
            
            
            if (json.Length < 5)
            {
                Debug.WriteLine("AKVR:BaseMapper - Empty answer");
                // just to make sure there is something
                json = "{}";
            }

            return (returnArray) ? this.addArray(json) : this.stripArray(json);
        }



        private bool checkSessionStorage(string address)
        {
            // true jos avain löytyy ja avaimen validTo on alle nykyinen hetki
            bool value = (SessionStorage.ContainsKey(address) && SessionStorage[address].validTo > DateTime.Now);

            // jos false niin poistetaan avain.
            if (!value)
            {
                SessionStorage.Remove(address);
            }

            return value;
        }



        // hakee jsonin sessionista
        private string DownloadFromSessionStorage(string address)
        {
            Debug.WriteLine("AKVR:BaseMapper - Downloading JSON from SESSIONSTORAGE (" + address + ")");
            return SessionStorage[address].json;
        }



        // lisätään sessionstorageen tavaraa
        private void addToSessionStorage(string address, string json, int cacheTime)
        {
            SessionStorageModel newSessionStorageModel;
            newSessionStorageModel.json = json;
            newSessionStorageModel.validTo = DateTime.Now.AddSeconds(cacheTime);
            SessionStorage.Add(address, newSessionStorageModel);
        }



        // hakee jsonin webistä
        private string DownloadFromWeb(string address)
        {
            string json = "";

            try
            {

                // kasataan path
                string path = VRbaseurl + address;

                // ladataan tiedoston sisältö
                Debug.WriteLine("AKVR:BaseMapper - Downloading JSON from WEB (" + path + ")");
                json = base.DownloadString(path);

            }
            catch (Exception ex)
            {
                Debug.WriteLine("AKVR:BaseMapper - Downloading JSON from WEB FAILED: " + ex.Message);
            }
            
            return json;
        }
        
        

        // hakee jsonin localista
        private string DownloadFromLocal(string address)
        {
            string json = "";

            try
            {

                // merkit / ja = ja ? vaihdetaan merkkiin -
                Regex pattern = new Regex("/|=|\\?");
                string url = pattern.Replace(address, "-");

                // kasataan koko path kuntoon
                string path = this.Localbaseurl + url + ".json";

                // ladataan tiedoston sisältö
                Debug.WriteLine("AKVR:BaseMapper - Downloading JSON from LOCAL (" + path + ")");
                json = File.ReadAllText(path);

            }
            catch (Exception ex)
            {
                Debug.WriteLine("AKVR:BaseMapper - Downloading JSON from LOCAL FAILED: " + ex.Message);
            }

            return json;
        }




        // vr näyttää palauttavan paljon taulukkona joten
        // simppeli metodi karsimaan se YMPÄRILTÄ pois mikäli
        // halutaan vain yksittäinen
        private string stripArray(string json)
        {
            return (json[0] == '[')
                ? json.Substring(1, json.Length - 2)
                : json;
        }


        private string addArray(string json)
        {
            return (json[0] != '[')
                ? '[' + json + ']'
                : json;
        }



    }


}
