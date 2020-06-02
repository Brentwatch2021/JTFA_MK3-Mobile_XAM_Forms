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
    public partial class NewJobCardPage : ContentPage
    {
        public Job_Card job_Card { get; set; }

        private Job_Card jobCardToBEUPDATED;

        private bool IsCreate = true;

        // Creation of a new job card
        public NewJobCardPage()
        {
            InitializeComponent();

            job_Card = new Job_Card()
            {
                Car = "Toyota",
                Date = DateTime.UtcNow
            };
              
            BindingContext = this;
        }

        public NewJobCardPage(Job_Card jobCardToBeUpdated)
        {
            InitializeComponent();

            C_ClientName.Text = jobCardToBeUpdated.Name;
            C_Car.Text = jobCardToBeUpdated.Car;
            C_VIN.Text = jobCardToBeUpdated.VIN;
            C_Email.Text = jobCardToBeUpdated.Email;
            C_Mileage.Text = jobCardToBeUpdated.Mileage;
            C_Notes.Text = jobCardToBeUpdated.Notes;
            C_Phone.Text = jobCardToBeUpdated.Phone;
            C_REG.Text = jobCardToBeUpdated.REG;

            if (jobCardToBeUpdated != null)
            {
                jobCardToBEUPDATED = jobCardToBeUpdated;
                IsCreate = false;
            }

            BindingContext = this;

        }


        private async void ToolbarItem_Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        private async void ToolbarItem_Save_Clicked(object sender, EventArgs e)
        {
            if (IsCreate)
            {
                job_Card.Name = C_ClientName.Text;
                job_Card.Car = C_Car.Text;
                job_Card.VIN = C_VIN.Text;
                job_Card.Email = C_Email.Text;
                job_Card.Mileage = C_Mileage.Text;
                job_Card.Notes = C_Notes.Text;
                job_Card.Phone = C_Phone.Text;
                job_Card.REG = C_REG.Text;
                MessagingCenter.Send(this, "AddJobcard", job_Card);
                await Navigation.PopModalAsync();
            }
            else
            {
                // Update Item
                jobCardToBEUPDATED.Name = C_ClientName.Text;
                jobCardToBEUPDATED.Car = C_Car.Text;
                jobCardToBEUPDATED.VIN = C_VIN.Text;
                jobCardToBEUPDATED.Email = C_Email.Text;
                jobCardToBEUPDATED.Mileage = C_Mileage.Text;
                jobCardToBEUPDATED.Notes = C_Notes.Text;
                jobCardToBEUPDATED.Phone = C_Phone.Text;
                jobCardToBEUPDATED.REG = C_REG.Text;
                MessagingCenter.Send(this, "UpdateJobcard", jobCardToBEUPDATED);
                await Navigation.PopModalAsync();

            }
        }
    }
}