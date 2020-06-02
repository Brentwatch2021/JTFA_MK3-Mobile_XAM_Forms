using JTFA_MK3.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace JTFA_MK3.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [DesignTimeVisible(false)]
    public partial class NewEmployeePage : ContentPage
    {
        public Employee Employee { get; set; }

        public NewEmployeePage()
        {
            InitializeComponent();

            Employee = new Employee
            {
                firstName = "John",
                lastName = "Smith"
            };

            BindingContext = this;
        }

        private async void ToolbarItem_Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        private async void ToolbarItem_Save_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "AddEmployee", Employee);
            await Navigation.PopModalAsync();
        }
    }
}