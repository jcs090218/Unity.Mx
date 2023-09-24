#if UNITY_EDITOR
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace MetaX
{
    public static class MxUtil
    {
        /* Variables */

        public const string KEY = "Mx";

        /* Setter & Getter */

        /* Functions */

        public static string FormKey(string name)
        {
            // XXX: Registery is one instance per history!
            return Application.dataPath + "." + MxWindow.NAME + "." + name;
        }

        public static void SetList(string key, List<string> lst)
        {
            string result = "";

            foreach (string item in lst)
            {
                result += item + ":";
            }

            EditorPrefs.SetString(key, result);
        }

        public static List<string> GetList(string key)
        {
            string reg = EditorPrefs.GetString(key);

            return reg.Split(":").ToList();
        }
    }
}
#endif
