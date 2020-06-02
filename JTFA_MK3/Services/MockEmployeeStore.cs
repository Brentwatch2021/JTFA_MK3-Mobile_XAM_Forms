using JTFA_MK3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JTFA_MK3.Services
{
    public class MockEmployeeStore : IEmployeeStore<Employee>
    {
        readonly List<Employee> employees;

        public MockEmployeeStore()
        {
            employees = new List<Employee>()
            {
                new Employee { employeeId = 1, firstName = "First Name", lastName="Last Name", gender="Gender", phoneNumbwer="068 4565 333", email="sadasd@sds.com", dateOfBirth=DateTime.UtcNow },
            };
        }



        public async Task<bool> AddItemAsync(Employee item)
        {
            employees.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(long id)
        {
            var oldItem = employees.Where((Employee arg) => arg.employeeId == id).FirstOrDefault();
            employees.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<Employee> GetItemAsync(long id)
        {
            return await Task.FromResult(employees.FirstOrDefault(s => s.employeeId == id));
        }

        public async Task<IEnumerable<Employee>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(employees);
        }

        public async Task<bool> UpdateItemAsync(Employee item)
        {
            var oldItem = employees.Where((Employee arg) => arg.employeeId == item.employeeId).FirstOrDefault();
            employees.Remove(oldItem);
            employees.Add(item);

            return await Task.FromResult(true);
        }
    }
}
