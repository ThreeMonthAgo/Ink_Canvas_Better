using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Ink_Canvas_Better.Services;
using Newtonsoft.Json;

namespace Ink_Canvas_Better.Utilities.Bases;

/// <remarks>
/// Note that all classes that inherit from this base class should be marked with the
/// <see cref="Attributes.ComponentAttribute"/> attribute for proper serialization and deserialization.
/// </remarks>
public abstract class ViewModelBase : INotifyPropertyChanged
{
    /// <summary>
    /// Sets the proprtty.
    /// Invokes OnPropertyChanged if the value has changed or "force" is true.
    /// </summary>
    protected virtual void SetProperty<T>(
        ref T field,
        T newValue,
        [CallerMemberName] string? propertyName = null,
        bool force = true)
    {
        if (EqualityComparer<T>.Default.Equals(field, newValue))
        {
            if (force) OnPropertyChanged(propertyName);
        }
        else
        {
            field = newValue;
            OnPropertyChanged(propertyName);
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null, bool force = true)
    {
        if (!IsInitializing) App.GetService<SettingsService>().SaveSettings();
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    /// <summary>
    /// Remember to set this to false after initialization is complete otherwise settings will not be saved.
    /// </summary>
    [JsonIgnore]
    public bool IsInitializing { get; set; } = true;
}
