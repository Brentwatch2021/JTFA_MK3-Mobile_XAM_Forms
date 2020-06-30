using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using JTFA_MK3.Services;
using JTFA_MK3.Views;
using JTFA_MK3.Services.V3_Services;
using JTFA_MK3.Constants.ToDoItem;

namespace JTFA_MK3
{
    public partial class App : Application
    {
        //TODO: Replace with *.azurewebsites.net url after deploying backend to Azure
        //To debug on Android emulators run the web backend against .NET Core not IIS
        //If using other emulators besides stock Google images you may need to adjust the IP address
        public static string HOME_IP = "http://10.0.0.102:5555";
        public static string AZURE_API_URL = "https://dashboardmk420200603124832.azurewebsites.net";
        public static string Darren_Becker_Dolphin_Beach_Hotel_IP = "http://10.0.0.7:5050";
        public static string API_WEB_IP = AZURE_API_URL;


        public static bool UseMockDataStore = false;


        static VehicleDatabase _vehicleDatabase;

        public static VehicleDatabase VehicleDatabase 
        { 
            get
            {
                if (_vehicleDatabase == null)
                {
                    _vehicleDatabase = new VehicleDatabase();
                }
                return _vehicleDatabase;
            } 
        }


        public App()
        {
            InitializeComponent();

            if (UseMockDataStore)
            {
             
            }
            else
            {
                DependencyService.Register<DashboardMK4_API_Job_Card_Store>();
                DependencyService.Register<DashboardMK4_API_Vehicle_Store>();
                DependencyService.Register<DashboardMK4_API_JTFA_Client_Store>();
                DependencyService.Register<DashboardMK4_API_JobCardV3_DataStore>();
            }
                

            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
