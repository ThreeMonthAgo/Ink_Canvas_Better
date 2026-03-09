using System.Collections.ObjectModel;
using System.Windows.Controls;
using Ink_Canvas_Better.Helpers;
using Ink_Canvas_Better.Logging;
using Ink_Canvas_Better.Services;
using Ink_Canvas_Better.Utilities.Interface;
using Ink_Canvas_Better.View.Windows;
using Ink_Canvas_Better.ViewModel.Controls.FloatingBar;
using Microsoft.Extensions.Logging;
using static Ink_Canvas_Better.Helpers.DllHelper;

namespace Ink_Canvas_Better.View.Pages.Settings.Debug;

/// <summary>
/// Interaction logic for DebugPage.xaml
/// </summary>
public partial class DebugPage : Page
{
    public static ObservableCollection<MonitorInfo> Screens => DllHelper.Screens;
    public ObservableCollection<FloatingBarVM> FloatingBarCollection => IApp.GetService<SettingsService>().Settings.MainWindowVM.ToolBarCollection;

    public DebugPage()
    {
        InitializeComponent();

        DataContext = this;
    }

    private void Button_Save_Strokes(object sender, System.Windows.RoutedEventArgs e)
    {
        var inkCanvas = IApp.GetService<MainWindow>().InkCanvas;
        var logger = IApp.GetService<ILogger<DebugPage>>();
        var settings = IApp.GetService<SettingsService>();
        IApp.GetService<InkCanvasService>().SaveData(inkCanvas);
        logger.WriteLog(LogLevel.Debug, $"Saved strokes to file. Path:{settings.Settings.DataDirPath}");
    }
}
