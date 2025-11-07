using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ink_Canvas_Better.Services;

namespace Ink_Canvas_Better.Controls
{
    internal interface ISerializableControl
    {
        static Guid ControlGuid { get; }
    }
}
