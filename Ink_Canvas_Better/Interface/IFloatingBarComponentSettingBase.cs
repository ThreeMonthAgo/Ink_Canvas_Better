using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

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

        /// <summary>
        /// If the component is not a container, please set it to null.
        /// </summary>
        public ObservableCollection<IFloatingBarComponentSettingBase>? Items { get; set; }
    }
}
