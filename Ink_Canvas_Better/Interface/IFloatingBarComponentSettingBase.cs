using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Ink_Canvas_Better.Services;

namespace Ink_Canvas_Better.Interface
{
    /// <summary>
    /// All floating bar component settings must implement this interface to be recognized by the application.
    /// </summary>
    public interface IFloatingBarComponentSettingBase
    {
        /// <summary>
        /// Specific guid of the component, please ensure its uniqueness.
        /// </summary>
        public string ComponentGuid { get; }

        public object Settings { get; set; }

        public bool TryInvoke();
    }
}
