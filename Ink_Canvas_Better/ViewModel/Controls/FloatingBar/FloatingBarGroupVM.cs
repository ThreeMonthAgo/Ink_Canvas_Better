using System.Collections.ObjectModel;
using System.Windows.Controls;
using Ink_Canvas_Better.Utilities.Attributes;
using Ink_Canvas_Better.Utilities.Bases;
using Ink_Canvas_Better.View.Controls.FloatingBar;

namespace Ink_Canvas_Better.ViewModel.Controls.FloatingBar;

[Component(
    viewType: typeof(FloatingBarGroup),
    guid: "B1E2F3A4-5678-90AB-CDEF-1234567890AB")]
public class FloatingBarGroupVM : FloatingBarViewModelBase
{
    private ObservableCollection<FloatingBarViewModelBase>? _items = [];
    private double _spacing = 4.0;
    private Orientation _orientation = Orientation.Horizontal;

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

    #endregion
}