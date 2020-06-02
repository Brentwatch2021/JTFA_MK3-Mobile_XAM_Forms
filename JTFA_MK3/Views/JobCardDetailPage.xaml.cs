using JTFA_MK3.Models;
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
    public partial class JobCardDetailPage : ContentPage
    {

        JobCardDetailViewModel viewModel;

        public JobCardDetailPage(JobCardDetailViewModel _viewModel)
        {
            InitializeComponent();

            BindingContext = this.viewModel = _viewModel;
        }

        public JobCardDetailPage()
        {
            InitializeComponent();

            var item = new Job_Card
            {
                Car = "Golf"
            };

            viewModel = new JobCardDetailViewModel(item);
            BindingContext = viewModel;
        }
    }
}