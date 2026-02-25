using System.Windows;
using Ink_Canvas_Better.Utilities.Attributes;
using Ink_Canvas_Better.Utilities.Bases;
using Ink_Canvas_Better.View.Controls.FloatingBar.FloatingBarControl;
using Newtonsoft.Json;

namespace Ink_Canvas_Better.ViewModel.Controls.FloatingBar.FloatingBarControl;

[Component(
    viewType: typeof(NextPageControl),
    guid: "69924F29-4EE7-401C-B983-08CFB49AB859")]
public class NextPageControlVM : FloatingBarViewModelBase
{
    // ignored below
    private Visibility _textVisibility = Visibility.Collapsed;

    #region

    [JsonIgnore]
    public Visibility TextVisibility
    {
        get { return _textVisibility; }
        set { SetProperty(ref _textVisibility, value, false); }
    }

    #endregion
}
