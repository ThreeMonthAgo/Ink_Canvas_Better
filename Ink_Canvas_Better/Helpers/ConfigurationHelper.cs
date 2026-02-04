using System;
using System.Collections.Generic;
using System.IO;

namespace Ink_Canvas_Better.Helpers;

public static class ConfigurationHelper
{
    public static bool SaveConfiguration(string data, string filePath)
    {
        try
        {
            File.WriteAllText(filePath, data);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public static string LoadConfiguration(string filePath)
    {
        var data = File.ReadAllText(filePath);
        return data;
    }
}
