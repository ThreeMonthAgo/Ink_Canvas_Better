using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using Ink_Canvas_Better.Interface;
using Ink_Canvas_Better.Windows;

namespace Ink_Canvas_Better.Controls.FloatingBar.FloatingBarControl
{
    /// <summary>
    /// MultifunctionControl.xaml 的交互逻辑
    /// </summary>
    public partial class MultifunctionControl : FloatingBarComponentBase, IFloatingBarComponentSettingBase
    {
        public static string Guid { get; } = "03C5FD8D-2880-40F7-BAC5-9D83C347162C";
        public string ComponentGuid => Guid; 
        public ObservableCollection<IFloatingBarComponentSettingBase>? Items { get; set; } = null;


        FloatingBar floatingBar;
        MainWindow mainWindow;

        private bool _isMouseDown = false;
        private Point _mouseDownPos, _mouseUpPos, _mouseDownControlPos, _currentMousePos;

        public MultifunctionControl()
        {
            InitializeComponent();
            this.MouseDown += MultifuntionControl_MouseDown;
            this.MouseUp += MultifuntionControl_MouseUp;
        }

        #region Properties

        #region Source

        public ImageSource Source
        {
            get { return (ImageSource)GetValue(SourceProperty); }
            set { SetValue(SourceProperty, value); }
        }

        public static readonly DependencyProperty SourceProperty =
            DependencyProperty.Register("Source", typeof(ImageSource), typeof(MultifunctionControl), new PropertyMetadata(null));

        #endregion

        #region Text

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(MultifunctionControl), new PropertyMetadata("Text"));

        #endregion

        #region TextVisibility

        public Visibility TextVisibility
        {
            get { return (Visibility)GetValue(TextVisibilityProperty); }
            set { SetValue(TextVisibilityProperty, value); }
        }

        public static readonly DependencyProperty TextVisibilityProperty =
            DependencyProperty.Register("TextVisibility", typeof(Visibility), typeof(MultifunctionControl), new PropertyMetadata(Visibility.Collapsed));

        #endregion

        #region ImageWidth

        public double ImageWidth
        {
            get { return (double)GetValue(ImageWidthProperty); }
            set { SetValue(ImageWidthProperty, value); }
        }

        public static readonly DependencyProperty ImageWidthProperty =
            DependencyProperty.Register("ImageWidth", typeof(double), typeof(MultifunctionControl), new PropertyMetadata(40d));

        #endregion

        #region ImageHeight

        public double ImageHeight
        {
            get { return (double)GetValue(ImageHeightProperty); }
            set { SetValue(ImageHeightProperty, value); }
        }

        public static readonly DependencyProperty ImageHeightProperty =
            DependencyProperty.Register("ImageHeight", typeof(double), typeof(MultifunctionControl), new PropertyMetadata(40d));

        #endregion

        // Popup Properties

        #region Title

        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register(nameof(Title), typeof(string), typeof(MultifunctionControl), new PropertyMetadata("Subpanel"));

        #endregion

        #region IsOpen

        public bool IsOpen
        {
            get { return (bool)GetValue(IsOpenProperty); }
            set { SetValue(IsOpenProperty, value); }
        }

        public static readonly DependencyProperty IsOpenProperty =
            DependencyProperty.Register(nameof(IsOpen), typeof(bool), typeof(MultifunctionControl), new PropertyMetadata(false));

        #endregion

        #region StaysOpen

        public bool StaysOpen
        {
            get { return (bool)GetValue(StaysOpenProperty); }
            set { SetValue(StaysOpenProperty, value); }
        }

        public static readonly DependencyProperty StaysOpenProperty =
            DependencyProperty.Register(nameof(StaysOpen), typeof(bool), typeof(MultifunctionControl), new PropertyMetadata(false));

        #endregion

        #region PlacementTarget

