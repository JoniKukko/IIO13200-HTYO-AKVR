using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;


namespace AKVR.Services.Operator
{
    public class OperatorService
    {
        // from here...
        private OperatorMapper Mapper { get; set; }

        public OperatorService(OperatorMapper mapper)
        {
            this.Mapper  = mapper;
        }
        // ...to here should be in every service class



        // hakee mapperilta kaiken ja parsii itse joko kokonaisella nimellä tai
        // nimen alulla.
        public OperatorModel SelectByOperatorName(string operatorName)
        {
            // palautusarvo
            OperatorModel Operator = null;

            // haetaan lista ja etsitään kokonaisella nimellä
            List<OperatorModel> list = this.Mapper.SelectAll();
            Operator = list.Find(m => m.operatorName == operatorName);

            // jos ei löytynyt kokonaisella nimellä mitään niin haetaan nimen alulla
            if (Operator == null)
            {
                Debug.WriteLine("Operator not found with full operatorName - trying with \"StartsWith\"");
                Operator = list.Find(m => m.operatorName.StartsWith(operatorName));

            } else
            {
                Debug.WriteLine("Operator found with full name");
            }


            // jos nimen alullakaan ei löytynyt mitään niin palautetaan lopulta tyhjä
            return (Operator == null)
                ? new OperatorModel()
                : Operator;
        }



        // hakee kaiken mapperilta ja parsii itse nimen alun perusteella halutun listan
        public List<OperatorModel> SelectListByOperatorName(string operatorName)
        {
            // haetaan kaikki
            List<OperatorModel> Operators = this.Mapper.SelectAll();
            // palautetaan ne joiden nimi alkaa annetulla parametrillä
            return Operators.FindAll(m => m.operatorName.StartsWith(operatorName));
        }


        // Ei mitään erikoista, palautetaan kaikki
        public List<OperatorModel> SelectAll()
        {
            return this.Mapper.SelectAll();
        }

    }
}