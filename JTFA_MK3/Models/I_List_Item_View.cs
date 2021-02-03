using System;
using System.Collections.Generic;
using System.Text;

namespace JTFA_MK3.Models
{
    public interface I_List_Item_View
    {
        string Display_Name { get; }

        string TimeCreated { get; }

        string TimeModified { get; }
    }
}
