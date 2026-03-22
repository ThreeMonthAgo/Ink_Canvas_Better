using System.Windows;
using System.Windows.Ink;
using Ink_Canvas_Better.Services;
using Ink_Canvas_Better.Utilities.Attributes;
using Ink_Canvas_Better.Utilities.Bases;
using Ink_Canvas_Better.Utilities.Interface;
using Ink_Canvas_Better.View.Controls.FloatingBar.FloatingBarControl;
using Ink_Canvas_Better.View.Windows;
using Newtonsoft.Json;
using static Ink_Canvas_Better.Utilities.Enums.InkCanvas;

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
        set { SetProperty(ref _isOpen, value, false); }
    }

    [JsonIgnore]
    public Visibility TextVisibility
    {
        get { return _textVisibility; }
        set { SetProperty(ref _textVisibility, value, false); }
    }

    [JsonIgnore]
    public bool IsShowClearButton
    {
        get { return _isShowClearButton; }
        set { SetProperty(ref _isShowClearButton, value, false); }
    }

    #endregion

    public void Click()
    {
        var mainWindowVM = IApp.GetService<SettingsService>().Settings.MainWindowVM;
        if (mainWindowVM.CurrentEditingMode != EditingMode.EraseByStroke
            && mainWindowVM.CurrentEditingMode != EditingMode.EraseByPoint)
        {
            this.Apply();
        }
        else
        {
            this.IsOpen = true;
        }
    }

    public void Clear()
    {
        IApp.GetService<MainWindow>().ClearStrokes();
    }

    public void Apply()
    {
        try
        {
            var mainWindowVM = IApp.GetService<SettingsService>().Settings.MainWindowVM;
            switch (this.GridViewSelectedIndex)
            {
                case 0:
                    mainWindowVM.CurrentEditingMode = EditingMode.EraseByStroke;
                    break;
                case 1:
                    mainWindowVM.CurrentDrawingAttributes.StylusTip = StylusTip.Ellipse;
                    mainWindowVM.EraserShape = new EllipseStylusShape(this.Thickness, this.Thickness);
                    mainWindowVM.CurrentEditingMode = EditingMode.Ink; // necessary
                    mainWindowVM.CurrentEditingMode = EditingMode.EraseByPoint;
                    break;
                case 2:
                    mainWindowVM.CurrentDrawingAttributes.StylusTip = StylusTip.Rectangle;
                    mainWindowVM.EraserShape = new RectangleStylusShape(this.Thickness, this.Thickness);
                    mainWindowVM.CurrentEditingMode = EditingMode.Ink; // necessary
                    mainWindowVM.CurrentEditingMode = EditingMode.EraseByPoint;
                    break;
            }
        }
        catch (Exception) { }
    }
}
