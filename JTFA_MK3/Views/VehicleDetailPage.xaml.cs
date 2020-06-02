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
    public partial class VehicleDetailPage : ContentPage
    {
        VehicleDetailViewModel viewModel;
        public VehicleDetailPage(VehicleDetailViewModel _viewmodel)
        {
            InitializeComponent();

            BindingContext = this.viewModel = _viewmodel;
        }

        public VehicleDetailPage()
        {
            InitializeComponent();
            var item = new Vehicle
            {
                Make = "Volkswagen"
            };

            viewModel = new VehicleDetailViewModel(item);
            BindingContext = viewModel;
        }

    }
}