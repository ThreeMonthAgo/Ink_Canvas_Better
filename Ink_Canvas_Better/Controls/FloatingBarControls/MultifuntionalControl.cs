using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using Ink_Canvas_Better.Services;

namespace Ink_Canvas_Better.Controls.FloatingBarControls
{
    internal class MultifuntionalControl : FloatingBarControlBase, ISerializableControl
    {
        public Guid ControlGuid => new("{03C5FD8D-2880-40F7-BAC5-9D83C347162C}");

        MultifuntionalControl() {
            Source = (DrawingImage)this.Resources["FUI.Drag"];
        }

    }
}
