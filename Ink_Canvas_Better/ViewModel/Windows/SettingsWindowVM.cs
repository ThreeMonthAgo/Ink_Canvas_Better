using System.Windows.Controls;
using Ink_Canvas_Better.Utilities.Bases;

namespace Ink_Canvas_Better.ViewModel.Windows
{
    internal class SettingsWindowVM : ViewModelBase
    {
        private Page _selectedPage;

        #region

        public Page SelectedPage
        {
            get => _selectedPage;
            set => SetProperty(ref _selectedPage, value);
        }

        #endregion
    }
}
