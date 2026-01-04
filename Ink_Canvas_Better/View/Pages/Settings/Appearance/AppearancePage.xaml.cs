using System.Windows.Controls;
using Ink_Canvas_Better.Services;
using Ink_Canvas_Better.Utilities.Interface;

namespace Ink_Canvas_Better.View.Pages.Settings.Appearance
{
    public partial class AppearancePage : Page
    {
        public Model.Settings Settings => IApp.GetService<SettingsService>().Settings;

        public AppearancePage()
        {
            InitializeComponent();
        }
    }
}
