#if UNITY_EDITOR
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

        [Interactive(
            Summary: "Open Mx project's homepage")]
        private static void MxHomepage()
        {
            Application.OpenURL("https://github.com/jcs090218/Unity.Mx");
        }
    }
}
#endif
