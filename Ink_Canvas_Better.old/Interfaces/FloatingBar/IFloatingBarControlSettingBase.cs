using System;
using System.Collections.Generic;
using System.Text;

namespace Ink_Canvas_Better.Interfaces.FloatingBar
{
    internal interface IFloatingBarControlSettingBase
    {
        public object Settings { get; set; }

        static Guid ControlGuid { get; }
    }
}
