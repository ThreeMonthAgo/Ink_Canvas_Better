using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using Ink_Canvas_Better.Controls.FloatingBar.SubPanel;
using Ink_Canvas_Better.Interface;
using Ink_Canvas_Better.Services;
using Ink_Canvas_Better.Windows;
using Newtonsoft.Json;

namespace Ink_Canvas_Better.Controls.FloatingBar.FloatingBarControl;
public partial class PenControl : UserControl, IFloatingBarComponentSettingBase
{
    private MainWindow mainWindow;

    public static string Guid { get; } = "87F7581C-364A-49D7-93C3-3355A8415D38";
    public string ComponentGuid => Guid;
    public object Settings { get; set; } = new PenControlSettings();

    public PenControl()
    {
        InitializeComponent();

        this.Loaded += PenControl_Loaded;
        this.MouseUp += PenControl_MouseUp;
    }

    private void PenControl_Loaded(object sender, RoutedEventArgs e)
    {
        mainWindow = App.GetService<MainWindow>();
        (Settings as PenControlSettings).IsInitializing = false;
    }

    private void PenControl_MouseUp(object sender, MouseButtonEventArgs e)
    {
        if (mainWindow.CurrentEditingMode != Enums.EditingMode.Ink)
        {
            mainWindow.CurrentEditingMode = Enums.EditingMode.Ink;
        }
        else
        {
            this.IsOpen = true;
        }
        foreach (var item in (Settings as PenControlSettings).Subpanels)
        {
            item.TryInvoke();
        }
    }

    public bool TryInvoke()
    {
        try
        {
            foreach (var item in (Settings as PenControlSettings).Subpanels) item.TryInvoke();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    #region Properties

    #region Text

    public string Text
    {
        get { return (string)GetValue(TextProperty); }
        set { SetValue(TextProperty, value); }
    }

    public static readonly DependencyProperty TextProperty =
        DependencyProperty.Register("Text", typeof(string), typeof(PenControl), new PropertyMetadata("Text"));

    #endregion

    #region TextVisibility

    public Visibility TextVisibility
    {
        get { return (Visibility)GetValue(TextVisibilityProperty); }
        set { SetValue(TextVisibilityProperty, value); }
    }

    public static readonly DependencyProperty TextVisibilityProperty =
        DependencyProperty.Register("TextVisibility", typeof(Visibility), typeof(PenControl), new PropertyMetadata(Visibility.Collapsed));

    #endregion

    // Popup Properties

    #region IsOpen

    public bool IsOpen
    {
        get { return (bool)GetValue(IsOpenProperty); }
        set { SetValue(IsOpenProperty, value); }
    }

    public static readonly DependencyProperty IsOpenProperty =
        DependencyProperty.Register(nameof(IsOpen), typeof(bool), typeof(PenControl), new PropertyMetadata(false));

    #endregion

    #region StaysOpen

    public bool StaysOpen
    {
        get { return (bool)GetValue(StaysOpenProperty); }
        set { SetValue(StaysOpenProperty, value); }
    }

    public static readonly DependencyProperty StaysOpenProperty =
        DependencyProperty.Register(nameof(StaysOpen), typeof(bool), typeof(PenControl), new PropertyMetadata(false));

    #endregion

    #region CornerRadius

    public CornerRadius CornerRadius
    {
        get { return (CornerRadius)GetValue(CornerRadiusProperty); }
        set { SetValue(CornerRadiusProperty, value); }
    }

    public static readonly DependencyProperty CornerRadiusProperty =
        DependencyProperty.Register(nameof(CornerRadius), typeof(CornerRadius), typeof(PenControl), new PropertyMetadata(new CornerRadius(4d)));

    #endregion

    #region PopupAnimation

    public PopupAnimation PopupAnimation
    {
        get { return (PopupAnimation)GetValue(PopupAnimationProperty); }
        set { SetValue(PopupAnimationProperty, value); }
    }

    public static readonly DependencyProperty PopupAnimationProperty =
        DependencyProperty.Register(nameof(PopupAnimation), typeof(PopupAnimation), typeof(PenControl), new PropertyMetadata(PopupAnimation.Fade));

    #endregion

    #endregion

    private void Button_HidePopup(object sender, RoutedEventArgs e) => this.IsOpen = false;
}

public class PenControlSettings : INotifyPropertyChanged
{
    private ObservableCollection<IFloatingBarComponentSettingBase> _subpanels = [App.GetService<PenSubpanel>()];

    #region

    public ObservableCollection<IFloatingBarComponentSettingBase> Subpanels
    {
        get { return _subpanels; }
        set { _subpanels = value; OnPropertyChanged(); }
    }

    #endregion

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        if (!IsInitializing) App.GetService<SettingsService>().SaveSettings();
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    [JsonIgnore]
    public bool IsInitializing { get; set; } = true;
}

