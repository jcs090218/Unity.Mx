#if UNITY_EDITOR
using UnityEngine;
using System.Reflection;

namespace MetaX
{
    public class MxCommands : Mx
    {
        /* Variables */

        /* Setter & Getter */

        /* Functions */

        /// <summary>
        /// Log Mx version.
        /// </summary>
        [Interactive]
        private static void Mx_Version()
        {
            Debug.Log("Mx " + VERSION);
        }

        /// <summary>
        /// Clear the console logs.
        /// </summary>
        [Interactive]
        private static void ConsoleClear()
        {
            var assembly = Assembly.GetAssembly(typeof(UnityEditor.Editor));
            var type = assembly.GetType("UnityEditor.LogEntries");
            var method = type.GetMethod("Clear");
            method.Invoke(new object(), null);
        }

        /// <summary>
        /// Clear the history.
        /// </summary>
        [Interactive]
        private static void ClearHistory()
        {
            MxWindow.ClearHistory();
        }
    }
}
#endif
