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
    public partial class JTFA_Invoices_Page : ContentPage
    {

        private JTFA_Invoice_ViewModel viewModel;

        public JTFA_Invoices_Page()
        {
            InitializeComponent();
            BindingContext = viewModel = new JTFA_Invoice_ViewModel();
        }


        protected override void OnAppearing()
        {
            base.OnAppearing();
            this.viewModel.LoadJTFA_InvoicesCommand.Execute(null);
        }

        public async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as JTFA_Invoice;
            if (item == null)
                return;

            await Navigation.PushAsync(new JTFA_Invoice_Detail_Page(new JTFA_Invoice_Detail_Page_View_Model(item)));

            // Manually deselect item.
            ItemsListView.SelectedItem = null;
        }

        public async void AddJTFA_Invoice_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new New_JTFA_Invoice_Page(new New_JTFA_Invoice_ViewModel())));
        }
        private void MenuItem_Delete_Clicked(object sender, EventArgs e)
        {
            var item = ((MenuItem)sender);
            var jTFA_Invoice_CmdPar = item.CommandParameter as JTFA_Invoice;
            MessagingCenter.Send(this, "Remove_JTFA_Invoice", jTFA_Invoice_CmdPar);
        }

        private void MenuItem_Update_Clicked(object sender, EventArgs e)
        {
            var item = ((MenuItem)sender);
            var jTFA_Invoice = item.CommandParameter as JTFA_Invoice;
            Navigation.PushModalAsync(new NavigationPage(new New_JTFA_Invoice_Page(jTFA_Invoice, new New_JTFA_Invoice_ViewModel())));
        }

    }
}