        public UIElement PlacementTarget
        {
            get { return (UIElement)GetValue(PlacementTargetProperty); }
            set { SetValue(PlacementTargetProperty, value); }
        }

        public static readonly DependencyProperty PlacementTargetProperty =
            DependencyProperty.Register(nameof(PlacementTarget), typeof(UIElement), typeof(MultifunctionControl), new PropertyMetadata(null));

        #endregion

        #region TitleBarHeight

        public double TitleBarHeight
        {
            get { return (double)GetValue(TitleBarHeightProperty); }
            set { SetValue(TitleBarHeightProperty, value); }
        }

        public static readonly DependencyProperty TitleBarHeightProperty =
            DependencyProperty.Register(nameof(TitleBarHeight), typeof(double), typeof(MultifunctionControl), new PropertyMetadata(36d));

        #endregion

        #region CornerRadius

        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register(nameof(CornerRadius), typeof(CornerRadius), typeof(MultifunctionControl), new PropertyMetadata(new CornerRadius(4d)));

        #endregion

        #region Placement

        public PlacementMode Placement
        {
            get { return (PlacementMode)GetValue(PlacementProperty); }
            set { SetValue(PlacementProperty, value); }
        }

        public static readonly DependencyProperty PlacementProperty =
            DependencyProperty.Register(nameof(Placement), typeof(PlacementMode), typeof(MultifunctionControl), new PropertyMetadata(PlacementMode.Top));

        #endregion

        #region PopupAnimation

        public PopupAnimation PopupAnimation
        {
            get { return (PopupAnimation)GetValue(PopupAnimationProperty); }
            set { SetValue(PopupAnimationProperty, value); }
        }

        public static readonly DependencyProperty PopupAnimationProperty =
            DependencyProperty.Register(nameof(PopupAnimation), typeof(PopupAnimation), typeof(MultifunctionControl), new PropertyMetadata(PopupAnimation.Slide));

        #endregion

        #region PlacementRectangle

        public Rect PlacementRectangle
        {
            get { return (Rect)GetValue(PlacementRectangleProperty); }
            set { SetValue(PlacementRectangleProperty, value); }
        }

        public static readonly DependencyProperty PlacementRectangleProperty =
            DependencyProperty.Register(nameof(PlacementRectangle), typeof(Rect), typeof(MultifunctionControl), new PropertyMetadata(new Rect(0, 0, 0, 0)));

        #endregion

        #endregion

        private void MultifuntionControl_MouseDown(object sender, MouseButtonEventArgs e)
        {
            floatingBar = Ink_Canvas_Better.Helpers.VisualTreeHelper.GetParent<FloatingBar>(this);
            if (floatingBar == null)
            {
                return;
            }
            _isMouseDown = true;
            _mouseDownPos = e.GetPosition(mainWindow);
            if (floatingBar.RenderTransform is not TranslateTransform transform)
            {
                transform = new TranslateTransform();
            }
            _mouseDownControlPos = new Point(transform.X, transform.Y);
            this.MouseMove += MultifuntionControl_MouseMove;
            this.CaptureMouse();
            e.Handled = true;
        }

        private void MultifuntionControl_MouseMove(object sender, MouseEventArgs e)
        {
            if (_isMouseDown)
            {
                TranslateTransform transform = (TranslateTransform)floatingBar.RenderTransform;
                _currentMousePos = e.GetPosition(mainWindow);
                transform.X = _mouseDownControlPos.X + _currentMousePos.X - _mouseDownPos.X;
                transform.Y = _mouseDownControlPos.Y + _currentMousePos.Y - _mouseDownPos.Y;
            }
        }

        private void MultifuntionControl_MouseUp(object sender, MouseButtonEventArgs e)
        {
            this.MouseMove -= MultifuntionControl_MouseMove;
            this.ReleaseMouseCapture();
            _isMouseDown = false;
            _mouseUpPos = e.GetPosition(mainWindow);
            // TODO: fold the floatingbar
        }
    }
}
