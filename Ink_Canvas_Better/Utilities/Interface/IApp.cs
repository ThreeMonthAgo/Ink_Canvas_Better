using System.Diagnostics;
using System.Windows;
using Microsoft.Extensions.Hosting;

namespace Ink_Canvas_Better.Utilities.Interface;

internal interface IApp
{
    public static IHost Host;
    public static string[] StartupArgs;

    public static void ShutdownApp() => Application.Current.Shutdown();

    public static void RestartApp()
    {
        Process.Start(Environment.ProcessPath);
        ShutdownApp();
    }

    public static void ExitApp() => Environment.Exit(0);

    #region GetService

    public static object GetService(Type t)
    {
        var s = Host?.Services.GetService(t);
        if (s != null)
        {
            return s;
        }
        throw new ArgumentException($"Service {s} is null!");
    }

    public static object? TryGetService(Type t)
    {
        return Host?.Services.GetService(t);
    }

    public static T GetService<T>()
    {
        var s = Host?.Services.GetService(typeof(T));
        if (s != null)
        {
            return (T)s;
        }
        throw new ArgumentException($"Service {typeof(T)} is null!");
    }

    public static T? TryGetService<T>()
    {
        return (T?)Host?.Services.GetService(typeof(T));
    }

    #endregion
}
