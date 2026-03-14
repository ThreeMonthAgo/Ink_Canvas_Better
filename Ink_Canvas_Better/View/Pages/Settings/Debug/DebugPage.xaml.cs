using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using Ink_Canvas_Better.Helpers;
using Ink_Canvas_Better.Logging;
using Ink_Canvas_Better.Services;
using Ink_Canvas_Better.Utilities.Interface;
using Ink_Canvas_Better.View.Windows;
using Ink_Canvas_Better.ViewModel.Controls;
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
    public ObservableCollection<ToolBarControlVM> FloatingBarCollection => IApp.GetService<SettingsService>().Settings.MainWindowVM.ToolBarCollection;

    public DebugPage()
    {
        InitializeComponent();

        DataContext = this;
    }

    private void Page_Loaded(object sender, RoutedEventArgs e)
    {
        TextBox_StrokeFilePath.Text = IApp.GetService<SettingsService>().Settings.DataDirPath;
    }

    private void Button_Save_Strokes(object sender, RoutedEventArgs e)
    {
        var logger = IApp.GetService<ILogger<DebugPage>>();
        var inkCanvas = IApp.GetService<MainWindow>().InkCanvas;
        if (inkCanvas.Strokes.Count < 1)
        {
            logger.WriteLog(LogLevel.Information, $"InkCanvas is empty!");
            return;
        }
        var settings = IApp.GetService<SettingsService>();
        IApp.GetService<InkCanvasService>().SaveData(inkCanvas);
        logger.WriteLog(LogLevel.Debug, $"Saved strokes to file. Path:{settings.Settings.DataDirPath}");
    }

    private void Button_Load_Strokes(object sender, RoutedEventArgs e)
    {
        var path = TextBox_StrokeFilePath.Text;
        var logger = IApp.GetService<ILogger<DebugPage>>();
        if (!File.Exists(path))
        {
            logger.WriteLog(LogLevel.Error, $"File not found. Path:{path}");
            return;
        }
        var inkCanvas = IApp.GetService<MainWindow>().InkCanvas;
        var settings = IApp.GetService<SettingsService>();
        IApp.GetService<InkCanvasService>().LoadData(inkCanvas, path);
        logger.WriteLog(LogLevel.Debug, $"Saved strokes to file. Path:{settings.Settings.DataDirPath}");
    }
}
