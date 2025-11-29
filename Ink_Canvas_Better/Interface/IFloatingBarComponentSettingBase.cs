using System;
using System.Collections.Generic;
using System.Text;

namespace Ink_Canvas_Better.Interface
{
    public interface IFloatingBarComponentSettingBase
    {
        object Settings { get; set; }
        static string Guid { get; }
    }
}
