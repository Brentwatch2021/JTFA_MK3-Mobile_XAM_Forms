using JTFA_MK3.Models;
using JTFA_MK3.Models.V2_Models;
using JTFA_MK3.Services.V3_Services;
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
    public partial class New_JTFA_Invoice_Page : ContentPage
    {
        public JTFA_Invoice jTFA_Invoice { get; set; }

        public List<JobCardV3> jobCardsToSelect { get; set; }

        public IIJobCardV3DataStore<JobCardV3> DataStore => DependencyService.Get<IIJobCardV3DataStore<JobCardV3>>();

        private bool IsCreate = true;

        // Update Constructor
        public New_JTFA_Invoice_Page(JTFA_Invoice jTFA_Invoice, New_JTFA_Invoice_ViewModel viewModel)
        {
            InitializeComponent();
            if (jTFA_Invoice != null)
            {
                this.jTFA_Invoice = jTFA_Invoice;
                this.L_Invoice_Number.Text = jTFA_Invoice.INV_Number.ToString();
                this.E_Email_Recipients.Text = jTFA_Invoice.Email_Recipients;
            }
            IsCreate = false;
            LoadJobCards(jTFA_Invoice?.JobCardID.ToString());
        }

        // Adding Constructor
        public New_JTFA_Invoice_Page(New_JTFA_Invoice_ViewModel viewModel)
        {
            InitializeComponent();
            LoadJobCards();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        private async void LoadJobCards(string jobCardID = "")
        {
            jobCardsToSelect = new List<JobCardV3>();
            // Get List of Vehicles from API
            var items = await DataStore.GetItemsAsync();
            var selectedItemIndex = -1;
            for (int i = 0; i < items.Count(); i++)
            {
                JobCardV3 jobCard = items.ElementAt(i) as JobCardV3;
                jobCardsToSelect.Add(jobCard);
                if (jobCardID != null && jobCardID != string.Empty && jobCard?.JobCardID.ToString() == jobCardID)
                {
                    selectedItemIndex = i;
                }
            }
            P_JobCards.ItemsSource = jobCardsToSelect;
            P_JobCards.SelectedIndex = selectedItemIndex;
        }


        private async void ToolbarItem_Save_Clicked(object sender, EventArgs e)
        {
            
            if (IsCreate)
            {
                var new_Invoice = new JTFA_Invoice();
                new_Invoice.Email_Recipients = E_Email_Recipients.Text;
                JobCardV3 jobCardselected = P_JobCards.SelectedItem as JobCardV3;
                new_Invoice.JobCardID = jobCardselected.JobCardID;
                MessagingCenter.Send(this, "Add_JTFA_Invoice", new_Invoice);
                await Navigation.PopModalAsync();
            }
            else
            {
                // Update Item
                this.jTFA_Invoice.Email_Recipients = E_Email_Recipients.Text;
                JobCardV3 jobCardselected = P_JobCards.SelectedItem as JobCardV3;
                this.jTFA_Invoice.JobCardID = jobCardselected.JobCardID;
                MessagingCenter.Send(this, "UpdateJTFA_Invoice", this.jTFA_Invoice);
                await Navigation.PopModalAsync();
            }
        }

        private async void ToolbarItem_Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

    }
}