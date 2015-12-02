using System.IO;
using System.Text;

/**
 * Yksinkertainen luokka yhtenäistämään toimintoja
 **/
namespace AKVR.Services
{
    public class LocalClient
    {
        public Encoding Encoding = UTF8Encoding.UTF8;

        // Kuten webclientin metodi
        public string DownloadString(string path)
        {
            return File.ReadAllText(path);
        }
    }
}