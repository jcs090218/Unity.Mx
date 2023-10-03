/**
 * Copyright (c) Jen-Chieh Shen. All rights reserved.
 * 
 * jcs090218@gmail.com
 */
using UnityEditor;
using UnityEngine;

namespace Mx
{
    public class MxC : Mx
    {
        /* Variables */

        /* Setter & Getter */

        /* Functions */

        public override bool Enable() { return true; }

        [Interactive(Summary: "Log Mx version")]
        public static void MxVersion()
        {
            Debug.Log("Mx " + VERSION);
        }

        [Interactive(Summary: "Open Mx project's homepage")]
        public static void MxHome()
        {
            Application.OpenURL("https://github.com/jcs090218/Unity.Mx");
        }

        [Interactive(
            Icon: "d_UnityEditor.AnimationWindow",
            Summary: "Clear the completion history")]
        public static void MxClearHistory()
        {
            MxCompletionWindow.ClearHistory();
        }

        [Interactive(
            Icon: "ClothInspector.SettingsTool",
            Summary: "Show Mx preferences")]
        public static void MxPreference()
        {
            SettingsService.OpenUserPreferences("Preferences/Mx");
        }
    }
}
