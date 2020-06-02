using JTFA_MK3.Models;
using JTFA_MK3.ViewModels;
using JTFA_MK3.Views.Login;
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
    public partial class JobCardsPage : ContentPage
    {
        JobCardsViewModel viewModel;

        public JobCardsPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new JobCardsViewModel();
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as Job_Card;
            if (item == null)
                return;

            await Navigation.PushAsync(new JobCardDetailPage(new JobCardDetailViewModel(item)));

            // Manually deselect item.
            ItemsListView.SelectedItem = null;
        }

        async void AddJobCard_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new NewJobCardPage()));
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            //if (viewModel.JobCards.Count == 0)
                viewModel.LoadJobCardsCommand.Execute(null);
        }

        private void MenuItem_Delete_Clicked(object sender, EventArgs e)
        {
            var item = ((MenuItem)sender);
            var jobCardCmdPar = item.CommandParameter as Job_Card;
            MessagingCenter.Send(this, "RemoveJobCard", jobCardCmdPar);
            //viewModel.JobCards.Remove(jobCardCmdPar);
            //await Navigation.PopAsync();
            //viewModel.LoadJobCardsCommand.Execute(null);
        }

        private void MenuItem_Update_Clicked(object sender, EventArgs e)
        {
            var item = ((MenuItem)sender);
            var jobCardCmdPar = item.CommandParameter as Job_Card;

            

            Navigation.PushModalAsync(new NavigationPage(new NewJobCardPage(jobCardCmdPar)));
        }

        public async void ToolbarItem_Login_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new LoginWithSocialIconPage()));
        }
    }
}