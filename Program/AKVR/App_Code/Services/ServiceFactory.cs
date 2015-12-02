using System.Diagnostics;
using AKVR.Services.Train;
using AKVR.Services.TrafficLocation;
using AKVR.Services.Assembly;
using AKVR.Services.TrainType;
using AKVR.Services.Operator;
using AKVR.Services.ReasonCategory;
using AKVR.Services.ReasonCode;
using System.Net;



/**
 * Factory pattern for services
 */
namespace AKVR.Services
{
    public static class ServiceFactory
    {

        // Train service
        public static TrainService Train()
        {
            Debug.WriteLine("AKVR:ServiceFactory - New Train.Service");
            return new TrainService(new TrainMapper(new WebClient(), new LocalClient(), new SessionClient()));
        }



        // TrafficLocation service
        public static TrafficLocationService TrafficLocation()
        {
            Debug.WriteLine("AKVR:ServiceFactory - New TrafficLocation.Service");
            return new TrafficLocationService(new TrafficLocationMapper(new WebClient(), new LocalClient(), new SessionClient()));
        }



        // Assembly service
        public static AssemblyService Assembly()
        {
            Debug.WriteLine("AKVR:ServiceFactory - New Assembly.Service");
            return new AssemblyService(new AssemblyMapper(new WebClient(), new LocalClient(), new SessionClient()));
        }



        // TrainType service
        public static TrainTypeService TrainType()
        {
            Debug.WriteLine("AKVR:ServiceFactory - New TrainType.Service");
            return new TrainTypeService(new TrainTypeMapper(new WebClient(), new LocalClient(), new SessionClient()));
        }



        // Operator service
        public static OperatorService Operator()
        {
            Debug.WriteLine("AKVR:ServiceFactory - New Operator.Service");
            return new OperatorService(new OperatorMapper(new WebClient(), new LocalClient(), new SessionClient()));
        }



        // ReasonCategory service
        public static ReasonCategoryService ReasonCategory()
        {
            Debug.WriteLine("AKVR:ServiceFactory - New ReasonCategory.Service");
            return new ReasonCategoryService(new ReasonCategoryMapper(new WebClient(), new LocalClient(), new SessionClient()));
        }


        // ReasonCode service
        public static ReasonCodeService ReasonCode()
        {
            Debug.WriteLine("AKVR:ServiceFactory - New ReasonCategory.Service");
            return new ReasonCodeService(new ReasonCodeMapper(new WebClient(), new LocalClient(), new SessionClient()));
        }
        
    }
}
