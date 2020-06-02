using JTFA_MK3.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace JTFA_MK3.Services
{
    public class DashboardMK4_API_Employee_Store : IEmployeeStore<Employee>
    {
        System.Net.Http.HttpClient client;
        IEnumerable<Employee> employees;

        public DashboardMK4_API_Employee_Store()
        {
            client = new System.Net.Http.HttpClient();
            client.BaseAddress = new Uri($"{App.API_WEB_IP}/");

            employees = new List<Employee>();
        }

        bool IsConnected => Connectivity.NetworkAccess == NetworkAccess.Internet;

        public async Task<bool> AddItemAsync(Employee item)
        {
            // Adding this line of code solved the issue of not able to connect to the API server hosted in IIS Where the Post Request worked the Get worked 
            // However with some random testing we added the delete item to the add method and somehow now the getstrings async works how no
            // clue but its working 
            //this.DeleteItemAsync(1);
            
            if (item == null || !IsConnected)
                return false;

            item.employeeId = 0;
            //item.email = "sdasd@sas.com";
            item.dateOfBirth = DateTime.UtcNow;
            //item.lastName = "FromXamarin";
            //item.gender = "Male";
            //item.phoneNumbwer = "sdasjdajs";
            var serializedItem = JsonConvert.SerializeObject(item);
            string correctedformat = "{\"employeeId\": 0,\"firstName\": \"From\",\"lastName\": \"Postman\",\"dateOfBirth\": \"1979 - 04 - 25T00: 00:00\",\"phoneNumber\": \"999 - 888 - 7777\",\"email\": \"uncle.bob @gmail.com\" }";

            try
            {
                var stringContent = new System.Net.Http.StringContent(serializedItem, Encoding.UTF8, "application/json");
                string stringContentstring = stringContent.ToString();
                var response = await client.PostAsync($"api/employee",stringContent);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                
            }
            
            return false;
        }

        public async Task<bool> DeleteItemAsync(long id)
        {
            if (id != 0 && !IsConnected)
                return false;

            var response = await client.DeleteAsync($"api/employee/{id}");

            return response.IsSuccessStatusCode;
        }

        public async Task<Employee> GetItemAsync(long id)
        {
            if (id != null && IsConnected)
            {
                var json = await client.GetStringAsync($"api/employee/{id}");
                return await Task.Run(() => JsonConvert.DeserializeObject<Employee>(json));
            }

            return null;
        }

        public async Task<IEnumerable<Employee>> GetItemsAsync(bool forceRefresh = false)
        {
            if (forceRefresh && IsConnected)
            {
                try
                {
                    var json = await client.GetStringAsync($"api/employee");
                    employees = await Task.Run(() => JsonConvert.DeserializeObject<IEnumerable<Employee>>(json));
                }
                catch (Exception ex)
                {
                    
                }
            }
            return employees;
        }

        public async Task<bool> UpdateItemAsync(Employee item)
        {
            if (item == null || item.employeeId == 0 || !IsConnected)
                return false;

            var serializedItem = JsonConvert.SerializeObject(item);
            var buffer = Encoding.UTF8.GetBytes(serializedItem);
            var byteContent = new System.Net.Http.ByteArrayContent(buffer);

            var response = await client.PutAsync(new Uri($"api/employee/{item.employeeId}"), byteContent);

            return response.IsSuccessStatusCode;
        }
    }
}
