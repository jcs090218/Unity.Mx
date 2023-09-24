#if UNITY_EDITOR
using UnityEngine;
using System.Reflection;
using UnityEditor;
using System.Collections.Generic;
using System.IO;

namespace MetaX
{
    public class MxC : Mx
    {
        /* Variables */

        /* Setter & Getter */

        /* Functions */

        [Interactive("Animation.Record", "Log Mx version")]
        private static void Mx_Version()
        {
            Debug.Log("Mx " + VERSION);
        }

        [Interactive("", "Clear the console logs")]
        private static void ConsoleClear()
        {
            var assembly = Assembly.GetAssembly(typeof(UnityEditor.Editor));
            var type = assembly.GetType("UnityEditor.LogEntries");
            var method = type.GetMethod("Clear");
            method.Invoke(new object(), null);
        }

        [Interactive("d_UnityEditor.AnimationWindow", "Clear the history")]
        private static void ClearHistory()
        {
            MxWindow.ClearHistory();
        }

        [Interactive("", "Enter the play mode")]
        private static void EnterPlayMode() => EditorApplication.EnterPlaymode();

        [Interactive("", "Exit the play mode")]
        private static void ExitPlaymode() => EditorApplication.ExitPlaymode();

        [Interactive("", "Toggle the play mode")]
        private static void TogglePlaymode()
        {
            if (Application.isPlaying)
                ExitPlaymode();
            else
                EnterPlayMode();
        }

        [Interactive("", "Show data path in file browser")]
        private static void OpenDataPath()
        {
            CompletionRead("Try compleing read: ", new List<string>()
            {
                Application.dataPath,
                Application.persistentDataPath,
                Application.streamingAssetsPath,
                Application.temporaryCachePath,
            },
            (answer) =>
            {
                EditorUtility.RevealInFinder(answer);
            });
        }
    }
}
#endif
