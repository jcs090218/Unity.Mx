#if UNITY_EDITOR
/**
 * Copyright (c) Jen-Chieh Shen. All rights reserved.
 * 
 * jcs090218@gmail.com
 */
using System.Collections.Generic;
using System.Reflection;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using System;
using Unity.VisualScripting;

namespace Mx
{
    public class MxC : Mx
    {
        /* Variables */

        /* Setter & Getter */

        /* Functions */

        public override bool Enable() { return true; }

        [Interactive(Summary: "Log Mx version")]
        private static void MxVersion()
        {
            Debug.Log("Mx " + VERSION);
        }

        [Interactive(
            Icon: "d_UnityEditor.AnimationWindow",
            Summary: "Clear the completion history")]
        private static void MxClearHistory()
        {
            MxCompletionWindow.ClearHistory();
        }

        [Interactive(
            Icon: "d_Settings",
            Summary: "Show Mx preferences")]
        private static void MxPreference()
        {
            SettingsService.OpenUserPreferences("Preferences/Mx");
        }

        //

        [Interactive(Summary: "Clear the console logs")]
        private static void ConsoleClear()
        {
            var assembly = Assembly.GetAssembly(typeof(UnityEditor.Editor));
            var type = assembly.GetType("UnityEditor.LogEntries");
            var method = type.GetMethod("Clear");
            method.Invoke(new object(), null);
        }

        [Interactive(Summary: "Enter the play mode")]
        private static void EnterPlayMode() => EditorApplication.EnterPlaymode();

        [Interactive(Summary: "Exit the play mode")]
        private static void ExitPlayMode() => EditorApplication.ExitPlaymode();

        [Interactive(Summary: "Toggle the play mode")]
        private static void TogglePlayMode()
        {
            if (Application.isPlaying)
                ExitPlayMode();
            else
                EnterPlayMode();
        }

        [Interactive(Summary: "Pause in the editor application")]
        private static void Pause()
        {
            EditorApplication.isPaused = true;
        }

        [Interactive(Summary: "Unpause in the editor application")]
        private static void Unpause()
        {
            EditorApplication.isPaused = false;
        }

        [Interactive(Summary: "Toggle pausing in the editor application")]
        private static void TogglePause()
        {
            EditorApplication.isPaused = !EditorApplication.isPaused;
        }

        [Interactive(
            Icon:"d_FolderEmpty Icon",
            Summary: "Show data path in file browser")]
        private static void FindDataPath()
        {
            CompletionRead("Data path: ", new List<string>()
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

        /// <summary>
        /// Return all scenes.
        /// </summary>
        /// <returns></returns>
        public static List<string> GetScenes()
        {
            string[] paths = Directory.GetFiles("Assets", "*.unity", SearchOption.AllDirectories);
            return paths.ToList();
        }

        [Interactive(
            Icon: "UnityLogo",
            Summary: "Open a scene")]
        private static void OpenScene()
        {
            CompletionRead("Scene name: ", GetScenes(),
            (answer) =>
            {
                EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
                EditorSceneManager.OpenScene(answer);
            });
        }
    }
}
#endif
