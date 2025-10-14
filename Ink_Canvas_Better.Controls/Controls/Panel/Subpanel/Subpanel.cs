using Ink_Canvas_Better.Controls.Helpers;
using Ink_Canvas_Better.Controls.Helpers.Converter;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Markup;

namespace Ink_Canvas_Better.Controls.Panel
{
    [ContentProperty("Child")]
    public partial class Subpanel : ExPopup
    {
        static Subpanel()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Subpanel), new FrameworkPropertyMetadata(typeof(Subpanel)));
        }

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);

            _titleTextBlock.Text = Title;
            _titleTextBlock.SetBinding(TextBlock.TextProperty, new Binding("Title") { Source = this });

            _titleBarGrid.Background = ThemeHelper.DefaultBackgroundColor;
            _titleBarGrid.Children.Add(_titleTextBlock);
            Grid.SetColumn(_titleTextBlock, 0);

            _pinTextBlock.SetBinding(TextBlock.TextProperty, new Binding("StaysOpen")
            {
                Source = this,
                Converter = new BooleanToTextCconverter_Pin()
            });
            _pinButton.Content = _pinTextBlock;
            _pinButton.Click += OnPinButtonClicked;
            _titleBarGrid.Children.Add(_pinButton);
            Grid.SetColumn(_pinButton, 1);

            _closeButton.Click += OnCloseButtonClicked;
            _titleBarGrid.Children.Add(_closeButton);
            Grid.SetColumn(_closeButton, 2);

            _contentPresenter.SetBinding(ContentPresenter.ContentProperty, new Binding("Child") { Source = MemberwiseClone() });

            _mainGrid.Children.Add(_titleBarGrid);
            Grid.SetRow(_titleBarGrid, 0);

            _mainGrid.Children.Add(_contentPresenter);
            Grid.SetRow(_contentPresenter, 1);

            _titleBarGrid.SetBinding(VisibilityProperty, new Binding("IsShowHeader")
            {
                Source = this,
                Converter = new BooleanToVisibilityConverter()
            });

            _mainBorder.Child = _mainGrid;
            _mainBorder.SetBinding(MarginProperty, new Binding("Margin") { Source = this });

            _transparentGrid.Children.Add(_mainBorder);
            Child = _transparentGrid;
        }
    }
}
