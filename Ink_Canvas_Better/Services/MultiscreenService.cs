using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Windows;
using Ink_Canvas_Better.Helpers;
using Ink_Canvas_Better.Utilities.DataStructures;
using Ink_Canvas_Better.Windows;

namespace Ink_Canvas_Better.Services
{
    public class MultiscreenService
    {
        SettingsService settingsService;

        // TODO
        public void CreateMainWindows()
        {
            List<Screen> screens = Win32Helper.GetScreens();
            settingsService = App.GetService<SettingsService>();

            if (screens.Count == 0)
            {
                var window = new MainWindow(settingsService);
                window.Show();
                return;
            }

            foreach (var screen in screens)
            {
                var window = new MainWindow(settingsService)
                {
                    WindowStartupLocation = WindowStartupLocation.Manual,
                    Left = screen.X,
                    Top = screen.Y,
                    Width = screen.Width,
                    Height = screen.Height,
                    WindowStyle = WindowStyle.None,
                    ResizeMode = ResizeMode.NoResize
                };
                window.Show();
            }

        }
    }
}
