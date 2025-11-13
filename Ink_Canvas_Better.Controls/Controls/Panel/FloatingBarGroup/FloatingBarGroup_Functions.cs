using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace Ink_Canvas_Better.Controls.Panel
{
    public partial class FloatingBarGroup
    {
        public void Add(object obj)
        {
            this.Items.Add(obj);
        }

        public void Clear()
        {
            this.Items.Clear();
        }
    }
}
