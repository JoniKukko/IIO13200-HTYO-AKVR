using System.Net;
using System.Diagnostics;
using System;

namespace AKVR.Services
{
    public static class Factory
    {

        // false = development
        // true = production
        private const bool fromWebDefault = false;
        

        public static Train.Service Train(bool fromWeb = fromWebDefault)
        {
            return new Train.Service(new Train.Mapper(new LocalClient(fromWeb)));
        }

        
    }
}
