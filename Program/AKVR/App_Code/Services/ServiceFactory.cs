using System.Diagnostics;
using AKVR.Services.Train;

namespace AKVR.Services
{
    public static class ServiceFactory
    {

        public static TrainService Train()
        {
            Debug.WriteLine("AKVR:ServiceFactory - New Train.Service");
            return new TrainService(new TrainMapper());
        }

        
    }
}
