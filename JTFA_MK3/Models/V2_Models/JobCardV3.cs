using System;
using System.Collections.Generic;
using System.Text;

namespace JTFA_MK3.Models.V2_Models
{
    public class JobCardV3
    {
        
        public Guid JobCardID { get; set; }
        
        public string JobCardName { get; set; }

        public ICollection<TaskDescriptionV3> TaskDescriptions { get; set; }

        // IGNORE the attributes below this is for the Mobile API
        public Vehicle_Display_ID VehicleDisplay { get; set; }

        public Client_Display_ID ClientDisplay { get; set; }

        public Vehicle Vehicle { get; set; }

        public JTFA_Client JTFA_Client { get; set; }

        public override string ToString()
        {
            return JobCardName;
        }
    }
}
