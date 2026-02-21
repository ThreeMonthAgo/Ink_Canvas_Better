using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using Ink_Canvas_Better.Services;
using Ink_Canvas_Better.Utilities.Interface;
using Newtonsoft.Json;

namespace Ink_Canvas_Better.ViewModel.Windows
{
    internal class SettingsWindowVM : INotifyPropertyChanged
    {
        private Page _selectedPage;

        #region

        public Page SelectedPage
        {
            get => _selectedPage;
            set => SetProperty(ref _selectedPage, value);
        }

        #endregion

        protected virtual void SetProperty<T>(
            ref T field,
            T newValue,
            Action? onChanged = null,
            bool force = true,
            [CallerMemberName] string? propertyName = null)
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
            if (!IsInitializing) IApp.GetService<SettingsService>().SaveSettings();
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        [JsonIgnore]
        public bool IsInitializing { get; set; } = true;
    }
}
