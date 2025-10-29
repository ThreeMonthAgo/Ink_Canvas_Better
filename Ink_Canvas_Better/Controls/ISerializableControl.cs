using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ink_Canvas_Better.Controls
{
    internal interface ISerializableControl
    {
        string ControlGuid { get; }
    }
}
