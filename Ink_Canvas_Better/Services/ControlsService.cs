using System.Collections.Concurrent;
using System.Windows.Controls;
using Ink_Canvas_Better.Controls;

namespace Ink_Canvas_Better.Services
{
    internal class ControlsService
    {
        private readonly ConcurrentDictionary<Guid,Type> _controls = new();

        public bool TryRegisterControl<T>(Guid guid) where T : ISerializableControl
        {
            return _controls.TryAdd(guid,typeof(T));
        }

        public bool UnregisterControl(Guid guid, out Type? type)
        {
            return _controls.TryRemove(guid, out type);
        }

        public Control CreateControl(Guid guid)
        {
            Type type;
            _controls.TryGetValue(guid,out type);
            var control = Activator.CreateInstance(type) as Control;
            return control;
        }

        public bool TryCreateControl(Guid guid, out Control? control)
        {
            Type type;
            _controls.TryGetValue(guid, out type);
            if (type == null)
            {
                control = null;
                return false;
            }
            else
            {
                control = Activator.CreateInstance(type) as Control;
                return true;
            }
        }
    }
}
