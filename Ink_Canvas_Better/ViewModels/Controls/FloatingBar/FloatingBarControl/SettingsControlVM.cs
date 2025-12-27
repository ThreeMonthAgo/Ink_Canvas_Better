using System;
using System.Windows;
using Ink_Canvas_Better.Controls.FloatingBar.FloatingBarControl;
using Ink_Canvas_Better.Services.JsonConverter;
using Ink_Canvas_Better.Utilities.Attributes;
using Ink_Canvas_Better.Utilities.Bases;
using Newtonsoft.Json;

namespace Ink_Canvas_Better.ViewModels.Controls.FloatingBar.FloatingBarControl;

[Component(
    viewType: typeof(SettingsControl),
    guid: "8AA94A7A-4847-4ED2-930F-292A7BFBA7CB")]
public class SettingsControlVM : ViewModelBase
{
    // ignored below
    private Visibility _textVisibility = Visibility.Collapsed;

    #region

    [JsonIgnore]
    public Visibility TextVisibility
    {
        get { return _textVisibility; }
        set { SetProperty(ref _textVisibility, value); }
    }

    #endregion
}
