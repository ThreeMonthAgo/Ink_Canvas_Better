using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Windows;
using Ink_Canvas_Better.Helpers;
using Ink_Canvas_Better.Utilities.DataStructures;

namespace Ink_Canvas_Better.Services
{
    public class MultiscreenService
    {
        SettingsService settingsService;

        public void CheckFloatingBars()
        {
            List<Screen> screens = Win32Helper.GetScreens();
            settingsService = App.GetService<SettingsService>();
            var floatingBars = settingsService.Settings.FloatingBarCollection;
            if (floatingBars.Count < screens.Count)
            {
                for (int i = floatingBars.Count; i < screens.Count; i++)
                {
                    floatingBars.Add(Settings.CreateDefaultFloatingBar(i));
                }
            }
            while (floatingBars.Count > screens.Count)
            {
                floatingBars.RemoveAt(floatingBars.Count - 1);
            }
        }

        public Screen GetScreen(int index)
        {
            return Win32Helper.GetScreens()[index];
        }
    }
}
