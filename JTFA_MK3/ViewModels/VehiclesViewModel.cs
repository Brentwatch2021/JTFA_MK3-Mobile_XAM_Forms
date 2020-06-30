using JTFA_MK3.Models;
using JTFA_MK3.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace JTFA_MK3.ViewModels
{
    public class VehiclesViewModel : BaseVehiclesViewModel
    {
        public ObservableCollection<Vehicle> Vehicles { get; set; }
        public Command LoadVehiclesCommand { get; set; }
        public VehiclesViewModel()
        {
            Title = "Browse";
            Vehicles = new ObservableCollection<Vehicle>();
            LoadVehiclesCommand = new Command(async () => await ExecuteLoadItemsCommand());

            // Addition Functionaility
            MessagingCenter.Subscribe<NewVehiclePage, Vehicle>(this, "AddVehicle", async (obj, item) =>
            {
                var newItem = item as Vehicle;
                Vehicles.Add(newItem);
                ///await App.VehicleDatabase.SaveItemAsync(newItem);
                await DataStore.AddItemAsync(newItem);
            });

            // Removal functionality
            MessagingCenter.Subscribe<VehiclesPage, Vehicle>(this, "RemoveVehicle", async (obj, item) => {
                var itemToDelete = item as Vehicle;
                Vehicles.Remove(itemToDelete);
                //await DataStore.DeleteItemAsync(itemToDelete.Vehicle_ID);
                await App.VehicleDatabase.DeleteItemAsync(itemToDelete);
            });
        }

        public async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Vehicles.Clear();
                // Get items from local sqlLite DB
                //var itemsFromLocalAppDb = await App.VehicleDatabase.GetItemsAsync();
                // Get Items from API in cloud
                var items = await DataStore.GetItemsAsync(true);
                foreach (var item in items)
                {
                    Vehicles.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
