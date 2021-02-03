using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace JTFA_MK3.Views
{

    public class JTFA_Main_PageMasterMenuItem
    {
        public JTFA_Main_PageMasterMenuItem()
        {
            TargetType = typeof(JTFA_Main_PageMasterMenuItem);
        }
        public int Id { get; set; }
        public string Title { get; set; }

        public Type TargetType { get; set; }

        public string IconSource { get; set; }
    }
}