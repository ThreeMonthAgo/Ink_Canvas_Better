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
using static Ink_Canvas_Better.Enums;

namespace Ink_Canvas_Better.Services
{
    public class InkCanvasService(MainWindow mainWindow)
    {
        private readonly SolidColorBrush NearlyTransparent = new(Color.FromArgb(1,255,255,255));
        private readonly SolidColorBrush Transparent = Brushes.Transparent;
        private MainWindow mainWindow = mainWindow;


        private EditingMode _currentEditingMode;
        public EditingMode CurrentEditingMode
        {
            get => _currentEditingMode;
            set
            {
                if (_currentEditingMode == value) return;
                _currentEditingMode = value;
                InkCanvas inkCanvas = mainWindow.MW_InkCanvas;
                switch (value)
                {
                    case EditingMode.None:
                        inkCanvas.Background = Transparent;
                        break;
                    case EditingMode.Ink:
                        inkCanvas.Background = NearlyTransparent;
                        break;
                    case EditingMode.Highlighter:
                    case EditingMode.Select:
                    case EditingMode.EraseByPoint:
                    case EditingMode.EraseByStroke:
                    case EditingMode.Shape:
                        throw new NotImplementedException();
                }
            }
        }


    }
}
