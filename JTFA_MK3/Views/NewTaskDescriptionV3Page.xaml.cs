using JTFA_MK3.Models.V2_Models;
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
    public partial class NewTaskDescriptionV3Page : ContentPage
    {
        private JobCardV3 jobCardV3 { get; set; }
        public NewTaskDescriptionV3Page(JobCardV3 _jobCard)
        {
            InitializeComponent();

            this.jobCardV3 = _jobCard;

            BindingContext = this;
        }


        private async void ToolbarItem_Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        private async void ToolbarItem_Save_Clicked(object sender, EventArgs e)
        {
            TaskDescriptionV3 taskDescription = new TaskDescriptionV3();
            taskDescription.Description = C_Description.Text;
            taskDescription.LabourCost = C_LabourCost.Text;
            taskDescription.PartsPrice = C_PartsCost.Text;
            taskDescription.TotalTaskCost = C_TotalCost.Text;

            jobCardV3?.TaskDescriptions.Add(taskDescription);
            await Navigation.PopModalAsync();
        }

    }
}