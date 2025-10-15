using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Ink_Canvas_Better.Controls.Panel
{
    public partial class Stackpanel
    {

        protected override Size MeasureOverride(Size availableSize)
        {
            double height = 0, width = 0;

            switch (Orientation)
            {
                case Orientation.Horizontal:
                    foreach (UIElement child in InternalChildren)
                    {
                        if (child == null) continue;
                        child.Measure(availableSize);
                        width += child.DesiredSize.Width;
                        if (child.DesiredSize.Height > height) height = child.DesiredSize.Height;
                    }
                    width += Spacing * InternalChildren.Count;
                    break;
                case Orientation.Vertical:
                    foreach (UIElement child in InternalChildren)
                    {
                        if (child == null) continue;
                        child.Measure(availableSize);
                        height += child.DesiredSize.Height;
                        if (child.DesiredSize.Width > width) width = child.DesiredSize.Width;
                    }
                    height += Spacing * InternalChildren.Count;
                    break;
            }
            return new Size(width, height);
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            double totalWidth = 0, totalHeight = 0;
            double x, y;
            switch (Orientation)
            {
                case Orientation.Horizontal:
                    for (int i = 0; i < InternalChildren.Count; i++)
                    {
                        y = totalHeight;
                        x = InternalChildren[i].DesiredSize.Width * i + Spacing * i;
                        totalWidth += Children[i].DesiredSize.Width;
                        Rect rect = new(x, y, Children[i].DesiredSize.Width, Children[i].DesiredSize.Height);
                        Children[i].Arrange(rect);
                    }
                    break;
                case Orientation.Vertical:
                    for (int i = 0; i < InternalChildren.Count; i++)
                    {
                        x = totalWidth;
                        y = InternalChildren[i].DesiredSize.Height * i + Spacing * i;
                        totalHeight += Children[i].DesiredSize.Height;
                        Rect rect = new(x, y, Children[i].DesiredSize.Width, Children[i].DesiredSize.Height);
                        Children[i].Arrange(rect);
                    }
                    break;
            }
            return finalSize;
        }
    }
}
