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
    public partial class JTFA_ClientDetailPage : ContentPage
    {

        JTFA_ClientDetailViewModel _viewModel;
        public JTFA_ClientDetailPage(JTFA_ClientDetailViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = this._viewModel = viewModel;
        }

        public JTFA_ClientDetailPage()
        {
            InitializeComponent();
            var item = new JTFA_Client
            {
                Name = " ----Daffy Duck------"
            };

            _viewModel = new JTFA_ClientDetailViewModel(item);
            BindingContext = _viewModel;
        }




    }
}