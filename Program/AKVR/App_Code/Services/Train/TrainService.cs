using System.Diagnostics;


namespace AKVR.Services.Train
{


    public class TrainService
    {
        // from here...
        private TrainMapper Mapper { get; set; }

        public TrainService(TrainMapper mapper)
        {
            this.Mapper  = mapper;
        }
        // ...to here should be in every service class





        // nothing special to do..
        // passing method call to mapper
        public TrainModel SelectByTrainNumber(int trainNumber)
        {
            return this.Mapper.SelectByTrainNumber(trainNumber);
        }


    }


}
