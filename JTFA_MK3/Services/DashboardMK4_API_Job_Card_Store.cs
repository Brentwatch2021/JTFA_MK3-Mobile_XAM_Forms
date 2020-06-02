using JTFA_MK3.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace JTFA_MK3.Services
{
    public class DashboardMK4_API_Job_Card_Store : IJobCardDataStore<Job_Card>
    {
        System.Net.Http.HttpClient client;
        IEnumerable<Job_Card> jobCards;

        public DashboardMK4_API_Job_Card_Store()
        {
            client = new System.Net.Http.HttpClient();

            client.BaseAddress = new Uri($"{App.API_WEB_IP}/");

            jobCards = new List<Job_Card>();
        }

        bool IsConnected => Connectivity.NetworkAccess == NetworkAccess.Internet;
        public async Task<bool> AddItemAsync(Job_Card item)
        {
            if (item == null || !IsConnected)
                return false;

            //item.employeeId = 0;
            //item.email = "sdasd@sas.com";
            //item.dateOfBirth = DateTime.UtcNow;
            //item.lastName = "FromXamarin";
            //item.gender = "Male";
            //item.phoneNumbwer = "sdasjdajs";
            var serializedItem = JsonConvert.SerializeObject(item);
            //string correctedformat = "{\"employeeId\": 0,\"firstName\": \"From\",\"lastName\": \"Postman\",\"dateOfBirth\": \"1979 - 04 - 25T00: 00:00\",\"phoneNumber\": \"999 - 888 - 7777\",\"email\": \"uncle.bob @gmail.com\" }";

            try
            {
                var stringContent = new System.Net.Http.StringContent(serializedItem, Encoding.UTF8, "application/json");
                string stringContentstring = stringContent.ToString();
                var response = await client.PostAsync($"api/jobcard", stringContent);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {

            }

            return false;
        }

        public async Task<bool> DeleteItemAsync(Guid id)
        {
            if (id != null && !IsConnected)
                return false;

            //var response = "";
            //System.Net.HttpResponseMessage  dsd = new System.Net.HttpResponseMessage

            //try
            ///{
              var  response = await client.DeleteAsync($"api/jobcard/{id.ToString()}");
            //}
            //catch (Exception ex)
            //{
                
            //}
            

            return response.IsSuccessStatusCode;
        }

        public  async Task<Job_Card> GetItemAsync(Guid id)
        {
            if (id != null && IsConnected)
            {
                var json = await client.GetStringAsync($"api/employee/{id}");
                return await Task.Run(() => JsonConvert.DeserializeObject<Job_Card>(json));
            }

            return null;
        }

        public async Task<IEnumerable<Job_Card>> GetItemsAsync(bool forceRefresh = false)
        {
            if (forceRefresh && IsConnected)
            {
                try
                {
                    var json = await client.GetStringAsync($"api/jobcard");
                    jobCards = await Task.Run(() => JsonConvert.DeserializeObject<IEnumerable<Job_Card>>(json));
                }
                catch (Exception ex)
                {
                    Debug.Write(ex.Message);
                }
            }
            return jobCards;
        }

        public async Task<bool> UpdateItemAsync(Job_Card item)
        {
            if (item == null || item.JobCardID == null || !IsConnected)
                return false;

            var serializedItem = JsonConvert.SerializeObject(item);
            var stringContent = new System.Net.Http.StringContent(serializedItem, Encoding.UTF8, "application/json");
            Uri uriforUpdate = null;
            try
            {
               uriforUpdate = new Uri(string.Format("http://10.0.0.102:5555/api/jobcard/{0}", item.JobCardID.ToString()));
            }
            catch (Exception ex)
            { 
            }
             
            var response = await client.PutAsync(uriforUpdate, stringContent);

            return response.IsSuccessStatusCode;
        }
    }
}
