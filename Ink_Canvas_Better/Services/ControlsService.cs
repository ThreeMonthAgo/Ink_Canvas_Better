using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.DirectoryServices.ActiveDirectory;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ink_Canvas_Better.Controls;

namespace Ink_Canvas_Better.Services
{
    internal class ControlsService
    {
        private readonly ConcurrentDictionary<Guid,ISerializableControl> _controls = new();

        public bool TryRegisterControl(ISerializableControl control)
        {
            return _controls.TryAdd(control.ControlGuid, control);
        }

        public bool TryGetControl(Guid guid, out ISerializableControl control)
        {
            return _controls.TryGetValue(guid, out control);
        }

        public bool UnregisterControl(Guid guid)
        {
            return _controls.TryRemove(guid, out _);
        }

        public bool UnregisterControl(ISerializableControl control)
        {
            return control != null && _controls.TryRemove(control.ControlGuid, out _);
        }

        public IEnumerable<ISerializableControl> GetAllControls()
        {
            return _controls.Values;
        }
    }
}
