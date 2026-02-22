using System.Windows;
using Ink_Canvas_Better.Utilities.Attributes;
using Ink_Canvas_Better.Utilities.Bases;
using Ink_Canvas_Better.View.Controls.FloatingBar.FloatingBarControl;
using Newtonsoft.Json;

namespace Ink_Canvas_Better.ViewModel.Controls.FloatingBar.FloatingBarControl;

[Component(
    viewType: typeof(EraserControl),
    guid: "F4A558A1-ABF1-4493-8D14-8D0D18363B72")]
public class EraserControlVM : FloatingBarViewModelBase
{
    private int _thickness = 20;
    private int _gridViewSelectedIndex = 0;

    // ignored below
    private bool _isOpen = false;
    private Visibility _textVisibility = Visibility.Collapsed;
    private bool _isShowClearButton = true;

    #region

    public int Thickness
    {
        get { return _thickness; }
        set { SetProperty(ref _thickness, value); }
    }

    public int GridViewSelectedIndex
    {
        get { return _gridViewSelectedIndex; }
        set { SetProperty(ref _gridViewSelectedIndex, value); }
    }

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

    [JsonIgnore]
    public bool IsShowClearButton
    {
        get { return _isShowClearButton; }
        set { SetProperty(ref _isShowClearButton, value); }
    }

    #endregion
}
