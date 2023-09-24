#if UNITY_EDITOR
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Mx
{
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
