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
    public class JobCardsViewModel : BaseJobCardsViewModel
    {
        public ObservableCollection<Job_Card> JobCards { get; set; }
        public Command LoadJobCardsCommand { get; set; }
        public JobCardsViewModel()
        {
            Title = "Browse";
            JobCards = new ObservableCollection<Job_Card>();
            LoadJobCardsCommand = new Command(async () => await ExecuteLoadJobCardsCommand());

            // Addition Functionaility
            MessagingCenter.Subscribe<NewJobCardPage, Job_Card>(this, "AddJobcard", async (obj, item) =>
            {
                var newItem = item as Job_Card;
                JobCards.Add(newItem);
                await DataStore.AddItemAsync(newItem);
            });

            // Update 
            MessagingCenter.Subscribe<NewJobCardPage, Job_Card>(this, "UpdateJobcard", async (obj, item) =>
             {
                 var jobCardToUpdate = item as Job_Card;
                 await DataStore.UpdateItemAsync(jobCardToUpdate);
             }
            );

            // Removal functionality
            MessagingCenter.Subscribe<JobCardsPage, Job_Card>(this, "RemoveJobCard", async (obj, item) => {
                var itemToDelete = item as Job_Card;
                JobCards.Remove(itemToDelete);
                await DataStore.DeleteItemAsync(itemToDelete.JobCardID);
            });


        }

        private void JobCards_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            throw new NotImplementedException();
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
