using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;

namespace AKVR.Services
{
    public class LocalClient : WebClient
    {


        private bool fromWeb;
        private string VRbaseurl = "http://rata.digitraffic.fi/api/v1/";



        public LocalClient(bool fromWeb)
        {
            this.fromWeb = fromWeb;
        }



        public string DownloadJSON(string address)
        {
            string json;

            if (this.fromWeb)
            {
                string path = VRbaseurl + address;
                Debug.WriteLine("Downloading JSON from WEB (" + path + ")");
                json = base.DownloadString(path);
            }
            else
            {
                json = this.DownloadLocalString(address);
            }

            return json;
        }



        private string DownloadLocalString(string address)
        {
            
            string json;

            try
            {
                // merkit / ja = ja ? vaihdetaan merkkiin -
                Regex pattern = new Regex("/|=|\\?");
                string url = pattern.Replace(address, "-");

                // kasataan koko path kuntoon
                string path 
                    = AppDomain.CurrentDomain.GetData("DataDirectory").ToString() 
                    + "\\JSON\\" + url + ".json";
                
                // ladataan tiedoston sisältö
                Debug.WriteLine("Downloading JSON from LOCAL (" + path + ")");
                json = File.ReadAllText(path);

            }
            catch (Exception ex)
            {
                Debug.WriteLine("Downloading JSON from LOCAL FAILED - No file");
                json = "[{}]";
            }

            return json;
        }



    }
}