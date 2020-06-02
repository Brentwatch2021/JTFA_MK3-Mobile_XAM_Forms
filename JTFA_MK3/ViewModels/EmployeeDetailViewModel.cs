using JTFA_MK3.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace JTFA_MK3.ViewModels
{
    public class EmployeeDetailViewModel : BaseEmployeeViewModel
    {
        public Employee Employee { get; set; }
        public EmployeeDetailViewModel(Employee employee = null)
        {
            Employee = employee;
            Title = String.Format("{0} {1}", Employee.firstName, Employee.lastName);
        }
    }
}
