using System.Collections.Concurrent;
using System.Windows.Controls;
using Ink_Canvas_Better.Controls;
using Ink_Canvas_Better.Interface;
using Microsoft.Extensions.DependencyInjection;

namespace Ink_Canvas_Better.Services
{
    public class ControlsService
    {
        private readonly ConcurrentDictionary<string,Type> _controls = new();
        IServiceProvider serviceProvider;

        public ControlsService(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public bool TryRegisterControl<T>(string guid)
        {
            return _controls.TryAdd(guid,typeof(T));
        }

        public bool UnregisterControl(string guid, out Type? type)
        {
            return _controls.TryRemove(guid, out type);
        }

        public IFloatingBarComponentSettingBase CreateControl(string guid)
        {
            _controls.TryGetValue(guid, out Type type);
            var c = ActivatorUtilities.CreateInstance(serviceProvider, type) as IFloatingBarComponentSettingBase;
            return c;
        }

        public bool TryCreateControl(string guid, out IFloatingBarComponentSettingBase? control)
        {
            _controls.TryGetValue(guid, out Type type);
            if (type == null)
            {
                control = null;
                return false;
            }
            else
            {
                control = ActivatorUtilities.CreateInstance(serviceProvider, type) as IFloatingBarComponentSettingBase;
                return true;
            }
        }
    }
}
