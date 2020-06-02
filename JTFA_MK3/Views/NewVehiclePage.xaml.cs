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
    public partial class NewVehiclePage : ContentPage
    {
        public Vehicle vehicle { get; set; }
        public NewVehiclePage()
        {
            InitializeComponent();

            vehicle = new Vehicle()
            {
                Make = "Toyota",
                
            };

            BindingContext = this;
        }

        private async void ToolbarItem_Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        private async void ToolbarItem_Save_Clicked(object sender, EventArgs e)
        {
            vehicle.Make = C_Make.Text;
            vehicle.CC = C_CC.Text;
            vehicle.Engine_Number = C_Engine_Number.Text;
            vehicle.Mileage = C_Mileage.Text;
            vehicle.REG = C_REG.Text;
            vehicle.VIN = C_VIN.Text;
            vehicle.Year = C_Year.Text;
            
            MessagingCenter.Send(this, "AddVehicle", vehicle);
            await Navigation.PopModalAsync();
        }
    }
}