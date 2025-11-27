using System.Collections.Concurrent;
using System.Windows.Controls;
using Ink_Canvas_Better.Controls;
using Ink_Canvas_Better.Interface;
using Microsoft.Extensions.DependencyInjection;

namespace Ink_Canvas_Better.Services
{
    internal class ControlsService
    {
        private readonly ConcurrentDictionary<Guid,Type> _controls = new();
        IServiceProvider serviceProvider;

        public ControlsService(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public bool TryRegisterControl<T>(Guid guid)
        {
            return _controls.TryAdd(guid,typeof(T));
        }

        public bool UnregisterControl(Guid guid, out Type? type)
        {
            return _controls.TryRemove(guid, out type);
        }

        public Control CreateControl(Guid guid)
        {
            _controls.TryGetValue(guid, out Type type);
            var c = ActivatorUtilities.CreateInstance(serviceProvider, type) as Control;
            return c;
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
                control = ActivatorUtilities.CreateInstance(serviceProvider, type) as Control;
                return true;
            }
        }
    }
}
