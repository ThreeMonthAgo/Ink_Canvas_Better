using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Ink_Canvas_Better.Interface;
using Ink_Canvas_Better.Services;
using Ink_Canvas_Better.Windows;
using Newtonsoft.Json;

namespace Ink_Canvas_Better.Controls.FloatingBar.FloatingBarControl;

public partial class MultifunctionControl : UserControl, IFloatingBarComponentSettingBase
{
    public static string Guid { get; } = "03C5FD8D-2880-40F7-BAC5-9D83C347162C";
    public string ComponentGuid => Guid; 
    public object Settings { get; set; } = new MultifunctionControlSettings();

    FloatingBar floatingBar;

    private bool _isMouseDown = false;
    private Point _mouseDownPos, _mouseUpPos, _mouseDownControlPos, _currentMousePos;

    public MultifunctionControl()
    {
        InitializeComponent();

        this.MouseDown += MultifuntionControl_MouseDown;
        this.MouseUp += MultifuntionControl_MouseUp;
    }

    public bool TryInvoke() => true;

    private void MultifuntionControl_MouseDown(object sender, MouseButtonEventArgs e)
    {
        var t = Helpers.VisualTreeHelper.GetVisualParent<FloatingBar>(this);
        if (t == null) return;
        floatingBar = t;
        _isMouseDown = true;
        _mouseDownPos = e.GetPosition(null);
        TranslateTransform transform = null;
        foreach (var item in ((TransformGroup)floatingBar.RenderTransform).Children)
        {
            if (item.GetType() == typeof(TranslateTransform))
            {
                transform = (TranslateTransform)item;
                break;
            }
        }
        _mouseDownControlPos = new Point(transform.X, transform.Y);
        this.MouseMove += MultifuntionControl_MouseMove;
        this.CaptureMouse();
        e.Handled = true;
    }

    private void MultifuntionControl_MouseMove(object sender, MouseEventArgs e)
    {
        if (_isMouseDown)
        {
            TranslateTransform transform = null;
            foreach (var item in ((TransformGroup)floatingBar.RenderTransform).Children)
            {
                if (item.GetType() == typeof(TranslateTransform))
                {
                    transform = (TranslateTransform)item;
                    break;
                }
            }
            _currentMousePos = e.GetPosition(null);
            transform.X = _mouseDownControlPos.X + _currentMousePos.X - _mouseDownPos.X;
            transform.Y = _mouseDownControlPos.Y + _currentMousePos.Y - _mouseDownPos.Y;
        }
    }

    private void MultifuntionControl_MouseUp(object sender, MouseButtonEventArgs e)
    {
        this.MouseMove -= MultifuntionControl_MouseMove;
        this.ReleaseMouseCapture();
        _isMouseDown = false;
        _mouseUpPos = e.GetPosition(null);
        // TODO: fold the floatingbar
    }
}

public class MultifunctionControlSettings : INotifyPropertyChanged
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
