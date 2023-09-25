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
    public class MxCProject : Mx
    {
        /* Variables */

        /* Setter & Getter */

        /* Functions */

        public override bool Enable() { return true; }

        [Interactive(
            Icon: "d_FolderEmpty Icon",
            Summary: "Show project path in the file browser")]
        public static void OpenProjectPath()
        {
            EditorUtility.RevealInFinder(Application.dataPath);
        }

        [Interactive(
            Summary: "Show project settings")]
        public static void ProjectSettings()
        {
            EditorApplication.ExecuteMenuItem("Edit/Project Settings...");
        }
    }
}
#endif
