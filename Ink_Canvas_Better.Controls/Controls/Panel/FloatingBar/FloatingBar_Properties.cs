using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows.Controls;

namespace Ink_Canvas_Better.Controls.Panel
{
    public partial class FloatingBar
    {
        private ObservableCollection<UIElementCollection> _controlsCollection = [];
        public ObservableCollection<UIElementCollection> ControlsCollection
        {
            get => _controlsCollection;
            set
            {
                if (_controlsCollection != null) _controlsCollection.CollectionChanged -= OnControlsCollectionChanged;
                _controlsCollection = value;
                if (_controlsCollection != null) _controlsCollection.CollectionChanged += OnControlsCollectionChanged;
            }
        }

    }
}
