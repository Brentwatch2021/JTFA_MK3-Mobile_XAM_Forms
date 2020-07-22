using JTFA_MK3.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace JTFA_MK3.ViewModels
{
    public class New_JTFA_Invoice_ViewModel : BaseJTFAInvoicesViewModel
    {
        public JTFA_Invoice jTFA_Invoice { get; set; }

        public New_JTFA_Invoice_ViewModel(JTFA_Invoice jTFA_Invoice = null)
        {
        }
    }
}
