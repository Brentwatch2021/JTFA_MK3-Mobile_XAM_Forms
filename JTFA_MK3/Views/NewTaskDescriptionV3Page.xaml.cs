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

        public TaskDescriptionV3 taskDescription { get; set; }
        public NewTaskDescriptionV3Page(JobCardV3 _jobCard)
        {
            InitializeComponent();

            this.jobCardV3 = _jobCard;

            BindingContext = this;
        }

        public NewTaskDescriptionV3Page(TaskDescriptionV3 _taskDescription)
        {
            InitializeComponent();

            this.taskDescription = _taskDescription;
            

            // Load up control values with current values
            C_Description.Text = _taskDescription.Description;
            C_LabourCost.Text = _taskDescription.LabourCost;
            C_PartsCost.Text = _taskDescription.PartsPrice;
            C_TotalCost.Text = _taskDescription.TotalTaskCost;

            BindingContext = this;
        }


        private async void ToolbarItem_Cancel_Clicked(object sender, EventArgs e)
        {
            if (this.taskDescription != null)
            {
                await Navigation.PopAsync();
            }
            else
            {
                await Navigation.PopModalAsync();
            }
            
        }

        private async  void ToolbarItem_Save_Clicked(object sender, EventArgs e)
        {
            if (jobCardV3 != null && this.taskDescription == null && !IsPropsNull(C_Description.Text, C_LabourCost.Text, C_PartsCost.Text, C_TotalCost.Text))
            {
                // new task description
                TaskDescriptionV3 taskDescription = new TaskDescriptionV3();
                taskDescription.Description = C_Description.Text;
                taskDescription.LabourCost = C_LabourCost.Text;
                taskDescription.PartsPrice = C_PartsCost.Text;
                taskDescription.TotalTaskCost = C_TotalCost.Text;
                jobCardV3?.TaskDescriptions?.Add(taskDescription);
                await Navigation.PopModalAsync();
            }
            else if (this.taskDescription != null)
            {
                // update of a task description
                this.taskDescription.Description = C_Description.Text;
                this.taskDescription.LabourCost = C_LabourCost.Text;
                this.taskDescription.PartsPrice = C_PartsCost.Text;
                this.taskDescription.TotalTaskCost = C_TotalCost.Text;
                await Navigation.PopAsync();
            }
            // Need to find differant soltion to removing the latest modal cant rely on popmodalasync or popasync
            // Inconsistant issue 3 random conditions can happen 
            // 1. works as expected removing the new task description page
            // 2. removes both modal pages returning user to root
            // 3. Works as expected returns user ro new jobcard page
            //Navigation.PopModalAsync();
            //var thispage = Navigation.ModalStack[1];
            
        }

        private bool IsPropsNull(string text1, string text2, string text3, string text4)
        {
            return text1 == null && text2 == null && text3 == null && text4 == null;
        }
    }
}