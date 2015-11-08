using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Web.Configuration;

namespace AKVR.Services
{

    public abstract class BaseMapper : WebClient
    {


        private bool fromWeb { get; set; }
        private string VRbaseurl = WebConfigurationManager.AppSettings["VRpath"];
        private string Localbaseurl = AppDomain.CurrentDomain.GetData("DataDirectory").ToString() + "\\JSON\\";



        public BaseMapper(bool fromWeb)
        {
            this.fromWeb = fromWeb;
        }



        // vr näyttää palauttavan paljon taulukkona joten
        // simppeli metodi karsimaan se YMPÄRILTÄ pois mikäli
        // halutaan vain yksittäinen
        protected string stripArray(string json)
        {
            return (json[0] == '[') 
                ? json.Substring(1, json.Length - 2) 
                : json;
        }



        // hakee jsonin annetusta urlista
        protected string getJSON(string address)
        {
            return (this.fromWeb)
                ? this.DownloadFromWeb(address)
                : this.DownloadFromLocal(address);
        }



        // hakee jsonin webistä
        private string DownloadFromWeb(string address)
        {
            string json;

            try {

                // kasataan path
                string path = VRbaseurl + address;

                // ladataan tiedoston sisältö
                Debug.WriteLine("AKVR:BaseMapper - Downloading JSON from WEB (" + path + ")");
                json = base.DownloadString(path);

            } catch (Exception ex)
            {
                Debug.WriteLine("AKVR:BaseMapper - Downloading JSON from WEB FAILED: " + ex.Message);
                json = "[{}]";
            }

            return json;
        }



        // hakee jsonin localista
        private string DownloadFromLocal(string address)
        {
            string json;

            try {

                // merkit / ja = ja ? vaihdetaan merkkiin -
                Regex pattern = new Regex("/|=|\\?");
                string url = pattern.Replace(address, "-");

                // kasataan koko path kuntoon
                string path = this.Localbaseurl + url + ".json";

                // ladataan tiedoston sisältö
                Debug.WriteLine("AKVR:BaseMapper - Downloading JSON from LOCAL (" + path + ")");
                json = File.ReadAllText(path);

            } catch (Exception ex)
            {
                Debug.WriteLine("AKVR:BaseMapper - Downloading JSON from LOCAL FAILED: " + ex.Message);
                json = "[{}]";
            }

            return json;
        }



    }


}
