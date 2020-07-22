using JTFA_MK3.Models.V2_Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace JTFA_MK3.Models
{
    public class JTFA_Invoice
    {
        public Guid JTFA_Invoice_ID { get; set; }

        public int INV_Number { get; set; }

        public Guid JobCardID { get; set; }

        public string Email_Recipients { get; set; }
    }
}
