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

        [Interactive(
            icon: "d_console.erroricon.inactive.sml",
            summary: "Clear console logs")]
        public static void ClearConsole()
        {
            var assembly = Assembly.GetAssembly(typeof(Editor));
            var type = assembly.GetType("UnityEditor.LogEntries");
            var method = type.GetMethod("Clear");
            method.Invoke(new object(), null);
        }

        [Interactive(summary: "Toggle the play mode on/off")]
        public static void TogglePlay()
        {
            if (Application.isPlaying)
                EditorApplication.ExitPlaymode();
            else
                EditorApplication.EnterPlaymode();
        }

        [Interactive(summary: "Toggle pausing on/off in the editor application")]
        public static void TogglePause()
        {
            EditorApplication.isPaused = !EditorApplication.isPaused;
        }

        [Interactive(summary: "Perform a single frame step")]
        public static void StepFrame()
        {
            EditorApplication.Step();
        }

        [Interactive(
            icon: "d_FolderEmpty Icon",
            summary: "Show data path in file browser")]
        public static void FindDataPath()
        {
            CompletingRead("Data path: ", new Dictionary<string, string>()
            {
                { Application.dataPath, "Application.dataPath" },
                { Application.persistentDataPath, "Application.persistentDataPath" },
                { Application.streamingAssetsPath, "Application.streamingAssetsPath" },
                { Application.temporaryCachePath, "Application.temporaryCachePath" },
            },
            (answer, summary) =>
            { EditorUtility.RevealInFinder(answer); });
        }
    }
}
