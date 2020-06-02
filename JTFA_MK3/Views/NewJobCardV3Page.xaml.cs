using JTFA_MK3.Models;
using JTFA_MK3.Models.V2_Models;
using JTFA_MK3.Services;
using JTFA_MK3.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace JTFA_MK3.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewJobCardV3Page : ContentPage
    {
        public NewjobCardV3ViewModel newjobCardV3ViewModel;
        public JobCardV3 job_Card { get; set; }

        public JobCardV3 jobCardToBEUPDATED { get; set; }

        public List<Vehicle> VehiclesToSelect { get; set; }
        public IVehicleDataStore<Vehicle> DataStore => DependencyService.Get<IVehicleDataStore<Vehicle>>();

        private bool IsCreate = true;
        public NewJobCardV3Page(NewjobCardV3ViewModel  _viewModel)
        {
            InitializeComponent();

            job_Card = new JobCardV3()
            {
                JobCardName = "Service"
            };

            LoadVehicles();

            job_Card.TaskDescriptions = new List<TaskDescriptionV3>();

            BindingContext = this.newjobCardV3ViewModel = _viewModel;
        }


        private async void LoadVehicles()
        {
            VehiclesToSelect = new List<Vehicle>();
            // Get List of Vehicles from API
            var items = await DataStore.GetItemsAsync();
            foreach (var item in items)
            {
                VehiclesToSelect.Add(item);
            }
            P_Vehicles.ItemsSource = VehiclesToSelect;
        }


        public NewJobCardV3Page(JobCardV3 jobCardToBeUpdated, NewjobCardV3ViewModel _viewModel)
        {
            InitializeComponent();

            C_JobCardName.Text = jobCardToBeUpdated.JobCardName;
            

            if (jobCardToBeUpdated != null)
            {
                jobCardToBEUPDATED = jobCardToBeUpdated;
                _viewModel.JobCard = jobCardToBeUpdated;
                job_Card = jobCardToBeUpdated;
                IsCreate = false;
            }

            BindingContext = this.newjobCardV3ViewModel = _viewModel;

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            newjobCardV3ViewModel.TaskDescriptions.Clear();
            if (job_Card != null && job_Card.TaskDescriptions != null && job_Card.TaskDescriptions.Count > 0)
            {
                foreach (var item in job_Card.TaskDescriptions)
                {
                    newjobCardV3ViewModel.TaskDescriptions.Add(item);
                }
            }

            /* This is when the items is loaded via db in this case we only loading it with memory as it is 
             * not yet saved
            if (newjobCardV3ViewModel.TaskDescriptions.Count == 0)
            {
                newjobCardV3ViewModel.LoadTaskDescriptionsCommand.Execute(null);
            }
            */
        }


        private async void ToolbarItem_Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        private async void ToolbarItem_Save_Clicked(object sender, EventArgs e)
        {
            if (IsCreate)
            {
                job_Card.JobCardName = C_JobCardName.Text;
                job_Card.Vehicle = P_Vehicles?.SelectedItem as Vehicle;
                MessagingCenter.Send(this, "AddJobcardV3", job_Card);
                await Navigation.PopModalAsync();
            }
            else
            {
                // Update Item
                jobCardToBEUPDATED.JobCardName = C_JobCardName.Text;
                MessagingCenter.Send(this, "UpdateJobcardV3", jobCardToBEUPDATED);
                await Navigation.PopModalAsync();
            }

            
        }

        private async void Button_New_TaskDescription_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new NewTaskDescriptionV3Page(job_Card)));
        }

        private async void ItemsListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as TaskDescriptionV3;
            if (item == null)
                return;

            await Navigation.PushAsync(new TaskDescriptionV3DetailPage(new TaskDescriptionV3DetailViewModel(item)));

            // Manually deselect item.
            ItemsListView.SelectedItem = null;
        }
    }
}