using System.IO;
using System.Text;

namespace AKVR.Services
{
    public class LocalClient
    {
        public Encoding Encoding = UTF8Encoding.UTF8;

        public string DownloadString(string path)
        {
            return File.ReadAllText(path);
        }
    }
}