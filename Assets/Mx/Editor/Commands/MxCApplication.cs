#if UNITY_EDITOR
/**
 * Copyright (c) Jen-Chieh Shen. All rights reserved.
 * 
 * jcs090218@gmail.com
 */
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace Mx
{
    public class MxCApplication : Mx
    {
        /* Variables */

        /* Setter & Getter */

        /* Functions */

        public override bool Enable() { return true; }

        [Interactive(Summary: "Clear the console logs")]
        public static void ClearConsole()
        {
            var assembly = Assembly.GetAssembly(typeof(UnityEditor.Editor));
            var type = assembly.GetType("UnityEditor.LogEntries");
            var method = type.GetMethod("Clear");
            method.Invoke(new object(), null);
        }

        [Interactive(Summary: "Toggle the play mode")]
        public static void TogglePlayMode()
        {
            if (Application.isPlaying)
                EditorApplication.ExitPlaymode();
            else
                EditorApplication.EnterPlaymode();
        }

        [Interactive(Summary: "Toggle pausing in the editor application")]
        public static void TogglePause()
        {
            EditorApplication.isPaused = !EditorApplication.isPaused;
        }

        [Interactive(
            Icon: "d_FolderEmpty Icon",
            Summary: "Show data path in file browser")]
        public static void FindDataPath()
        {
            CompletingRead("Data path: ", new List<string>()
            {
                Application.dataPath,
                Application.persistentDataPath,
                Application.streamingAssetsPath,
                Application.temporaryCachePath,
            },
            (answer, summary) =>
            {
                EditorUtility.RevealInFinder(answer);
            });
        }
    }
}
#endif
