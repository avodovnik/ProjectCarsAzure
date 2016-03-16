using System;
using System.Runtime.Serialization;

namespace PCarsConsole
{

    public static class Extensions
    {
 
        public static T GetValue<T>(this SerializationInfo info, string fieldName)
        {
            return (T)info.GetValue(fieldName, typeof(T));
        }

        // Helper to avoid cross-threading updates (for forms version)

/*
        public static void CrossThread<T>(this T t, Action action) where T : Control
        {
            if (t.InvokeRequired)
                t.Invoke(action);
            else
                action();
        }

    */

    }
}
