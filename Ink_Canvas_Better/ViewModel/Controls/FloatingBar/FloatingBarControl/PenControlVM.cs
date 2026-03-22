using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Ink;
using System.Windows.Media;
using Ink_Canvas_Better.Model;
using Ink_Canvas_Better.Services;
using Ink_Canvas_Better.Utilities.Attributes;
using Ink_Canvas_Better.Utilities.Bases;
using Ink_Canvas_Better.Utilities.Interface;
using Ink_Canvas_Better.View.Controls.FloatingBar.FloatingBarControl;
using Newtonsoft.Json;
using static Ink_Canvas_Better.Utilities.Enums.InkCanvas;
using ColorConverter = Ink_Canvas_Better.Helpers.Converter.ColorConverter;

namespace Ink_Canvas_Better.ViewModel.Controls.FloatingBar.FloatingBarControl;

[Component(
    viewType: typeof(PenControl),
    guid: "87F7581C-364A-49D7-93C3-3355A8415D38")]
public class PenControlVM : FloatingBarViewModelBase
{
    private int _gridViewSelectedIndex = 0;
    private ObservableCollection<SolidColorBrush> _colorCollection =
        [
            ColorConverter.HexToSolidColorBrush("#FFFFFF"),
            ColorConverter.HexToSolidColorBrush("#000000"),
            ColorConverter.HexToSolidColorBrush("#A72C1D"),
            ColorConverter.HexToSolidColorBrush("#E03B27"),
            ColorConverter.HexToSolidColorBrush("#EFC046"),
            ColorConverter.HexToSolidColorBrush("#FCFC58"),
            ColorConverter.HexToSolidColorBrush("#A0CB64"),
            ColorConverter.HexToSolidColorBrush("#59AA5C"),
            ColorConverter.HexToSolidColorBrush("#61ADE9"),
            ColorConverter.HexToSolidColorBrush("#4170B8"),
            ColorConverter.HexToSolidColorBrush("#19275C"),
            ColorConverter.HexToSolidColorBrush("#673C98"),
        ];
    private int _thickness = 1;
    private byte _alpha = 0xFF;

    // ignored below
    private bool _isOpen = false;
    private SolidColorBrush _ellipseFill;
    private Visibility _textVisibility = Visibility.Collapsed;

    #region

    public int GridViewSelectedIndex
    {
        get { return _gridViewSelectedIndex; }
        set { SetProperty(ref _gridViewSelectedIndex, value); }
    }

    public ObservableCollection<SolidColorBrush> ColorCollection
    {
        get { return _colorCollection; }
        set { SetProperty(ref _colorCollection, value); }
    }

    public int Thickness
    {
        get { return _thickness; }
        set { SetProperty(ref _thickness, value); }
    }

    public byte Alpha
    {
        get { return _alpha; }
        set { SetProperty(ref _alpha, value); }
    }

    [JsonIgnore]
    public bool IsOpen
    {
        get { return _isOpen; }
        set { SetProperty(ref _isOpen, value, false); }
    }

    [JsonIgnore]
    public SolidColorBrush EllipseFill
    {
        get { return _ellipseFill; }
        set { SetProperty(ref _ellipseFill, value, false); }
    }

    [JsonIgnore]
    public Visibility TextVisibility
    {
        get { return _textVisibility; }
        set { SetProperty(ref _textVisibility, value, false); }
    }

    #endregion

    public void Click()
    {
        var mainWindowVM = IApp.GetService<SettingsService>().Settings.MainWindowVM;
        if (mainWindowVM.CurrentEditingMode != EditingMode.Ink)
        {
            this.Apply();
        }
        else
        {
            this.IsOpen = true;
        }
    }

    public void Apply()
    {
        try
        {
            var seletedIndex = this.GridViewSelectedIndex;
            var mainWindowVM = IApp.GetService<SettingsService>().Settings.MainWindowVM;
            // UI
            this.EllipseFill = this.ColorCollection[seletedIndex];
            // InkCanvas
            mainWindowVM.CurrentDrawingAttributes.Color = Color.FromArgb(
                this.Alpha,
                this.ColorCollection[seletedIndex].Color.R,
                this.ColorCollection[seletedIndex].Color.G,
                this.ColorCollection[seletedIndex].Color.B
                );
            mainWindowVM.CurrentDrawingAttributes.StylusTip = StylusTip.Ellipse;
            mainWindowVM.CurrentDrawingAttributes.Width = mainWindowVM.CurrentDrawingAttributes.Height = this.Thickness;
            mainWindowVM.CurrentEditingMode = EditingMode.Ink;
        }
        catch (Exception) { }
    }
}
