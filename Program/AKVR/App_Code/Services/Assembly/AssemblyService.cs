namespace AKVR.Services.Assembly
{
    public class AssemblyService
    {
        // from here...
        private AssemblyMapper Mapper { get; set; }

        public AssemblyService(AssemblyMapper mapper)
        {
            this.Mapper  = mapper;
        }
        // ...to here should be in every service class


        // Nothing to do, passing call to mapper
        public AssemblyModel SelectByTrainNumber(int trainNumber, string date)
        {
            return this.Mapper.SelectByTrainNumber(trainNumber, date);
        }



    }
}