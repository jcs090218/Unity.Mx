#if UNITY_EDITOR
/**
 * Copyright (c) Jen-Chieh Shen. All rights reserved.
 * 
 * jcs090218@gmail.com
 */
using UnityEditor;

namespace Mx
{
    public class MxCWindow : Mx
    {
        /* Variables */

        /* Setter & Getter */

        /* Functions */

        public override bool Enable() { return true; }

        [Interactive(
            Summary: "Open/Focus the console window")]
        private static void ToConsole()
        {
            EditorApplication.ExecuteMenuItem("Window/General/Console");
        }

        [Interactive(
            Summary: "Open/Focus the inspector window")]
        private static void ToInspector()
        {
            EditorApplication.ExecuteMenuItem("Window/General/Inspector");
        }
    }
}
#endif
