using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace JTFA_MK3.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class JTFA_Main_PageMaster : ContentPage
    {
        public ListView ListView;

        public JTFA_Main_PageMaster()
        {
            InitializeComponent();

            BindingContext = new JTFA_Main_PageMasterViewModel();
            ListView = MenuItemsListView;
        }

        class JTFA_Main_PageMasterViewModel : INotifyPropertyChanged
        {
            public ObservableCollection<JTFA_Main_PageMasterMenuItem> MenuItems { get; set; }

            public JTFA_Main_PageMasterViewModel()
            {
                MenuItems = new ObservableCollection<JTFA_Main_PageMasterMenuItem>(new[]
                {
                    new JTFA_Main_PageMasterMenuItem { Id = 0, Title = "Clients" , TargetType=typeof(JTFA_ClientsPage)},
                    new JTFA_Main_PageMasterMenuItem { Id = 1, Title = "Vehicles", TargetType=typeof(VehiclesPage) },
                    new JTFA_Main_PageMasterMenuItem { Id = 2, Title = "Jobcards" , TargetType=typeof(JobCardsV3Page) },
                    new JTFA_Main_PageMasterMenuItem { Id = 3, Title = "Invoices", TargetType=typeof(JTFA_Invoices_Page) }//,
                    //new JTFA_Main_PageMasterMenuItem { Id = 4, Title = "Settings", },
                });
            }

            #region INotifyPropertyChanged Implementation
            public event PropertyChangedEventHandler PropertyChanged;
            void OnPropertyChanged([CallerMemberName] string propertyName = "")
            {
                if (PropertyChanged == null)
                    return;

                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
            #endregion
        }
    }
}