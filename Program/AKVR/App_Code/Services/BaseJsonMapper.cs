using System.Net;

namespace AKVR.Services
{

    public abstract class BaseJsonMapper
    {
        

        private LocalClient Client { get; set; }

        

        public BaseJsonMapper(LocalClient Client)
        {
            this.Client = Client;
        }


        
        ~BaseJsonMapper()
        {
            this.Client.Dispose();
        }




        // hakee jsonin annetusta urlista
        protected string getJSON(string url)
        {
            return this.Client.DownloadJSON(url);
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

        
    }

    
}
