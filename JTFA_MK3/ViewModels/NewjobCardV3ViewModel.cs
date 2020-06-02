using JTFA_MK3.Models.V2_Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using Xamarin.Forms;

namespace JTFA_MK3.ViewModels
{
    public class NewjobCardV3ViewModel : BaseJobCardsV3Model
    {
        public JobCardV3 JobCard { get; set; }

        public ObservableCollection<TaskDescriptionV3> TaskDescriptions { get; set; }

        public Command LoadTaskDescriptionsCommand { get; set; }

        public NewjobCardV3ViewModel(JobCardV3 jobCard = null)
        {
            this.JobCard = jobCard;
            this.Title = jobCard?.JobCardName;
            this.TaskDescriptions = new ObservableCollection<TaskDescriptionV3>();
            LoadTaskDescriptionsCommand = new Command(() => ExecuteLoadTaskDescriptionsCommand());
        }

        public void ExecuteLoadTaskDescriptionsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                TaskDescriptions.Clear();
                if (JobCard != null && JobCard.TaskDescriptions != null && JobCard.TaskDescriptions.Count > 0)
                {
                    foreach (var item in JobCard.TaskDescriptions)
                    {
                        TaskDescriptions.Add(item);
                    }
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
