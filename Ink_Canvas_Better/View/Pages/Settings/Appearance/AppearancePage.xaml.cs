using System.Windows.Controls;
using Ink_Canvas_Better.Utilities.Interface;

namespace Ink_Canvas_Better.View.Pages.Settings.Appearance
{
    public partial class AppearancePage : Page
    {
        public Model.Settings Settings => IApp.Settings;

        public AppearancePage()
        {
            InitializeComponent();
        }
    }
}
