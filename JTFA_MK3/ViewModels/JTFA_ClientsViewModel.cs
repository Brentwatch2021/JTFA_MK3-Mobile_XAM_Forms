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
    public class JTFA_ClientsViewModel : BaseJTFA_ClientsViewModel
    {
        public ObservableCollection<JTFA_Client> jTFA_Clients { get; set; }
        public Command LoadJTFA_ClientsCommand { get; set; }
        public JTFA_ClientsViewModel()
        {
            Title = "Browse";
            jTFA_Clients = new ObservableCollection<JTFA_Client>();
            LoadJTFA_ClientsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            // Addition Functionaility
            MessagingCenter.Subscribe<NewJTFA_ClientPage, JTFA_Client>(this, "AddJTFA_Client", async (obj, item) =>
            {
                var newItem = item as JTFA_Client;
                jTFA_Clients.Add(newItem);
                await DataStore.AddItemAsync(newItem);
            });

            // Removal functionality
            MessagingCenter.Subscribe<JTFA_ClientsPage, JTFA_Client>(this, "RemoveJTFA_Client", async (obj, item) => {
                var itemToDelete = item as JTFA_Client;
                jTFA_Clients.Remove(itemToDelete);
                await DataStore.DeleteItemAsync(itemToDelete.JTFA_CLIENT_ID);
            });
        }

        public async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                jTFA_Clients.Clear();
                var items = await DataStore.GetItemsAsync(true);
                foreach (var item in items)
                {
                    jTFA_Clients.Add(item);
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
