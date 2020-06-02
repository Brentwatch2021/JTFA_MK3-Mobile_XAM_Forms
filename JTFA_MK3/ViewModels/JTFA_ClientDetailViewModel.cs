using JTFA_MK3.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace JTFA_MK3.ViewModels
{
    public class JTFA_ClientDetailViewModel : BaseJTFA_ClientsViewModel
    {
        public JTFA_Client _jTFA_Client { get; set; }
        public JTFA_ClientDetailViewModel(JTFA_Client jTFA_Client = null)
        {
            _jTFA_Client = jTFA_Client;
            Title = _jTFA_Client.Name;
        }
    }
}
