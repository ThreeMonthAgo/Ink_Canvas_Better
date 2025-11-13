using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Ink_Canvas_Better.Controls.Panel
{
    partial class FloatingBar
    {
        public void Add(FloatingBarGroup fg)
        {
            this.Items.Add(fg);
        }

        public void Clear()
        {
            this.Items.Clear();
        }
    }
}
