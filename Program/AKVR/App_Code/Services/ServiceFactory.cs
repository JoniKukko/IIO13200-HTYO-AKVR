using System.Diagnostics;
using AKVR.Services.Train;
using AKVR.Services.TrafficLocation;
using AKVR.Services.Assembly;

namespace AKVR.Services
{
    public static class ServiceFactory
    {

        public static TrainService Train()
        {
            Debug.WriteLine("AKVR:ServiceFactory - New Train.Service");
            return new TrainService(new TrainMapper());
        }

        public static TrafficLocationService TrafficLocation()
        {
            Debug.WriteLine("AKVR:ServiceFactory - New TrafficLocation.Service");
            return new TrafficLocationService(new TrafficLocationMapper());
        }

        public static AssemblyService Assembly()
        {
            Debug.WriteLine("AKVR:ServiceFactory - New Assembly.Service");
            return new AssemblyService(new AssemblyMapper());
        }


    }
}
