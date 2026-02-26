using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Ink_Canvas_Better.Controls.ICBInkCanvas;

namespace Ink_Canvas_Better.Gallery.Pages
{
    /// <summary>
    /// Interaction logic for ICBInkCanvasPage.xaml
    /// </summary>
    public partial class ICBInkCanvasPage : Page
    {
        public ICBInkCanvasPage()
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

        private void ApplyToICBInkCanvas()
        {
            ICBInkCanvas.DefaultDrawingAttributes.Width = double.Parse(TextBox_Width.Text);
            ICBInkCanvas.DefaultDrawingAttributes.Height = double.Parse(TextBox_Height.Text);
            ICBInkCanvas.DefaultStrokeInfo = ICBInkCanvas.StrokeRegistrar.RegisteredStrokes[ComboBox_StrokeType.SelectedIndex];
        }

        private void Button_Redo_Click(object sender, RoutedEventArgs e) => ICBInkCanvas.Redo();

        private void Button_Undo_Click(object sender, RoutedEventArgs e) => ICBInkCanvas.Undo();

        private void Button_Clear_Click(object sender, RoutedEventArgs e) => ICBInkCanvas.Clear();

        private void ComboBox_StrokeType_SelectionChanged(object sender, SelectionChangedEventArgs e) => Apply();

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e) => Apply();

        private void Apply()
        {
            if (TextBox_Width == null || TextBox_Height == null || ICBInkCanvas == null) return;
            if (!(func0(TextBox_Width) & func0(TextBox_Height))) return;
            try
            {
                ApplyToICBInkCanvas();
            }
            catch { }

            bool func0(TextBox c)
            {
                if (c.Text.Any(c => !char.IsDigit(c)))
                {
                    c.BorderBrush = Brushes.OrangeRed;
                    return false;
                }
                else
                {
                    c.BorderBrush = Brushes.AliceBlue;
                    return true;
                }
            }
        }

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
