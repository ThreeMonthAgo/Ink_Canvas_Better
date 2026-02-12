using System.Collections;

namespace Ink_Canvas_Better.Helpers;

public static class Extension
{
    public static void RemoveLast<T>(this T t) where T : IList
    {
        if (t.Count > 0)
        {
            t.RemoveAt(t.Count - 1);
        }
    }
}
