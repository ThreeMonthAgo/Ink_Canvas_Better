using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Ink_Canvas_Better.Utilities.DataStructures;
using Ink_Canvas_Better.Utilities.Interface;
using Ink_Canvas_Better.View.Windows;
using Ink_Canvas_Better.ViewModel.Controls.FloatingBar;
using static Ink_Canvas_Better.Utilities.Enums.UI;

namespace Ink_Canvas_Better.View.Controls.FloatingBar;

public partial class FloatingBar : UserControl
{
    public FloatingBarVM Settings => DataContext as FloatingBarVM;

    public FloatingBar()
    {
        InitializeComponent();

        Loaded += FloatingBar_Loaded;
    }

    private void FloatingBar_Loaded(object sender, RoutedEventArgs e)
    {
        Settings.IsInitializing = false;
        Dock();
    }

    public void Dock(DockPlacement? placement = null)
    {
        placement ??= Settings.DockPlacement;
        // get translateTransform
        var tg = this.RenderTransform as TransformGroup;
        ScaleTransform st = null;
        foreach (var item in tg.Children)
        {
            if (item is ScaleTransform scaleTransform)
            {
                st = scaleTransform;
            }
        }
        // Dock
        var floatingBarWindow = IApp.GetService<FloatingBarWindow>();
        switch (placement.VerticalAlignment)
        {
            case DockVerticalAlignment.Top:
                floatingBarWindow.Top = 0;
                break;
            case DockVerticalAlignment.Center:
                floatingBarWindow.Top = (scHeight() / 2) - (realHeight() / 2);
                break;
            case DockVerticalAlignment.Bottom:
                floatingBarWindow.Top = scHeight() - realHeight();
                break;
            case DockVerticalAlignment.AboveTaskBar:
            case DockVerticalAlignment.Unset:
                floatingBarWindow.Top = wkaHeight() - realHeight();
                break;
        }
        switch (placement.HorizontalAlignment)
        {
            case DockHorizontalAlignment.Left:
                floatingBarWindow.Left = 0;
                break;
            case DockHorizontalAlignment.Right:
                floatingBarWindow.Left = scWidth() - realWidth();
                break;
            case DockHorizontalAlignment.Center:
            case DockHorizontalAlignment.Unset:
                floatingBarWindow.Left = (scWidth() / 2) - (realWidth() / 2);
                break;
        }

        double scWidth() => SystemParameters.PrimaryScreenWidth;
        double scHeight() => SystemParameters.PrimaryScreenHeight;
        //double wkaWidth() => SystemParameters.WorkArea.Width;  // unused
        double wkaHeight() => SystemParameters.WorkArea.Height;
        double realWidth() => this.ActualWidth * st.ScaleX;
        double realHeight() => this.ActualHeight * st.ScaleY;
    }

    private void Thumb_DragDelta(object sender, System.Windows.Controls.Primitives.DragDeltaEventArgs e)
    {
        var floatingBarWindow = IApp.GetService<FloatingBarWindow>();
        floatingBarWindow.Left += e.HorizontalChange;
        floatingBarWindow.Top += e.VerticalChange;
    }
}
