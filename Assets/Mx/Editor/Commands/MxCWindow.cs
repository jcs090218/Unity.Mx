/**
 * Copyright (c) Jen-Chieh Shen. All rights reserved.
 * 
 * jcs090218@gmail.com
 */
using System.Linq;
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

        [Interactive(summary: "Exit the Unity editor application")]
        public static void KillUnity()
        {
            EditorApplication.Exit(0);
        }

        [Interactive(summary: "Find and make focus on the targeted window")]
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
