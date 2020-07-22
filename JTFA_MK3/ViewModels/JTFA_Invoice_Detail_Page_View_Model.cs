using JTFA_MK3.Models;
using JTFA_MK3.Models.V2_Models;
using JTFA_MK3.Services.V3_Services;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace JTFA_MK3.ViewModels
{
    public class JTFA_Invoice_Detail_Page_View_Model
    {
        public IIJobCardV3DataStore<JobCardV3> DataStore => DependencyService.Get<IIJobCardV3DataStore<JobCardV3>>();
        public JTFA_Invoice jTFA_Invoice { get; set; }
        public JTFA_Invoice_Detail_Page_View_Model(JTFA_Invoice invoice)
        {
            this.jTFA_Invoice = invoice;
        }
    }
}
