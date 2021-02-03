using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace JTFA_MK3.Models
{
    public class Job_Card : IJTFA_MainMenu_Item_View_Contract, I_List_Item_View
    {
        public Guid JobCardID { get; set; }

        /*
        private string name;
        public string Name 
        { 
            get { return name; } 
            set
            {
                OnPropertyChanged("Name");
            }
        }
        */

        public string Name { get; set; }
        public string VIN { set; get; }

        public string Email { set; get; }

        public string Car { get; set; }

        public DateTime Date { get; set; }

        public string Invoice { get; set; }

        public string REG { get; set; }

        public string Phone { get; set; }

        public string Mileage { get; set; }

        //public List<TaskDescription> Task_Descripts { get; set; }

        public string Total { get; set; }

        public string Notes { get; set; }

        public string IconType {
            get 
            {
                return "Job_Card";
            }
        }

        public string Main_Menu_Display_Name 
        {
            get 
            {
                return this.Name;
            }
        }

        public string TimeCreated 
        {
            get;
        }
        public string TimeModified { get => throw new NotImplementedException(); }
        public string Display_Name 
        {
            get 
            {
                return Name;
            }
        }

        /*
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string name)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
        */
    }
}
