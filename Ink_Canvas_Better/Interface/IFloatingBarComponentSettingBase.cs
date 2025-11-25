using System;
using System.Collections.Generic;
using System.Text;

namespace Ink_Canvas_Better.Interface
{
    internal interface IFloatingBarComponentSettingBase
    {
        public object Settings { get; set; }
        static Guid Guid { get; }
    }
}
