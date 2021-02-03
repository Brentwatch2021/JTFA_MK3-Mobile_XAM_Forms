using JTFA_MK3.Models.V2_Models;
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
    public class JobcardsV3ViewModel : BaseJobCardsV3Model
    {
        public ObservableCollection<JobCardV3> JobCards { get; set; }
        public Command LoadJobCardsCommand { get; set; }


        public JobcardsV3ViewModel()
        {
            Title = "Browse";
            JobCards = new ObservableCollection<JobCardV3>();
            LoadJobCardsCommand = new Command(async () => await ExecuteLoadJobCardsCommand());

            // Addition Functionaility
            MessagingCenter.Subscribe<NewJobCardV3Page, JobCardV3>(this, "AddJobcardV3", async (obj, item) =>
            {
                var newItem = item as JobCardV3;
                JobCards.Add(newItem);
                await DataStore.AddItemAsync(newItem);
            });

            // Removal functionality
            MessagingCenter.Subscribe<JTFA_Main_PageDetail, JobCardV3>(this, "RemoveJobCardV3", async (obj, item) => {
                var itemToDelete = item as JobCardV3;
                JobCards.Remove(itemToDelete);
                await DataStore.DeleteItemAsync(itemToDelete.JobCardID);
            });

            // Update 
            MessagingCenter.Subscribe<NewJobCardV3Page, JobCardV3>(this, "UpdateJobcardV3", async (obj, item) =>
            {
                var jobCardToUpdate = item as JobCardV3;
                await DataStore.UpdateItemAsync(jobCardToUpdate);
            }
            );


            MessagingCenter.Subscribe<JobCardsV3Page, JobCardV3>(this, "SendInvoice", async (obj, item) =>
            {
                try
                {
                    var jobCardToUpdate = item as JobCardV3;
                    await DataStore.SendInvoiceAsync(jobCardToUpdate);
                }
                catch (Exception ex)
                {
                    
                }
                
            }
            );

        }

        public async Task ExecuteLoadJobCardsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                JobCards.Clear();
                var items = await DataStore.GetItemsAsync(true);
                foreach (var item in items)
                {
                    JobCards.Add(item);
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
