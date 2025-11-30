using System.Collections.Concurrent;
using System.Windows.Controls;
using Ink_Canvas_Better.Controls;
using Ink_Canvas_Better.Interface;
using Microsoft.Extensions.DependencyInjection;

namespace Ink_Canvas_Better.Services
{
    public class ControlsService(IServiceProvider serviceProvider)
    {
        private readonly ConcurrentDictionary<string,Type> _controls = new();
        IServiceProvider serviceProvider = serviceProvider;

        public bool TryRegisterControl<T>(string guid)
        {
            return _controls.TryAdd(guid,typeof(T));
        }

        public bool UnregisterControl(string guid, out Type? type)
        {
            return _controls.TryRemove(guid, out type);
        }

        /// <summary>
        /// Creates an instance of a floating bar component setting based on the specified unique identifier.
        /// </summary>
        /// <remarks>The returned instance is created using dependency injection via the provided service
        /// provider. If the specified <paramref name="guid"/> does not match any registered component, the method
        /// returns <see langword="null"/>.</remarks>
        public IFloatingBarComponentSettingBase CreateControl(string guid)
        {
            _controls.TryGetValue(guid, out Type type);
            var c = ActivatorUtilities.CreateInstance(serviceProvider, type) as IFloatingBarComponentSettingBase;
            return c;
        }
        
        /// <summary>
        /// Attempts to create a floating bar control instance associated with the specified GUID.
        /// </summary>
        /// <remarks>This method uses the registered service provider to instantiate the control. If the
        /// GUID does not correspond to a known control type, no instance is created and the method returns
        /// false.</remarks>
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
