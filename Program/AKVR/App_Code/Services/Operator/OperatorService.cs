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



        public OperatorModel SelectByOperatorName(string operatorName)
        {
            OperatorModel Operator = null;
            List<OperatorModel> list = this.Mapper.SelectAll();
            Operator = list.Find(m => m.operatorName == operatorName);

            if (Operator == null)
            {
                Debug.WriteLine("Operator not found with full operatorName - trying with \"StartsWith\"");
                Operator = list.Find(m => m.operatorName.StartsWith(operatorName));
            } else
            {
                Debug.WriteLine("Operator found with full name");
            }

            return (Operator == null)
                ? new OperatorModel()
                : Operator;
        }



        public List<OperatorModel> SelectListByOperatorName(string operatorName)
        {
            List<OperatorModel> Operators = this.Mapper.SelectAll();
            return Operators.FindAll(m => m.operatorName.StartsWith(operatorName));
        }



        public List<OperatorModel> SelectAll()
        {
            return this.Mapper.SelectAll();
        }

    }
}