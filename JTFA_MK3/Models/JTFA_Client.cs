using System;
using System.Collections.Generic;
using System.Text;

namespace JTFA_MK3.Models
{
    public class JTFA_Client
    {
        public Guid JTFA_CLIENT_ID { get; set; }

        public string Name { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public string Notifications_Permission_Levels_Allowed { get; set; }
    }
}
