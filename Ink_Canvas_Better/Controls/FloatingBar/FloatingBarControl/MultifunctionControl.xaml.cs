using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Ink_Canvas_Better.Interface;
using Ink_Canvas_Better.Services;
using Ink_Canvas_Better.Windows;
using Newtonsoft.Json;

namespace Ink_Canvas_Better.Controls.FloatingBar.FloatingBarControl;

public partial class MultifunctionControl : UserControl, IFloatingBarComponentSettingBase
{
    public static string Guid { get; } = "03C5FD8D-2880-40F7-BAC5-9D83C347162C";
    public string ComponentGuid => Guid; 
    public object Settings { get; set; } = new MultifunctionControlSettings();

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

public class MultifunctionControlSettings : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        if (!IsInitializing) App.GetService<SettingsService>().SaveSettings();
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    [JsonIgnore]
    public bool IsInitializing { get; set; } = true;
}
