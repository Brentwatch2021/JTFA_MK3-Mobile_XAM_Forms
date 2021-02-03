using System;
using System.Collections.Generic;
using System.Text;

namespace JTFA_MK3.Models.V2_Models
{
    public class Mail_Request
    {
        public Guid Mail_Request_ID { get; set; }
        public string ToEmail { get; set; }
        public string Subject { get; set; }
        public string JobCardID { get; set; }
        public string Body { get; set; }
    }
}
