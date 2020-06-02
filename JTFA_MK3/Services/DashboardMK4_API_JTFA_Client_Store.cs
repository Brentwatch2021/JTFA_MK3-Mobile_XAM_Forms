using JTFA_MK3.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace JTFA_MK3.Services
{
    public class DashboardMK4_API_JTFA_Client_Store : IJTFA_ClientDataStore<JTFA_Client>
    {
        System.Net.Http.HttpClient client;
        IEnumerable<JTFA_Client> jtfa_Clients;

        public DashboardMK4_API_JTFA_Client_Store()
        {
            client = new System.Net.Http.HttpClient();

            client.BaseAddress = new Uri($"{App.API_WEB_IP}/");

            jtfa_Clients = new List<JTFA_Client>();
        }

        bool IsConnected => Connectivity.NetworkAccess == NetworkAccess.Internet;

        public async Task<bool> AddItemAsync(JTFA_Client jTFA_Client)
        {
            if (jTFA_Client == null || !IsConnected)
                return false;

            var serializedItem = JsonConvert.SerializeObject(jTFA_Client);

            try
            {
                var stringContent = new System.Net.Http.StringContent(serializedItem, Encoding.UTF8, "application/json");
                string stringContentstring = stringContent.ToString();
                var response = await client.PostAsync($"api/jtfa_client", stringContent);
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

            var response = await client.DeleteAsync($"api/jtfa_client{id}");

            return response.IsSuccessStatusCode;
        }

        public async Task<JTFA_Client> GetItemAsync(Guid id)
        {
            if (id != null && IsConnected)
            {
                var json = await client.GetStringAsync($"api/jtfa_client/{id}");
                return await Task.Run(() => JsonConvert.DeserializeObject<JTFA_Client>(json));
            }

            return null;
        }

        public async Task<IEnumerable<JTFA_Client>> GetItemsAsync(bool forceRefresh = false)
        {
            if (forceRefresh && IsConnected)
            {
                try
                {
                    var json = await client.GetStringAsync($"api/jtfa_client");
                    jtfa_Clients = await Task.Run(() => JsonConvert.DeserializeObject<IEnumerable<JTFA_Client>>(json));
                }
                catch (Exception ex)
                {
                    Debug.Write(ex.Message);
                }
            }
            return jtfa_Clients;
        }

        public async Task<bool> UpdateItemAsync(JTFA_Client item)
        {
            if (item == null || item.JTFA_CLIENT_ID == null || !IsConnected)
                return false;

            var serializedItem = JsonConvert.SerializeObject(item);
            var buffer = Encoding.UTF8.GetBytes(serializedItem);
            var byteContent = new System.Net.Http.ByteArrayContent(buffer);

            var response = await client.PutAsync(new Uri($"api/jtfa_client/{item.JTFA_CLIENT_ID}"), byteContent);

            return response.IsSuccessStatusCode;
        }
    }
}
