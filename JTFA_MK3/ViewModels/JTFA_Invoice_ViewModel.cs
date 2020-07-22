using JTFA_MK3.Models;
using JTFA_MK3.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace JTFA_MK3.ViewModels
{
    public class JTFA_Invoice_ViewModel : BaseJTFAInvoicesViewModel
    {
        public ObservableCollection<JTFA_Invoice> JTFA_Invoices { get; set; }
        public Command LoadJTFA_InvoicesCommand { get; set; }
        public JTFA_Invoice_ViewModel()
        {
            Title = "Browse";
            JTFA_Invoices = new ObservableCollection<JTFA_Invoice>();
            LoadJTFA_InvoicesCommand = new Command(async () => await ExecuteLoadJTFA_InvoicesCommand());

            MessagingCenter.Subscribe<New_JTFA_Invoice_Page, JTFA_Invoice>(this, "Add_JTFA_Invoice", async (obj, item) =>
            {
                var jTFA_InvoiceToAdd = item as JTFA_Invoice;
                await DataStore.AddItemAsync(jTFA_InvoiceToAdd);
            }
           );

            // Removal functionality
            MessagingCenter.Subscribe<JTFA_Invoices_Page, JTFA_Invoice>(this, "Remove_JTFA_Invoice", async (obj, item) => {
                var itemToDelete = item as JTFA_Invoice;
                JTFA_Invoices.Remove(itemToDelete);
                await DataStore.DeleteItemAsync(itemToDelete.JTFA_Invoice_ID);
            });


            // Update 
            MessagingCenter.Subscribe<New_JTFA_Invoice_Page, JTFA_Invoice>(this, "UpdateJTFA_Invoice", async (obj, item) =>
            {
                var jTFA_InvoiceToUpdate = item as JTFA_Invoice;
                await DataStore.UpdateItemAsync(jTFA_InvoiceToUpdate);
            }
            );
        }

        private async Task ExecuteLoadJTFA_InvoicesCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                JTFA_Invoices.Clear();
                var items = await DataStore.GetItemsAsync(true);
                foreach (var item in items)
                {
                    JTFA_Invoices.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
