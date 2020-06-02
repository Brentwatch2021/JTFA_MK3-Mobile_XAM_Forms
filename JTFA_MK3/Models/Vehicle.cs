using System;
using System.Collections.Generic;
using System.Text;

namespace JTFA_MK3.Models
{
    public class Vehicle
    {
        public Guid Vehicle_ID { get; set; }

        public string Make { get; set; }

        public string VIN { get; set; }

        public string REG { get; set; }

        public string Mileage { get; set; }

        public string Engine_Number { get; set; }

        public string Year { get; set; }

        public string CC { get; set; }


        public override string ToString()
        {
            return String.Format("{0} {1}", Make,REG);
        }

    }
}
