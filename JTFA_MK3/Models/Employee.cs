using System;
using System.Collections.Generic;
using System.Text;

namespace JTFA_MK3.Models
{
    public class Employee
    {
        public long employeeId { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public DateTime dateOfBirth { get; set; }
        public string phoneNumbwer { get; set; }

        public string gender { get; set; }
        public string email { get; set; }
    }
}
