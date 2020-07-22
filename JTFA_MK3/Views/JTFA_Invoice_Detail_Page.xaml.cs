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
    public partial class JTFA_Invoice_Detail_Page : ContentPage
    {
        public JTFA_Invoice_Detail_Page(JTFA_Invoice_Detail_Page_View_Model viewModel)
        {
            InitializeComponent();
            this.L_Invoice_Number.Text = viewModel.jTFA_Invoice.INV_Number.ToString();
            this.E_Email_Recipients.Text = viewModel.jTFA_Invoice.Email_Recipients;
            var jobCard = viewModel.DataStore.GetItemAsync(viewModel.jTFA_Invoice.JobCardID);
            this.P_JobCards.SelectedItem = jobCard;
        }
    }
}