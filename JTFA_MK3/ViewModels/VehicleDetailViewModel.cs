using JTFA_MK3.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace JTFA_MK3.ViewModels
{
    public class VehicleDetailViewModel : BaseVehiclesViewModel
    {
        public Vehicle Vehicle { get; set; }
        public VehicleDetailViewModel(Vehicle vehicle = null)
        {
            Vehicle = vehicle;
            Title = vehicle.Make;
        }
    }
}
