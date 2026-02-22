using System.Windows.Controls;
using Ink_Canvas_Better.Utilities.Bases;
using Ink_Canvas_Better.Utilities.DataStructures;
using Newtonsoft.Json;

namespace Ink_Canvas_Better.ViewModel.Controls;

public class SlideShowControlVM : ViewModelBase
{
    private double _spacing = 4.0;
    private Orientation _orientation = Orientation.Horizontal;
    private double _scale = 1.0;

    // ignored below
    private double _x = 0;
    private double _y = 0;
    private double _width = 0;
    private double _height = 0;

    #region

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
        set { SetProperty(ref _x, value); }
    }

    [JsonIgnore]
    public double Y
    {
        get { return _y; }
        set { SetProperty(ref _y, value); }
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
        set { SetProperty(ref _width, value); }
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
        set { SetProperty(ref _height, value); }
    }

    #endregion

    public void Dock(DockPlacement? placement = null)
    {

    }
}