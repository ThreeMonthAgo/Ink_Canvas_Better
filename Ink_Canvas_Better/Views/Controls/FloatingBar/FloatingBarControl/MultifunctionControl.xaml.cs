using System;
using System.Collections;
using System.Windows;
using System.Windows.Controls;
using Ink_Canvas_Better.Utilities.Interface;
using Ink_Canvas_Better.ViewModels.Controls.FloatingBar.FloatingBarControl;

namespace Ink_Canvas_Better.Controls.FloatingBar.FloatingBarControl;

public partial class MultifunctionControl : UserControl, IFloatingBarComponentSettingBase
{
    public object Settings { get; set; } = new MultifunctionControlVM();

    public MultifunctionControl()
    {
        foreach (DictionaryEntry resource in Application.Current.Resources)
        {
            if (!this.Resources.Contains(resource.Key))
            {
                this.Resources.Add(resource.Key, resource.Value);
            }
        }
        InitializeComponent();

        this.DataContext = Settings;
    }

    public bool TryInvoke() => true;

    private void Thumb_DragDelta(object sender, System.Windows.Controls.Primitives.DragDeltaEventArgs e)
    {
        // TODO: fold the floatingbar
    }
}
