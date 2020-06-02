using System;
using System.Collections.Generic;
using System.Text;

namespace JTFA_MK3.Models
{
    public interface IJTFA_MainMenu_Item_View_Contract
    {
        string IconType { get; }

        string Main_Menu_Display_Name { get; }
    }
}
