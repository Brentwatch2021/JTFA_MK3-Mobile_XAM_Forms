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
    public partial class JTFA_ClientsPage : ContentPage
    {
        JTFA_ClientsViewModel viewModel;
        public JTFA_ClientsPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new JTFA_ClientsViewModel();
        }

        private async void ToolbarItemAdd_JTFA_Client_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new NewJTFA_ClientPage()));
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as JTFA_Client;
            if (item == null)
                return;

            await Navigation.PushAsync(new JTFA_ClientDetailPage(new JTFA_ClientDetailViewModel(item)));
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (viewModel.jTFA_Clients.Count == 0)
                viewModel.LoadJTFA_ClientsCommand.Execute(null);

        }
    }
}