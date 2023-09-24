#if UNITY_EDITOR
using System.Collections.Generic;
using System.Reflection;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace MetaX
{
    public class MxC : Mx
    {
        /* Variables */

        /* Setter & Getter */

        /* Functions */

        [Interactive("", "Log Mx version")]
        private static void MxVersion()
        {
            Debug.Log("Mx " + VERSION);
        }

        [Interactive(
            "d_UnityEditor.AnimationWindow", 
            "Clear the completion history")]
        private static void MxClearHistory()
        {
            MxWindow.ClearHistory();
        }

        [Interactive("", "Clear the console logs")]
        private static void ConsoleClear()
        {
            var assembly = Assembly.GetAssembly(typeof(UnityEditor.Editor));
            var type = assembly.GetType("UnityEditor.LogEntries");
            var method = type.GetMethod("Clear");
            method.Invoke(new object(), null);
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

        [Interactive(
            "d_FolderEmpty Icon", 
            "Show data path in file browser")]
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
            "UnityLogo", 
            "Open a scene")]
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
