using JTFA_MK3.Models.V2_Models;
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
    public partial class JobCardV3DetailPage : ContentPage
    {
        JobCardV3DetailViewModel viewModel;

        public JobCardV3DetailPage(JobCardV3DetailViewModel _viewModel)
        {
            InitializeComponent();

            BindingContext = this.viewModel = _viewModel;

            B_Vehicle_Display.Text = _viewModel?.JobCard?.Vehicle?.ToString() == null ? "No Vehicle Selected" : _viewModel?.JobCard?.Vehicle?.ToString();
            B_JTFA_Client_Display.Text = _viewModel?.JobCard?.JTFA_Client?.ToString() == null ? "No Client Selected" : _viewModel?.JobCard?.JTFA_Client?.ToString();
        }

        public JobCardV3DetailPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.TaskDescriptions.Count == 0)
            viewModel.LoadTaskDescriptionsCommand.Execute(null);
        }

        public async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as TaskDescriptionV3;
            if (item == null)
                return;

            await Navigation.PushAsync(new TaskDescriptionV3DetailPage(new TaskDescriptionV3DetailViewModel(item)));

            // Manually deselect item.
            ItemsListView.SelectedItem = null;
        }

        private async void B_Vehicle_Display_Clicked(object sender, EventArgs e)
        {
            var item = this.viewModel?.JobCard?.Vehicle;
            if (item == null)
                return;

            await Navigation.PushAsync(new VehicleDetailPage(new VehicleDetailViewModel(item)));
        }

        private async void B_JTFA_Client_Display_Clicked(object sender, EventArgs e)
        {
            var item = this.viewModel?.JobCard?.JTFA_Client;
            if (item == null)
                return;

            await Navigation.PushAsync(new JTFA_ClientDetailPage(new JTFA_ClientDetailViewModel(item)));
        }

    }
}