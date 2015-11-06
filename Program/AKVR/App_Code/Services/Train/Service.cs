using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AKVR.Services.Train
{
    public class Service
    {
        private Mapper Mapper { get; set; }

        public Service(Mapper mapper)
        {
            this.Mapper = mapper;
        }


        public Model Select(int id)
        {
            return this.Mapper.Select(id);
        }


    }
}
