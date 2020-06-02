using System;
using System.Collections.Generic;
using System.Text;

namespace JTFA_MK3.Models.V2_Models
{
    public class TaskDescriptionV3
    {
        
        public Guid Task_Description_ID { get; set; }

        public string Description { get; set; }

        public string LabourCost { get; set; }

        public string PartsPrice { get; set; }
        
        public string TotalTaskCost { get; set; }

        public JobCardV3 JobCardV3 { get; set; }
        public Guid JobCardIDForRef { get; set; }
    }
}
