using System;
using Ink_Canvas_Better.Helpers;
using Ink_Canvas_Better.Utilities.DataStructures;
using Ink_Canvas_Better.ViewModels;

namespace Ink_Canvas_Better.Services
{
    /// <summary>
    /// Don't use it because I don't konw how to support multi-screen well yet.
    /// </summary>
    [Obsolete("Don't use it because I don't konw how to support multi-screen well yet.")]
    public class MultiscreenService
    {
        SettingsService settingsService;

        [Obsolete("Don't use it because I don't konw how to support multi-screen well yet.")]
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

        [Obsolete("Don't use it because I don't konw how to support multi-screen well yet.")]
        public Screen GetScreen(int index)
        {
            return Win32Helper.GetScreens()[index];
        }
    }
}
