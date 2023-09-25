#if UNITY_EDITOR
/**
 * Copyright (c) Jen-Chieh Shen. All rights reserved.
 * 
 * jcs090218@gmail.com
 */
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Mx
{
    public delegate void EmptyFunction();

    public static class MxEditorUtil
    {
        /* Variables */

        public const string KEY = "Mx";

        /* Setter & Getter */

        /* Functions */

        /// <summary>
        /// Construct a key to store `EditorPrefs` registry.
        /// </summary>
        public static string FormKey(string name)
        {
            // XXX: Registery is one instance per history!
            return Application.dataPath + "." + Mx.NAME + "." + name;
        }

        public static void BeginHorizontal(EmptyFunction func, bool flexibleSpace = false)
        {
            GUILayout.BeginHorizontal();
            if (flexibleSpace) GUILayout.FlexibleSpace();
            func.Invoke();
            GUILayout.EndHorizontal();
        }

        public static void BeginVertical(EmptyFunction func)
        {
            GUILayout.BeginVertical("box");
            func.Invoke();
            GUILayout.EndVertical();
        }

        public static void Indent(EmptyFunction func)
        {
            EditorGUI.indentLevel++;
            func.Invoke();
            EditorGUI.indentLevel--;
        }

        public static void LabelField(string text)
        {
            float width = EditorStyles.label.CalcSize(new GUIContent(text)).x;
            EditorGUILayout.LabelField(text, GUILayout.Width(width));
        }

        public static void ResetButton(EmptyFunction func)
        {
            if (!GUILayout.Button("Reset"))
                return;

            func.Invoke();
            MxSettings.data.SavePref();
        }

        #region EditorPrefs
        /// <summary>
        /// Extends `EditorPrefs` to set the list of strings.
        /// </summary>
        public static void SetList(string key, List<string> lst)
        {
            string result = "";

            foreach (string item in lst)
            {
                result += item + ":";
            }

            EditorPrefs.SetString(key, result);
        }

        /// <summary>
        /// Extends `EditorPrefs` to get the list of strings.
        /// </summary>
        public static List<string> GetList(string key)
        {
            string reg = EditorPrefs.GetString(key);

            return reg.Split(":").ToList();
        }
        #endregion

        /// <summary>
        /// Get a texture from its source filename.
        /// </summary>
        public static Texture FindTexture(string texName)
        {
            Texture tex = (texName == "") ? null : EditorGUIUtility.FindTexture(texName);
            return tex;
        }

        /// <summary>
        /// Print a list in one console log.
        /// </summary>
        public static void Print(List<string> lst)
        {
            string result = "";

            foreach (string item in lst)
            {
                result += item + " ";
            }

            Debug.Log(result);
        }
    }
}
#endif
