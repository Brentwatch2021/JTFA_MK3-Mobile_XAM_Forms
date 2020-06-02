using JTFA_MK3.Models;
using JTFA_MK3.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace JTFA_MK3.ViewModels
{
    public class EmployeesViewModel : BaseEmployeeViewModel
    {
        public ObservableCollection<Employee> Employees { get; set; }
        public Command LoadEmployeesCommand { get; set; }


        public EmployeesViewModel()
        {
            Title = "Browse";
            Employees = new ObservableCollection<Employee>();
            LoadEmployeesCommand = new Command(async () => await ExecuteLoadEmployeesCommand());

            MessagingCenter.Subscribe<NewEmployeePage, Employee>(this, "AddEmployee", async (obj, item) =>
            {
                var newItem = item as Employee;
                Employees.Add(newItem);
                await DataStore.AddItemAsync(newItem);
            });
        }

        async Task ExecuteLoadEmployeesCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Employees.Clear();
                var items = await DataStore.GetItemsAsync(true);
                foreach (var item in items)
                {
                    Employees.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
