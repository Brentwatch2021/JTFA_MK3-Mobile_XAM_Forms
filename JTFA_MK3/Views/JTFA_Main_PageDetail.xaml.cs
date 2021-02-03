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
    public partial class JTFA_Main_PageDetail : ContentPage
    {
        JobcardsV3ViewModel viewModel;

        public JTFA_Main_PageDetail()
        {
            InitializeComponent();
            BindingContext = viewModel = new JobcardsV3ViewModel();
            Setup_Nav_Icons();
        }

        private void Setup_Nav_Icons()
        {/*
            NV_Add_JobCard_BTN.IconImageSource = ImageSource.FromUri(new Uri("https://upload.wikimedia.org/wikipedia/commons/thumb/f/fc/Papio_anubis_%28Serengeti%2C_2009%29.jpg/200px-Papio_anubis_%28Serengeti%2C_2009%29.jpg"));
            NV_Add_Car_BTN.IconImageSource = ImageSource.FromUri(new Uri("https://upload.wikimedia.org/wikipedia/commons/thumb/f/fc/Papio_anubis_%28Serengeti%2C_2009%29.jpg/200px-Papio_anubis_%28Serengeti%2C_2009%29.jpg"));
            NV_Add_Client_BTN.IconImageSource = ImageSource.FromUri(new Uri("https://upload.wikimedia.org/wikipedia/commons/thumb/f/fc/Papio_anubis_%28Serengeti%2C_2009%29.jpg/200px-Papio_anubis_%28Serengeti%2C_2009%29.jpg"));
            NV_Add_Invoice_BTN.IconImageSource = ImageSource.FromUri(new Uri("https://upload.wikimedia.org/wikipedia/commons/thumb/f/fc/Papio_anubis_%28Serengeti%2C_2009%29.jpg/200px-Papio_anubis_%28Serengeti%2C_2009%29.jpg"));
            */
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            
            viewModel.LoadJobCardsCommand.Execute(null);
        }


        public async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as JobCardV3;
            if (item == null)
                return;

            await Navigation.PushAsync(new JobCardV3DetailPage(new JobCardV3DetailViewModel(item)));
            
            // Manually deselect item.
            ItemsListViewV2.SelectedItem = null;
        }


        public async void AddJobCard_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new NewJobCardV3Page(new NewjobCardV3ViewModel())));
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