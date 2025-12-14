using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using Ink_Canvas_Better.Interface;
using Ink_Canvas_Better.Windows;

namespace Ink_Canvas_Better.Controls.FloatingBar.FloatingBarControl;
/// <summary>
/// MultifunctionControl.xaml 的交互逻辑
/// </summary>
public partial class MultifunctionControl : UserControl, IFloatingBarComponentSettingBase
{
    public static string Guid { get; } = "03C5FD8D-2880-40F7-BAC5-9D83C347162C";
    public string ComponentGuid => Guid; 
    public object Settings { get; set; } = new MultifunctionControlSettings();
    public MultifunctionControlSettings MultifunctionControlSettings => Settings as MultifunctionControlSettings;

    FloatingBar floatingBar;

    private bool _isMouseDown = false;
    private Point _mouseDownPos, _mouseUpPos, _mouseDownControlPos, _currentMousePos;

    public MultifunctionControl()
    {
        InitializeComponent();

        this.MouseDown += MultifuntionControl_MouseDown;
        this.MouseUp += MultifuntionControl_MouseUp;
    }

    #region Properties

    #region ImageWidth

    public double ImageWidth
    {
        get { return (double)GetValue(ImageWidthProperty); }
        set { SetValue(ImageWidthProperty, value); }
    }

    public static readonly DependencyProperty ImageWidthProperty =
        DependencyProperty.Register("ImageWidth", typeof(double), typeof(MultifunctionControl), new PropertyMetadata(40d));

    #endregion

    #region ImageHeight

    public double ImageHeight
    {
        get { return (double)GetValue(ImageHeightProperty); }
        set { SetValue(ImageHeightProperty, value); }
    }

    public static readonly DependencyProperty ImageHeightProperty =
        DependencyProperty.Register("ImageHeight", typeof(double), typeof(MultifunctionControl), new PropertyMetadata(40d));

    #endregion

    #endregion

    private void MultifuntionControl_MouseDown(object sender, MouseButtonEventArgs e)
    {
        floatingBar = Ink_Canvas_Better.Helpers.VisualTreeHelper.GetParent<FloatingBar>(this);
        if (floatingBar == null)
        {
            return;
        }
        _isMouseDown = true;
        _mouseDownPos = e.GetPosition(null);
        if (floatingBar.RenderTransform is not TranslateTransform transform)
        {
            transform = new TranslateTransform();
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
            TranslateTransform transform = (TranslateTransform)floatingBar.RenderTransform;
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

public class MultifunctionControlSettings
{

}
