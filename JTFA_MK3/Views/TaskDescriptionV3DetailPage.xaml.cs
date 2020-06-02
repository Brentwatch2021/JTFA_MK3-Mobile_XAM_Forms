using JTFA_MK3.ViewModels;
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
    public partial class TaskDescriptionV3DetailPage : ContentPage
    {
        public TaskDescriptionV3DetailViewModel viewModel;
        public TaskDescriptionV3DetailPage()
        {
            InitializeComponent();
        }

        public TaskDescriptionV3DetailPage(TaskDescriptionV3DetailViewModel _viewModel)
        {
            InitializeComponent();

            BindingContext = this.viewModel = _viewModel;
        }

    }
}