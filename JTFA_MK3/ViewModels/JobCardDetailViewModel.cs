using JTFA_MK3.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using JTFA_MK3.Views;

namespace JTFA_MK3.ViewModels
{
    public class JobCardDetailViewModel : BaseJobCardsViewModel
    {
        public Job_Card JobCard { get; set; }
        public JobCardDetailViewModel(Job_Card jobCard = null)
        {
            JobCard = jobCard;
            Title = jobCard.Name;

            MessagingCenter.Subscribe<JobCardDetailPage, Job_Card>(this, "RemoveJobcard", async (obj, item) => {
                var itemToDelete = item as Job_Card;
                await DataStore.DeleteItemAsync(itemToDelete.JobCardID);
            });
        }
    }
}
