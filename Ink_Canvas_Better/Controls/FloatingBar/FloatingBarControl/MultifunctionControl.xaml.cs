using System;
using System.Windows.Controls;
using Ink_Canvas_Better.Interface;
using Ink_Canvas_Better.ViewModels.Controls.FloatingBar.FloatingBarControl;

namespace Ink_Canvas_Better.Controls.FloatingBar.FloatingBarControl;

public partial class MultifunctionControl : UserControl, IFloatingBarComponentSettingBase
{
    public static string Guid { get; } = "03C5FD8D-2880-40F7-BAC5-9D83C347162C";
    public string ComponentGuid => Guid; 
    public object Settings { get; set; } = new MultifunctionControlVM();

    public MultifunctionControl()
    {
        InitializeComponent();

        DataContext = Settings;
    }

    public bool TryInvoke() => true;

    private void Thumb_DragDelta(object sender, System.Windows.Controls.Primitives.DragDeltaEventArgs e)
    {
        // TODO: fold the floatingbar
    }
}
