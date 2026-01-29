using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using Ink_Canvas_Better.Utilities.Interface;
using Ink_Canvas_Better.View.Windows;
using Ink_Canvas_Better.ViewModel.Controls.FloatingBar.FloatingBarControl;

namespace Ink_Canvas_Better.View.Controls.FloatingBar.FloatingBarControl
{
    public partial class RetraceControl : UserControl
    {
        public RetraceControlVM Settings => DataContext as RetraceControlVM;

        public RetraceControl()
        {
            InitializeComponent();
        }

        private void RetraceControl_Click(object sender, RoutedEventArgs e) => Settings.IsOpen = true;

        private void Button_Undo_Click(object sender, RoutedEventArgs e) => IApp.GetService<MainWindow>().UndoStrokes();

        private void Button_Redo_Click(object sender, RoutedEventArgs e) => IApp.GetService<MainWindow>().RedoStroks();
    }
}
