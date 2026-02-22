using System.Windows;
using Ink_Canvas_Better.Utilities.Attributes;
using Ink_Canvas_Better.Utilities.Bases;
using Ink_Canvas_Better.View.Controls.FloatingBar.FloatingBarControl;
using Newtonsoft.Json;

namespace Ink_Canvas_Better.ViewModel.Controls.FloatingBar.FloatingBarControl;

[Component(
    viewType: typeof(ClearControl),
    guid: "FF5724A4-8232-48BD-926D-73CBFB7DDDE5")]
public class ClearControlVM : FloatingBarViewModelBase
{
    // ignored below
    private bool _isOpen = false;
    private Visibility _textVisibility = Visibility.Collapsed;

    [JsonIgnore]
    public bool IsOpen
    {
        get { return _isOpen; }
        set { SetProperty(ref _isOpen, value); }
    }

    [JsonIgnore]
    public Visibility TextVisibility
    {
        get { return _textVisibility; }
        set { SetProperty(ref _textVisibility, value); }
    }
}
