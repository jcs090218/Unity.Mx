#if UNITY_EDITOR
/**
 * Copyright (c) Jen-Chieh Shen. All rights reserved.
 * 
 * jcs090218@gmail.com
 */
using System.Linq;
using OpenCover.Framework.Model;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

namespace Mx
{
    public class MxCWindow : Mx
    {
        /* Variables */

        /* Setter & Getter */

        /* Functions */

        public override bool Enable() { return true; }

        [Interactive(Summary: "Open/Focus the console window")]
        public static void SwitchToConsole()
        {
            EditorApplication.ExecuteMenuItem("Window/General/Console");
        }

        [Interactive(Summary: "Open/Focus the inspector window")]
        public static void SwitchToInspector()
        {
            EditorApplication.ExecuteMenuItem("Window/General/Inspector");
        }

        [Interactive(Summary: "Open/Focus the hierarchy window")]
        public static void SwitchToHierarchy()
        {
            EditorApplication.ExecuteMenuItem("Window/General/Hierarchy");
        }

        [Interactive(Summary: "Open/Focus the scene view window")]
        public static void SwitchToSceneView()
        {
            EditorApplication.ExecuteMenuItem("Window/General/Scene");
        }

        [Interactive(Summary: "Open/Focus the game view window")]
        public static void SwitchToGameView()
        {
            EditorApplication.ExecuteMenuItem("Window/General/Game");
        }

        [Interactive(
            Summary: "Find and make focus on the targeted window")]
        public static void SwitchToWindow()
        {
            var windows = Resources.FindObjectsOfTypeAll<EditorWindow>().ToList();

            var windowss = MxUtil.ToListString(windows);

            CompletingRead("Switch to window: ", windowss,
                (answer, _) =>
                {
                    int index = windowss.IndexOf(answer);

                    EditorWindow window = windows[index];
                    window.Focus();
                });
        }
    }
}
#endif
