using JTFA_MK3.Models;
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
    public partial class VehiclesPage : ContentPage
    {
        VehiclesViewModel viewModel;
        public VehiclesPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new VehiclesViewModel();
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as Vehicle;
            if (item == null)
                return;

            await Navigation.PushAsync(new VehicleDetailPage(new VehicleDetailViewModel(item)));

        }


        private async void ToolbarItem_AddVehicle_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new NewVehiclePage()));
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (viewModel.Vehicles.Count == 0)
                viewModel.LoadVehiclesCommand.Execute(null);



        }
    }
}