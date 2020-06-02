using System;
using System.Collections.Generic;
using System.Text;

namespace JTFA_MK3.Search
{
    public interface ISearchPage
    {
        void OnSearchBarTextChanged(string text);

        event EventHandler<string> SearchBarTextChanged;
    }
}
