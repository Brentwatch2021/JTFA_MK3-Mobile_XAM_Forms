using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace JTFA_MK3.Views.Login
{
    /// <summary>
    /// View used to show the email entry with validation status.
    /// </summary>
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]

    public partial class LoginEmailEntry
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LoginEmailEntry" /> class.
        /// </summary>
        public LoginEmailEntry()
        {
            InitializeComponent();
        }
    }
}