using System.Windows;
using Ink_Canvas_Better.Services;
using Ink_Canvas_Better.Utilities.Attributes;
using Ink_Canvas_Better.Utilities.Bases;
using Ink_Canvas_Better.Utilities.Interface;
using Ink_Canvas_Better.View.Controls.FloatingBar.FloatingBarControl;
using Newtonsoft.Json;
using static Ink_Canvas_Better.Utilities.Enums.InkCanvas;

namespace Ink_Canvas_Better.ViewModel.Controls.FloatingBar.FloatingBarControl;

[Component(
    viewType: typeof(CursorControl),
    guid: "D034499E-882E-41DF-BE4B-C7446870A93C")]
public class CursorControlVM : FloatingBarViewModelBase
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

    public void Apply()
    {
        IApp.GetService<SettingsService>().Settings.MainWindowVM.CurrentEditingMode = EditingMode.None;
    }
}
