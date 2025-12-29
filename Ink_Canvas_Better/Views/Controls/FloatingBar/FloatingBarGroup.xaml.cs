using System;
using System.Collections;
using System.Windows;
using System.Windows.Controls;
using Ink_Canvas_Better.Utilities.Bases;
using Ink_Canvas_Better.Utilities.Interface;
using Ink_Canvas_Better.ViewModels.Controls.FloatingBar;

namespace Ink_Canvas_Better.Controls.FloatingBar;

public partial class FloatingBarGroup : UserControl, IFloatingBarComponentSettingBase
{
    public object Settings => DataContext as FloatingBarGroupVM;

    public FloatingBarGroup()
    {
        foreach (DictionaryEntry resource in Application.Current.Resources)
        {
            if (!this.Resources.Contains(resource.Key))
            {
                this.Resources.Add(resource.Key, resource.Value);
            }
        }
        InitializeComponent();

        this.Loaded += FloatingBarGroup_Loaded;
    }

    private void FloatingBarGroup_Loaded(object sender, RoutedEventArgs e)
    {
        (Settings as FloatingBarGroupVM).IsInitializing = false;
    }

    public bool TryInvoke() => true;

    public FloatingBarGroup Add(ViewModelBase component)
    {
        (Settings as FloatingBarGroupVM).Items.Add(component);
        return this;
    }
}
