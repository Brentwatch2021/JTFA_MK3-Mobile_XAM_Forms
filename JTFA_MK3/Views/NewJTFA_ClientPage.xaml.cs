using JTFA_MK3.Models;
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
    public partial class NewJTFA_ClientPage : ContentPage
    {
        public JTFA_Client jTFA_Client { get; set; }
        public NewJTFA_ClientPage()
        {
            InitializeComponent();

            jTFA_Client = new JTFA_Client()
            {
                Name = "-------Daffy Duck-------",
            };

            BindingContext = this;
        }

        private async void ToolbarItem_Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        private async void ToolbarItem_Save_Clicked(object sender, EventArgs e)
        {
            jTFA_Client.Name = C_Name.Text;
            jTFA_Client.PhoneNumber = C_PhoneNumber.Text;
            jTFA_Client.Email = C_Email.Text;
            jTFA_Client.Notifications_Permission_Levels_Allowed = C_Notifications_Permission_Levels_Allowed.Text;
            MessagingCenter.Send(this, "AddJTFA_Client", jTFA_Client);
            await Navigation.PopModalAsync();
        }


    }
}