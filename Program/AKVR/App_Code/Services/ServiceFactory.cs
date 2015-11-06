using System.Diagnostics;
using AKVR.Services.Train;

namespace AKVR.Services
{
    public static class ServiceFactory
    {

        // false = development
        // true = production
        // maybe this should be in web.config or something
        private const bool fromWebDefault = false;
        

        public static TrainService Train(bool fromWeb = fromWebDefault)
        {
            Debug.WriteLine("AKVR:ServiceFactory - New Train.Service");
            return new TrainService(new TrainMapper(fromWeb));
        }

        
    }
}
