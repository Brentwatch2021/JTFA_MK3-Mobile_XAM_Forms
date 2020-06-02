using JTFA_MK3.Models;
using JTFA_MK3.ViewModels;
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
    public partial class EmployeeDetailPage : ContentPage
    {
        EmployeeDetailViewModel viewModel;
        public EmployeeDetailPage(EmployeeDetailViewModel _viewmodel)
        {
            InitializeComponent();
            BindingContext = this.viewModel = _viewmodel;
        }

        public EmployeeDetailPage()
        {
            InitializeComponent();

            var employee = new Employee
            {
                firstName = "First Name 1",
                lastName = "Last Name 1"
            };

            viewModel = new EmployeeDetailViewModel(employee);
            BindingContext = viewModel;
        }
    }
}