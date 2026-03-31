using System.Diagnostics;
using System.IO;
using System.Windows.Controls;
using Ink_Canvas_Better.Utilities.Interface;
using Microsoft.Extensions.Logging;

namespace Ink_Canvas_Better.View.Pages.Settings.Data;

/// <summary>
/// Interaction logic for DataPage.xaml
/// </summary>
public partial class DataPage : Page
{
    public Model.Settings Settings => IApp.Settings;

    public DataPage()
    {
        InitializeComponent();

        DataContext = this;
    }

    public int LogLevel
    {
        get
        {
            return (int)IApp.Settings.LogLevel;
        }
        set
        {
            IApp.Settings.LogLevel = (LogLevel)value;
            TextBlock_Restart_LogDir.Visibility = System.Windows.Visibility.Visible;
        }
    }

    private void Button_OpenLogDir_Click(object sender, System.Windows.RoutedEventArgs e)
    {
        var p = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, IApp.Settings.LogDirPath);
        if (Directory.Exists(p))
        {
            Process.Start(new ProcessStartInfo($"{p}") { UseShellExecute = true });
        }
    }

    private void Button_EditLogDir_Click(object sender, System.Windows.RoutedEventArgs e)
    {
        if (string.Equals(TextBlock_LogDir.Text, IApp.Settings.LogDirPath))
        {
            return;
        }
        else
        {
            IApp.Settings.LogDirPath = TextBlock_LogDir.Text;
            TextBlock_Restart_LogDir.Visibility = System.Windows.Visibility.Visible;
            // TODO: Absolute path support
        }
    }
}
