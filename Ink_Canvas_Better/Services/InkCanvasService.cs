using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using Ink_Canvas_Better.Windows;

namespace Ink_Canvas_Better.Services
{
    internal class InkCanvasService
    {
        private readonly SolidColorBrush NearlyTransparent = new(Color.FromArgb(1,255,255,255));
        private readonly SolidColorBrush Transparent = Brushes.Transparent;

        public void SwitchInkCanvasMode(InkCanvasEditingMode em)
        {
            InkCanvas inkCanvas = AppHost.GetService<MainWindow>().MainInkCanvas;
            Grid grid = AppHost.GetService<MainWindow>().MainWindow_Grid;
            inkCanvas.EditingMode = em;
            // TODO
            switch (em)
            {
                case InkCanvasEditingMode.None:             // Cursor
                    inkCanvas.Background = Transparent;
                    break;
                case InkCanvasEditingMode.Ink:              // Pen and Highlighter
                    inkCanvas.Background = NearlyTransparent;
                    break;
                case InkCanvasEditingMode.Select:
                    break;
                case InkCanvasEditingMode.EraseByPoint:
                    break;
                case InkCanvasEditingMode.EraseByStroke:
                    break;
                case InkCanvasEditingMode.InkAndGesture:    // a special mode for pen and highlighter
                    break;
                case InkCanvasEditingMode.GestureOnly:      // Shape
                    break;
            }
            Debug.WriteLine(inkCanvas.EditingMode);
        }

        public InkCanvas Test()
        {
            return AppHost.GetService<MainWindow>().MainInkCanvas;
        }
    }
}
