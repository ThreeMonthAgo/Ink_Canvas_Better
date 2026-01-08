using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using Microsoft.Xaml.Behaviors;
using iNKORE.UI.WPF.Helpers;
using LogicalTreeHelper = Ink_Canvas_Better.Helpers.LogicalTreeHelper;

namespace Ink_Canvas_Better.Behaviors
{
    public class DragBehavior : Behavior<Thumb>
    {
        /// <summary>
        /// The type of the control that you want to drag.
        /// </summary>
        /// <remarks>
        /// <b>Warning:</b> Some special type (e.g. <see cref="Popup"/>) needs to use LogicalTreeHelper, please set "<see cref="IsUseLogicalTreeHelper"/>" to true.
        /// </remarks>
        public Type TypeOfControlToDrag
        {
            get { return _typeOfControlToDrag; }
            set { _typeOfControlToDrag = value; }
        }
        private Type _typeOfControlToDrag;

        /// <summary>
        /// For use in some special conditions.
        /// </summary>
        public bool IsUseLogicalTreeHelper
        {
            get { return _isUseLogicalTreeHelper; }
            set { _isUseLogicalTreeHelper = value; }
        }
        private bool _isUseLogicalTreeHelper = false;

        public bool IsEnabled
        {
            get { return (bool)GetValue(IsEnabledProperty); }
            set { SetValue(IsEnabledProperty, value); }
        }

        public static readonly DependencyProperty IsEnabledProperty =
            DependencyProperty.Register("IsEnabled", typeof(bool), typeof(DragBehavior), new PropertyMetadata(true));

        protected override void OnAttached()
        {
            base.OnAttached();
            if (IsEnabled)
            {
                AssociatedObject.DragDelta += AssociatedObject_DragDelta;
            }
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            AssociatedObject.DragDelta -= AssociatedObject_DragDelta;
        }

        private void AssociatedObject_DragDelta(object sender, DragDeltaEventArgs e)
        {
            // Get control to drag
            object temp = IsUseLogicalTreeHelper ? LogicalTreeHelper.FindAscendant(AssociatedObject, TypeOfControlToDrag) : VisualTree.FindAscendant(AssociatedObject, TypeOfControlToDrag);
            if (temp == null) return;
            if (TypeOfControlToDrag == typeof(Popup))
            {
                // Popup requires special handling.
                var controlToDrag = temp as Popup;
                controlToDrag.HorizontalOffset += e.HorizontalChange;
                controlToDrag.VerticalOffset += e.VerticalChange;
                return;
            }
            else
            {
                // Get TranslateTransform
                var controlToDrag = temp as Control;
                TranslateTransform translateTransform;
                if (controlToDrag.RenderTransform is TranslateTransform tt)
                {
                    translateTransform = tt;
                }
                else if (controlToDrag.RenderTransform is TransformGroup tg)
                {
                    foreach (var item in tg.Children)
                    {
                        if (item is TranslateTransform)
                        {
                            translateTransform = item as TranslateTransform;
                            break;
                        }
                    }
                    translateTransform = new();
                    tg.Children.Add(translateTransform);
                }
                else
                {
                    translateTransform = new();
                    controlToDrag.RenderTransform = new TransformGroup()
                    {
                        Children = [
                            controlToDrag.RenderTransform,
                            translateTransform
                            ]
                    };
                }
                // drag
                translateTransform.X += e.HorizontalChange;
                translateTransform.Y += e.VerticalChange;
                return;
            }
        }
    }
}
