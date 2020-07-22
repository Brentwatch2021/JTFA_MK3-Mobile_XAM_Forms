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
    public class DashboardMK4_API_Vehicle_Store : IVehicleDataStore<Vehicle>
    {
        System.Net.Http.HttpClient client;
        IEnumerable<Vehicle> vehicles;

        public DashboardMK4_API_Vehicle_Store()
        {
            client = new System.Net.Http.HttpClient();

            client.BaseAddress = new Uri($"{App.API_WEB_IP}/");

            vehicles = new List<Vehicle>();
        }

        bool IsConnected => Connectivity.NetworkAccess == NetworkAccess.Internet;


        public async Task<bool> AddItemAsync(Vehicle vehicle)
        {
            if (vehicle == null || !IsConnected)
                return false;

            var serializedItem = JsonConvert.SerializeObject(vehicle);
            //string correctedformat = "{\"employeeId\": 0,\"firstName\": \"From\",\"lastName\": \"Postman\",\"dateOfBirth\": \"1979 - 04 - 25T00: 00:00\",\"phoneNumber\": \"999 - 888 - 7777\",\"email\": \"uncle.bob @gmail.com\" }";

            try
            {
                var stringContent = new System.Net.Http.StringContent(serializedItem, Encoding.UTF8, "application/json");
                string stringContentstring = stringContent.ToString();
                var response = await client.PostAsync($"api/1/vehicle", stringContent);
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

            var response = await client.DeleteAsync($"api/1/vehicle{id}");

            return response.IsSuccessStatusCode;
        }

        public async Task<Vehicle> GetItemAsync(Guid id)
        {
            if (id != null && IsConnected)
            {
                var json = await client.GetStringAsync($"api/1/vehicle/{id}");
                return await Task.Run(() => JsonConvert.DeserializeObject<Vehicle>(json));
            }

            return null;

        }


        public async Task<IEnumerable<Vehicle>> GetItemsAsync(bool forceRefresh = false)
        {
            if (forceRefresh && IsConnected)
            {
                try
                {
                    var json = await client.GetStringAsync($"api/1/vehicle");
                    vehicles = await Task.Run(() => JsonConvert.DeserializeObject<IEnumerable<Vehicle>>(json));
                }
                catch (Exception ex)
                {
                    Debug.Write(ex.Message);
                }
            }
            return vehicles;

        }

        public async Task<bool> UpdateItemAsync(Vehicle item)
        {
            if (item == null || item.Vehicle_ID == null || !IsConnected)
                return false;

            var serializedItem = JsonConvert.SerializeObject(item);
            var buffer = Encoding.UTF8.GetBytes(serializedItem);
            var byteContent = new System.Net.Http.ByteArrayContent(buffer);

            var response = await client.PutAsync(new Uri($"api/1/vehicle/{item.Vehicle_ID}"), byteContent);

            return response.IsSuccessStatusCode;
        }

    }
}
