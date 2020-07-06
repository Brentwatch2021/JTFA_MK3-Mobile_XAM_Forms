using JTFA_MK3.Models.V2_Models;
using JTFA_MK3.ViewModels;
using Plugin.Media;
using Plugin.Permissions.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace JTFA_MK3.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class JobCardsV3Page : ContentPage
    {
        JobcardsV3ViewModel viewModel;
        public JobCardsV3Page()
        {
            InitializeComponent();

            BindingContext = viewModel = new JobcardsV3ViewModel();
        }

       public async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as JobCardV3;
            if (item == null)
                return;

            await Navigation.PushAsync(new JobCardV3DetailPage(new JobCardV3DetailViewModel(item)));

            // Manually deselect item.
            ItemsListView.SelectedItem = null;
        }

        public async void AddJobCard_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new NewJobCardV3Page(new NewjobCardV3ViewModel())));
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        
            // BUG #4
            // This needs to be altered as the property name does not update when items name is updated
            // at the moment we just reload the entire list which is not right for real world
            //
            //if (viewModel.JobCards.Count == 0)
            viewModel.LoadJobCardsCommand.Execute(null);
        
        }

        private void MenuItem_Delete_Clicked(object sender, EventArgs e)
        {
            var item = ((MenuItem)sender);
            var jobCardCmdPar = item.CommandParameter as JobCardV3;
            MessagingCenter.Send(this, "RemoveJobCardV3", jobCardCmdPar);
        }

        private void MenuItem_Update_Clicked(object sender, EventArgs e)
        {
            var item = ((MenuItem)sender);
            var jobCardCmdPar = item.CommandParameter as JobCardV3;
            Navigation.PushModalAsync(new NavigationPage(new NewJobCardV3Page(jobCardCmdPar, new NewjobCardV3ViewModel())));
        }

        
        

    }
}