using JTFA_MK3.Models;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace JTFA_MK3.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPageMk2 : ContentPage
    {
        public ObservableCollection<IJTFA_MainMenu_Item_View_Contract> Items { get; set; }

        public MainPageMk2()
        {
            InitializeComponent();

            Job_Card jobcard = new Job_Card();
            jobcard.Name = "Jobcad polo";

            Items = new ObservableCollection<IJTFA_MainMenu_Item_View_Contract>
            {
                jobcard,
            };

            /// whne getting the items dont follow inheritance follow association so you dont load th entire object each time
            /// 

            MyListView.ItemsSource = Items;
        }

        async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
                return;

            await DisplayAlert("Item Tapped", "An item was tapped.", "OK");

            //Deselect Item
            ((ListView)sender).SelectedItem = null;
        }

        // called when list is refreshed via user pull from top
        private void MyListView_Refreshing(object sender, EventArgs e)
        {

        }
    }
}
