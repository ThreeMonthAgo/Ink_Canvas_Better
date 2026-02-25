using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Windows.Controls;
using Ink_Canvas_Better.Helpers;
using Ink_Canvas_Better.Utilities.Attributes;
using Ink_Canvas_Better.Utilities.Bases;
using Ink_Canvas_Better.Utilities.DataStructures;
using Ink_Canvas_Better.View.Controls;
using Ink_Canvas_Better.ViewModel.Controls.FloatingBar;
using Ink_Canvas_Better.ViewModel.Controls.FloatingBar.FloatingBarControl;
using Newtonsoft.Json;
using Windows.Win32.Foundation;
using static Ink_Canvas_Better.Utilities.Enums.UI;

namespace Ink_Canvas_Better.ViewModel.Controls;

[Component(
    viewType: typeof(SlideShowControl),
    guid: "910AB395-3347-466F-BB00-C71E1212F69A")]
public class SlideShowControlVM : FloatingBarViewModelBase
{
    private ObservableCollection<FloatingBarViewModelBase>? _items = [
        new FloatingBarGroupVM(){
            Items = [
                new PreviousPageControlVM(),
                new NextPageControlVM(),
            ]
        },
        ];
    private double _spacing = 4.0;
    private Orientation _orientation = Orientation.Horizontal;
    private double _scale = 1.0;

    // ignored below
    private double _x = 0;
    private double _y = 0;
    private double _width = 0;
    private double _height = 0;

    #region

    public ObservableCollection<FloatingBarViewModelBase>? Items
    {
        get { return _items; }
        set { SetProperty(ref _items, value); }
    }

    public double Spacing
    {
        get { return _spacing; }
        set { SetProperty(ref _spacing, value); }
    }

    public Orientation Orientation
    {
        get { return _orientation; }
        set { SetProperty(ref _orientation, value); }
    }

    public double Scale
    {
        get { return _scale; }
        set { SetProperty(ref _scale, value); }
    }

    [JsonIgnore]
    public double X
    {
        get { return _x; }
        set { SetProperty(ref _x, value, false); }
    }

    [JsonIgnore]
    public double Y
    {
        get { return _y; }
        set { SetProperty(ref _y, value, false); }
    }

    /// <remarks>
    /// It's a one-way binding from the UI to the DataContext.
    /// (Note: While the exact implementation differs, this analogy
    /// helps illustrate the concept.) In other words, you cannot modify
    /// the actual width by changing this property.
    /// </remarks>
    [JsonIgnore]
    public double Width
    {
        get { return _width; }
        set { SetProperty(ref _width, value, false); }
    }

    /// <remarks>
    /// It's a one-way binding from the UI to the DataContext.
    /// (Note: While the exact implementation differs, this analogy
    /// helps illustrate the concept.) In other words, you cannot modify
    /// the actual height by changing this property.
    /// </remarks>
    [JsonIgnore]
    public double Height
    {
        get { return _height; }
        set { SetProperty(ref _height, value, false); }
    }

    #endregion

    /// <remarks>
    /// DockPlacement is required and not null due to the SlideShowControl is auto generated runtime
    /// </remarks>
    public void Dock(DockPlacement placement, RECT rect)
    {
        // Dock
        switch (placement.VerticalAlignment)
        {
            case DockVerticalAlignment.Top:
                this.Y = 0;
                break;
            case DockVerticalAlignment.Center:
                this.Y = (rect.Height - Height) / 2;
                break;
            case DockVerticalAlignment.AboveTaskBar:
            case DockVerticalAlignment.Bottom:
            case DockVerticalAlignment.Unset:
                this.Y = rect.Height - Height;
                break;
        }
        switch (placement.HorizontalAlignment)
        {
            case DockHorizontalAlignment.Left:
                this.X = 0;
                break;
            case DockHorizontalAlignment.Right:
                this.X = rect.Width - Width;
                break;
            case DockHorizontalAlignment.Center:
            case DockHorizontalAlignment.Unset:
                this.X = (rect.Width - Width) / 2;
                break;
        }
        this.X += rect.left;
        this.Y += rect.top;
    }
}