using JTFA_MK3.Models.V2_Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace JTFA_MK3.ViewModels
{
    public class TaskDescriptionV3DetailViewModel
    {
        public TaskDescriptionV3 TaskDescription { get; set; }
        public TaskDescriptionV3DetailViewModel(TaskDescriptionV3 taskDescription = null)
        {
            this.TaskDescription = taskDescription;
            //Title = this._taskDescription.LabourCost;
            /*
            MessagingCenter.Subscribe<JobCardDetailPage, Job_Card>(this, "RemoveJobcard", async (obj, item) => {
                var itemToDelete = item as Job_Card;
                await DataStore.DeleteItemAsync(itemToDelete.JobCardID);
            });
            */
        }

    }
}
