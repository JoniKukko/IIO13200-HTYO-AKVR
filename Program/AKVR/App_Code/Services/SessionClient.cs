using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Web;

namespace AKVR.Services
{
    struct SessionStorageModel
    {
        public DateTime validTo;
        public string json;
    }

    public class SessionClient
    {
        public Encoding Encoding = UTF8Encoding.UTF8;
        private static Dictionary<string, SessionStorageModel> SessionStorage
        {
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




        public string DownloadString(string path)
        {
            return this.Check(path)
                ? SessionStorage[path].json
                : null;
        }


        public bool Check(string address)
        {
            // true jos avain löytyy ja avaimen validTo on alle nykyinen hetki
            bool value = (SessionStorage.ContainsKey(address) && SessionStorage[address].validTo > DateTime.Now);

            // jos false niin poistetaan avain.
            if (!value) SessionStorage.Remove(address);

            return value;
        }


        public bool Add(string path, string json, int cacheTime)
        {
            SessionStorageModel newSessionStorageModel;
            newSessionStorageModel.json = json;
            newSessionStorageModel.validTo = DateTime.Now.AddSeconds(cacheTime);
            SessionStorage.Add(path, newSessionStorageModel);
            return true;
        }


    }
}