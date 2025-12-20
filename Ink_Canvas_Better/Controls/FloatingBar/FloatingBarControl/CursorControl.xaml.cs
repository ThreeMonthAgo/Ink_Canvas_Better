using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using Ink_Canvas_Better.Controls.FloatingBar.SubPanel;
using Ink_Canvas_Better.Interface;
using Ink_Canvas_Better.Services;
using Ink_Canvas_Better.Windows;
using Newtonsoft.Json;

namespace Ink_Canvas_Better.Controls.FloatingBar.FloatingBarControl;

public partial class CursorControl : UserControl, IFloatingBarComponentSettingBase
{
    private MainWindow mainWindow;

    public static string Guid { get; } = "D034499E-882E-41DF-BE4B-C7446870A93C";
    public string ComponentGuid => Guid;
    public object Settings { get; set; } = new CursorControlSettings();

    public CursorControl()
    {
        InitializeComponent();

        this.Loaded += CursorControl_Loaded;
        this.MouseUp += CursorControl_MouseUp;
    }

    private void CursorControl_MouseUp(object sender, MouseButtonEventArgs e) => TryInvoke();

    private void CursorControl_Loaded(object sender, RoutedEventArgs e)
    {
        this.mainWindow = App.GetService<MainWindow>();

        (Settings as CursorControlSettings).IsInitializing = false;
    }

    public bool TryInvoke()
    {
        mainWindow.CurrentEditingMode = Enums.EditingMode.None;
        return true;
    }

    #region Properties

    #region Text

    public string Text
    {
        get { return (string)GetValue(TextProperty); }
        set { SetValue(TextProperty, value); }
    }

    public static readonly DependencyProperty TextProperty =
        DependencyProperty.Register("Text", typeof(string), typeof(CursorControl), new PropertyMetadata("Text"));

    #endregion

    #region TextVisibility

    public Visibility TextVisibility
    {
        get { return (Visibility)GetValue(TextVisibilityProperty); }
        set { SetValue(TextVisibilityProperty, value); }
    }

    public static readonly DependencyProperty TextVisibilityProperty =
        DependencyProperty.Register("TextVisibility", typeof(Visibility), typeof(CursorControl), new PropertyMetadata(Visibility.Collapsed));

    #endregion

    #endregion
}

public class CursorControlSettings : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        if (!IsInitializing) App.GetService<SettingsService>().SaveSettings();
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    [JsonIgnore]
    public bool IsInitializing { get; set; } = true;
}


