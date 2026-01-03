using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Ink_Canvas_Better.Utilities.DataStructures;
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
        TranslateTransform tt = null;
        ScaleTransform st = null;
        foreach (var item in tg.Children)
        {
            if (item is TranslateTransform translateTransform)
            {
                tt = translateTransform;
                continue;
            }
            if (item is ScaleTransform scaleTransform)
            {
                st = scaleTransform;
                continue;
            }
        }
        if (tt == null) return;
        // Dock
        switch (placement.VerticalAlignment)
        {
            case DockVerticalAlignment.Top:
                tt.Y = 0;
                break;
            case DockVerticalAlignment.Center:
                tt.Y = (scHeight() / 2) - (realHeight() / 2);
                break;
            case DockVerticalAlignment.Bottom:
                tt.Y = scHeight() - realHeight();
                break;
            case DockVerticalAlignment.AboveTaskBar:
            case DockVerticalAlignment.Unset:
                tt.Y = wkaHeight() - realHeight();
                break;
        }
        switch (placement.HorizontalAlignment)
        {
            case DockHorizontalAlignment.Left:
                tt.X = 0;
                break;
            case DockHorizontalAlignment.Right:
                tt.X = scWidth() - realWidth();
                break;
            case DockHorizontalAlignment.Center:
            case DockHorizontalAlignment.Unset:
                tt.X = (scWidth() / 2) - (realWidth() / 2);
                break;
        }

        double scWidth() => SystemParameters.PrimaryScreenWidth;
        double scHeight() => SystemParameters.PrimaryScreenHeight;
        double wkaWidth() => SystemParameters.WorkArea.Width;
        double wkaHeight() => SystemParameters.WorkArea.Height;
        double realWidth() => this.ActualWidth * st.ScaleX;
        double realHeight() => this.ActualHeight * st.ScaleY;
    }
}
