using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
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

            this.Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            var stkTypes = Enum.GetValues<ICBInkCanvas.StrokeType>();
            ComboBox_StrokeType.ItemsSource = stkTypes;
            ComboBox_StrokeType.SelectedIndex = 0;
            TextBox_Size_TextChanged(null, null);
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

        private void TextBox_Size_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (TextBox_Width == null || TextBox_Height == null || ICBInkCanvas == null) return;
            ICBInkCanvas.DefaultDrawingAttributes.Width = double.Parse(TextBox_Width.Text);
            ICBInkCanvas.DefaultDrawingAttributes.Height = double.Parse(TextBox_Height.Text);
        }
    }
}