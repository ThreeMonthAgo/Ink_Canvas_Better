using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Ink_Canvas_Better.Interfaces.FloatingBar
{
    internal interface IFloatingBarSettingBase
    {
        public object Settings { get; set; }

        public ObservableCollection<IFloatingBarGroupSettingBase> Groups { get; }
    }
}
