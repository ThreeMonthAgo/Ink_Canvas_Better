using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using Ink_Canvas_Better.Controls.Controls.ICBInkCanvas;
using Ink_Canvas_Better.Controls.ICBInkCanvas;

namespace ICBUITest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Pen_Click(object sender, RoutedEventArgs e)
        {
            ICBInkCanvas.EditingMode = InkCanvasEditingMode.Ink;
        }

        private void Button_EraseByStroke_Click(object sender, RoutedEventArgs e)
        {
            ICBInkCanvas.EditingMode = InkCanvasEditingMode.EraseByStroke;
        }

        private void Button_EraseByPoint_Click(object sender, RoutedEventArgs e)
        {
            ICBInkCanvas.EditingMode = InkCanvasEditingMode.EraseByPoint;
        }

        private void Button_Select_Click(object sender, RoutedEventArgs e)
        {
            ICBInkCanvas.EditingMode = InkCanvasEditingMode.Select;
        }

        private void Button_Apply_Click(object sender, RoutedEventArgs e)
        {
            if (TextBox_Width == null || TextBox_Height == null || ICBInkCanvas == null) return;
            try
            {
                ApplyToICBInkCanvas();
            }
            catch { }
        }

        private void ApplyToICBInkCanvas()
        {
            ICBInkCanvas.DefaultDrawingAttributes.Width = double.Parse(TextBox_Width.Text);
            ICBInkCanvas.DefaultDrawingAttributes.Height = double.Parse(TextBox_Height.Text);
            ICBInkCanvas.DefaultStrokeInfo = ICBInkCanvas.StrokeRegistrar.RegisteredStrokes[ComboBox_StrokeType.SelectedIndex];
        }

        private void Button_Redo_Click(object sender, RoutedEventArgs e) => ICBInkCanvas.Redo();

        private void Button_Undo_Click(object sender, RoutedEventArgs e) => ICBInkCanvas.Undo();

        private void Button_Clear_Click(object sender, RoutedEventArgs e) => ICBInkCanvas.Clear();

        private void ICBInkCanvas_Loaded(object sender, RoutedEventArgs e)
        {
            List<string> typeName = [];
            foreach (var item in ICBInkCanvas.StrokeRegistrar.RegisteredStrokes)
            {
                if (item.StrokeType is null)
                {
                    typeName.Add("Default");
                }
                else
                {
                    typeName.Add(item.StrokeType.Name);
                }
            }
            ComboBox_StrokeType.ItemsSource = typeName;
            ComboBox_StrokeType.SelectedIndex = 0;
            ApplyToICBInkCanvas();
        }
    }
}