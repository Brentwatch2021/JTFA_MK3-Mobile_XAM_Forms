using JTFA_MK3.Models;
using JTFA_MK3.Models.V2_Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace JTFA_MK3.Services.V3_Services
{
    public class DashboardMK4_API_JobCardV3_DataStore : IIJobCardV3DataStore<JobCardV3>
    {
        System.Net.Http.HttpClient client;
        IEnumerable<JobCardV3> jobCards;

        public DashboardMK4_API_JobCardV3_DataStore()
        {
            this.client = new System.Net.Http.HttpClient();

            this.client.BaseAddress = new Uri($"{App.API_WEB_IP}/");

            jobCards = new List<JobCardV3>();
        }

        bool IsConnected => Connectivity.NetworkAccess == NetworkAccess.Internet;



        public async  Task<bool> AddItemAsync(JobCardV3 item)
        {
            if (item == null || !IsConnected)
            {
                return false;
            }
                
            var serializedItem = JsonConvert.SerializeObject(item);

            try
            {
                var stringContent = new System.Net.Http.StringContent(serializedItem, Encoding.UTF8, "application/json");
                string stringContentstring = stringContent.ToString();
                var response = await client.PostAsync($"api/1/jobcardV3", stringContent);
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
            var response = await client.DeleteAsync($"api/1/jobcardV3/{id.ToString()}");
            //}
            //catch (Exception ex)
            //{

            //}


            return response.IsSuccessStatusCode;
        }

        public async Task<JobCardV3> GetItemAsync(Guid id)
        {
            JobCardV3 JobCard = null;
            if (IsConnected)
            {
                try
                {
                    var json = await client.GetStringAsync($"api/1/jobcardV3/{id}");
                    JobCard = await Task.Run(() => JsonConvert.DeserializeObject<JobCardV3>(json));
                }
                catch (Exception ex)
                {
                    Debug.Write(ex.Message);
                }
            }
            return JobCard;
        }

        public async Task<IEnumerable<JobCardV3>> GetItemsAsync(bool forceRefresh = false)
        {
            if (forceRefresh && IsConnected)
            {
                try
                {
                    var json = await client.GetStringAsync($"api/1/jobcardV3");
                    jobCards = await Task.Run(() => JsonConvert.DeserializeObject<IEnumerable<JobCardV3>>(json));
                }
                catch (Exception ex)
                {
                    Debug.Write(ex.Message);
                }
            }
            return jobCards;
        }

        public async Task<bool> UpdateItemAsync(JobCardV3 item)
        {
            if (item == null || item.JobCardID == null || !IsConnected)
                return false;

            var serializedItem = JsonConvert.SerializeObject(item);
            var stringContent = new System.Net.Http.StringContent(serializedItem, Encoding.UTF8, "application/json");
            Uri uriforUpdate = null;
            try
            {
                uriforUpdate = new Uri(string.Format("{0}api/1/jobcardV3/{1}", client.BaseAddress.ToString(), item.JobCardID.ToString()));
            }
            catch (Exception ex)
            {
            }
            
            var response = await client.PutAsync(uriforUpdate, stringContent);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> SendInvoiceAsync(JobCardV3 item)
        {
            if (item == null || item.JobCardID == null || !IsConnected)
                return false;

            // Create Mail Request object
            Mail_Request mailRequest = new Mail_Request();
            mailRequest.JobCardID = item.JobCardID.ToString();
            mailRequest.Subject = "SC XXX";

            var serializedItem = JsonConvert.SerializeObject(mailRequest);
            var stringContent = new System.Net.Http.StringContent(serializedItem, Encoding.UTF8, "application/json");
            Uri uriforUpdate = null;
            try
            {
                uriforUpdate = new Uri("http://10.0.0.102:5555/api/1/mail");
            }
            catch (Exception ex)
            {
            }

            var response = await client.PostAsync(uriforUpdate, stringContent);

            return response.IsSuccessStatusCode;
        }
    }
}
