using System;
using System.Collections.Generic;
using System.Text;

namespace Ink_Canvas_Better.Interface
{
    /// <summary>
    /// All floating bar component settings must implement this interface to be recognized by the application.
    /// </summary>
    public interface IFloatingBarComponentSettingBase
    {
        object Settings { get; set; }
        public static string Guid { get; }
    }
}
