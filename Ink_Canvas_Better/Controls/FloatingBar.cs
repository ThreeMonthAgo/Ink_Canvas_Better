using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Ink_Canvas_Better.Controls.FloatingBarControls;
using Ink_Canvas_Better.Controls.Panel;
using Ink_Canvas_Better.Services;

namespace Ink_Canvas_Better.Controls
{
    class FloatingBar : ItemsControl
    {
        public ObservableCollection<FloatingBarGroup> Groups = [];

        // Temp
        public FloatingBar()
        {
            this.Loaded += FloatingBar_Loaded;
        }

        private void FloatingBar_Loaded(object sender, RoutedEventArgs e)
        {
            this.RenderTransform = new TranslateTransform();
            FloatingBarGroup f = new();
            f.ControlsCollection.Add(IAppHost.GetService<ControlsService>().CreateControl(MultifuntionControl.ControlGuid));
            Groups.Add(f);

            FloatingBarGroup f1 = new();
            f1.ControlsCollection.Add(IAppHost.GetService<ControlsService>().CreateControl(CursorControl.ControlGuid));
            Groups.Add(f1);
            this.ItemsSource = Groups;
            this.ItemsPanel = new ItemsPanelTemplate(new FrameworkElementFactory(typeof(StackPanel)));
        }
    }
}
