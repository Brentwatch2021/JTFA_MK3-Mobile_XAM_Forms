using JTFA_MK3.Models;
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
    public class DashboardMK4_API_JTFA_Invoice_DataStore : IJTFA_Invoice_DataStore<JTFA_Invoice>
    {
        private System.Net.Http.HttpClient client;
        private IEnumerable<JTFA_Invoice> jtfa_Invoices;

        public DashboardMK4_API_JTFA_Invoice_DataStore()
        {
            this.client = new System.Net.Http.HttpClient();
            this.client.BaseAddress = new Uri($"{App.API_WEB_IP}/");
            this.jtfa_Invoices = jtfa_Invoices;
        }

        private bool IsConnected => Connectivity.NetworkAccess == NetworkAccess.Internet;

        public async Task<bool> AddItemAsync(JTFA_Invoice item)
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
                var response = await client.PostAsync($"api/1/JTFA_Invoice", stringContent);
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

            
            var response = await client.DeleteAsync($"api/1/JTFA_Invoice/{id.ToString()}");
            return response.IsSuccessStatusCode;
        }

        public async Task<JTFA_Invoice> GetItemAsync(Guid id)
        {
            JTFA_Invoice jtfa_Invoice = null;
            if (IsConnected)
            {
                try
                {
                    if (id != null && id != Guid.Empty)
                    {
                        var json = await client.GetStringAsync($"api/1/JTFA_Invoice/{id}");
                        jtfa_Invoice = await Task.Run(() => JsonConvert.DeserializeObject<JTFA_Invoice>(json));
                    }
                }
                catch (Exception ex)
                {
                    Debug.Write(ex.Message);
                }
            }
            return jtfa_Invoice;
        }

        public async Task<IEnumerable<JTFA_Invoice>> GetItemsAsync(bool forceRefresh = false)
        {
            if (forceRefresh && IsConnected)
            {
                try
                {
                    var json = await client.GetStringAsync($"api/1/JTFA_Invoice");
                    jtfa_Invoices = await Task.Run(() => JsonConvert.DeserializeObject<IEnumerable<JTFA_Invoice>>(json));
                }
                catch (Exception ex)
                {
                    Debug.Write(ex.Message);
                }
            }
            return jtfa_Invoices;
        }

        public async Task<bool> UpdateItemAsync(JTFA_Invoice item)
        {
            if (item == null || item.JTFA_Invoice_ID == null || !IsConnected)
                return false;

            var serializedItem = JsonConvert.SerializeObject(item);
            var stringContent = new System.Net.Http.StringContent(serializedItem, Encoding.UTF8, "application/json");
            Uri uriforUpdate = null;
            try
            {
                uriforUpdate = new Uri(string.Format("{0}api/1/JTFA_Invoice/{1}", client.BaseAddress.ToString(), item.JTFA_Invoice_ID.ToString()));
            }
            catch (Exception ex)
            {
            }

            var response = await client.PutAsync(uriforUpdate, stringContent);

            return response.IsSuccessStatusCode;
        }
    }
}
