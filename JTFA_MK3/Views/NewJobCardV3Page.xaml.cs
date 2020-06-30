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

        public List<JTFA_Client> JTFA_ClientsToSelect { get; set; }

        public IVehicleDataStore<Vehicle> DataStore => DependencyService.Get<IVehicleDataStore<Vehicle>>();

        public IJTFA_ClientDataStore<JTFA_Client> JTFA_C_DataStore => DependencyService.Get<IJTFA_ClientDataStore<JTFA_Client>>();

        private bool IsCreate = true;


        public NewJobCardV3Page(NewjobCardV3ViewModel  _viewModel)
        {
            InitializeComponent();

            job_Card = new JobCardV3()
            {
                JobCardName = "Service"
            };

            LoadVehicles();

            LoadJTFA_Clients();

            job_Card.TaskDescriptions = new List<TaskDescriptionV3>();

            BindingContext = this.newjobCardV3ViewModel = _viewModel;
        }

        public NewJobCardV3Page(JobCardV3 jobCardToBeUpdated, NewjobCardV3ViewModel _viewModel)
        {
            InitializeComponent();

            C_JobCardName.Text = jobCardToBeUpdated?.JobCardName;
            LoadVehicles(jobCardToBeUpdated?.Vehicle?.Vehicle_ID.ToString());
            var jtfaClientID = jobCardToBeUpdated?.JTFA_Client?.JTFA_CLIENT_ID.ToString();
            LoadJTFA_Clients(jtfaClientID);

            if (jobCardToBeUpdated != null)
            {
                if (jobCardToBeUpdated.TaskDescriptions == null)
                {
                    jobCardToBeUpdated.TaskDescriptions = new List<TaskDescriptionV3>();
                }
                jobCardToBEUPDATED = jobCardToBeUpdated;
                _viewModel.JobCard = jobCardToBeUpdated;
                job_Card = jobCardToBeUpdated;
                IsCreate = false;
            }



            BindingContext = this.newjobCardV3ViewModel = _viewModel;

        }


        private async void LoadJTFA_Clients(string jtfa_clientID = "")
        {
            JTFA_ClientsToSelect = new List<JTFA_Client>();
            var jtfa_Clients = await JTFA_C_DataStore.GetItemsAsync();

            var selectedIndex = -1;
            for (int i = 0; i < jtfa_Clients.Count(); i++)
            {
                JTFA_Client jtfa_Client = jtfa_Clients.ElementAt(i) as JTFA_Client;
                JTFA_ClientsToSelect.Add(jtfa_Client);
                if (jtfa_Client.JTFA_CLIENT_ID.ToString() == jtfa_clientID)
                {
                    selectedIndex = i;
                }
            }
            P_Clients.ItemsSource = JTFA_ClientsToSelect;
            P_Clients.SelectedIndex = selectedIndex;
        }

        private async void LoadVehicles(string vehicleID = "")
        {
            VehiclesToSelect = new List<Vehicle>();
            // Get List of Vehicles from API
            var items = await DataStore.GetItemsAsync();
            var selectedItemIndex = -1;
            for(int i = 0; i < items.Count(); i++)
            {
                Vehicle vehicle = items.ElementAt(i) as Vehicle;
                VehiclesToSelect.Add(vehicle);
                if (vehicle.Vehicle_ID.ToString() == vehicleID)
                {
                    selectedItemIndex = i;
                }
            }
            P_Vehicles.ItemsSource = VehiclesToSelect;
            P_Vehicles.SelectedIndex = selectedItemIndex;
        }


        

        protected override void OnAppearing()
        {
            base.OnAppearing();

            newjobCardV3ViewModel.TaskDescriptions.Clear();
            if (job_Card != null && job_Card.TaskDescriptions != null && job_Card.TaskDescriptions.Count > 0)
            {
                foreach (var item in job_Card.TaskDescriptions)
                {
                    // There is a bug with Xam forms where the property dooes not update unless INotifyPropertyChanged is
                    // not implemented correctly but this seems to be the fastest solution 
                    var newItem = new TaskDescriptionV3();
                    newItem.Description = item.Description;
                    newItem.Task_Description_ID = item.Task_Description_ID;
                    newItem.PartsPrice = item.PartsPrice;
                    newItem.TotalTaskCost = item.TotalTaskCost;
                    newItem.LabourCost = item.LabourCost;
                    newjobCardV3ViewModel.TaskDescriptions.Add(newItem);
                }
            }

            //newjobCardV3ViewModel.ExecuteLoadTaskDescriptionsCommand();
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
                job_Card.JTFA_Client = P_Clients.SelectedItem as JTFA_Client;
                MessagingCenter.Send(this, "AddJobcardV3", job_Card);
                await Navigation.PopModalAsync();
            }
            else
            {
                // Update Item
                jobCardToBEUPDATED.JobCardName = C_JobCardName.Text;
                jobCardToBEUPDATED.Vehicle = P_Vehicles?.SelectedItem as Vehicle;
                jobCardToBEUPDATED.JTFA_Client = P_Clients.SelectedItem as JTFA_Client;
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


        public void MenuItem_Delete_Clicked(object sender, EventArgs e)
        {
            var item = ((MenuItem)sender);
            var taskDescription = item.CommandParameter as TaskDescriptionV3;
            jobCardToBEUPDATED.TaskDescriptions.Remove(taskDescription);
            newjobCardV3ViewModel.TaskDescriptions.Remove(taskDescription);
        }

        public async void MenuItem_Update_Clicked(object sender, EventArgs e)
        {
            var item = ((MenuItem)sender);
            var taskDescription = item.CommandParameter as TaskDescriptionV3;
            await Navigation.PushAsync(new NewTaskDescriptionV3Page(job_Card.TaskDescriptions.Where(td => td.Task_Description_ID == taskDescription.Task_Description_ID).FirstOrDefault()));
        }
        
    }
}