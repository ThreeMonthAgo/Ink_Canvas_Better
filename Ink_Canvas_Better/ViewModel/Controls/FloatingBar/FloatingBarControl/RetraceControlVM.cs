using System.Windows;
using Ink_Canvas_Better.Utilities.Attributes;
using Ink_Canvas_Better.Utilities.Bases;
using Ink_Canvas_Better.View.Controls.FloatingBar.FloatingBarControl;
using Newtonsoft.Json;

namespace Ink_Canvas_Better.ViewModel.Controls.FloatingBar.FloatingBarControl;

[Component(
    viewType: typeof(RetraceControl),
    guid: "17942105-E0A2-493C-A1F5-F5A86EE8D7DC")]
public class RetraceControlVM : FloatingBarViewModelBase
{
    // ignored below
    private bool _isOpen = false;
    private Visibility _textVisibility = Visibility.Collapsed;

    [JsonIgnore]
    public bool IsOpen
    {
        get { return _isOpen; }
        set { SetProperty(ref _isOpen, value, false); }
    }

    [JsonIgnore]
    public Visibility TextVisibility
    {
        get { return _textVisibility; }
        set { SetProperty(ref _textVisibility, value, false); }
    }
}
