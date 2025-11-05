using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Ink_Canvas_Better.Controls.Panel;

namespace Ink_Canvas_Better.Controls
{
    class FloatingBar : Control
    {
        public ObservableCollection<FloatingBarGroup> Groups = [];

    }
}
