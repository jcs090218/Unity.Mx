#if UNITY_EDITOR
/**
 * Copyright (c) Jen-Chieh Shen. All rights reserved.
 * 
 * jcs090218@gmail.com
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;

namespace Mx
{
    public class MxCEditor : Mx
    {
        private static readonly List<string> mSkipRoots = new List<string> { "CONTEXT", "&File", "&Help", "Help", "Component", "&Window" };

        private static string CreateCommandString(string _command, List<string> _list)
        {
            StringBuilder stringBuilder = new();

            foreach (string s in _list)
            {
                stringBuilder.Append(s.Trim(' ', '&'));
                stringBuilder.Append("/");
            }

            stringBuilder.Append(_command.Trim(' ', '&'));

            return stringBuilder.ToString();
        }

        private static List<string> MenuItemLists()
        {
            List<string> commands = new();

            string sCommands = EditorGUIUtility.SerializeMainMenuToString();
            string[] arCommands = sCommands.Split(new string[] { "\n" }, System.StringSplitOptions.RemoveEmptyEntries);

            List<string> lCurrentParents = new List<string>();
            for (int i = 0, imax = arCommands.Length; i < imax; i++)
            {
                string sCurrentRoot = lCurrentParents.Count != 0 ? lCurrentParents.First() : "";

                string sCurrentLine = arCommands[i];
                string sCurrentLineTrimmed = sCurrentLine.TrimStart(' ');
                int iPreviousIndention = lCurrentParents.Count - 1;
                int iCurrentIndention = (sCurrentLine.Length - sCurrentLineTrimmed.Length) / 4;

                int iLastTab = sCurrentLine.LastIndexOf('\t');
                sCurrentLineTrimmed = iLastTab != -1 ? sCurrentLine.Remove(iLastTab, sCurrentLine.Length - iLastTab).Trim() : sCurrentLine.Trim();

                if (sCurrentLineTrimmed == System.String.Empty)
                    continue;

                if (iPreviousIndention > iCurrentIndention)
                {
                    MxUtil.RemoveLast(lCurrentParents);
                    while (iPreviousIndention > iCurrentIndention)
                    {
                        MxUtil.RemoveLast(lCurrentParents);
                        iPreviousIndention--;
                    }

                    if (!mSkipRoots.Contains(sCurrentRoot))
                    {
                        commands.Add(CreateCommandString(sCurrentLineTrimmed, lCurrentParents));
                    }
                    lCurrentParents.Add(sCurrentLineTrimmed);
                }
                else if (iPreviousIndention < iCurrentIndention)
                {
                    if (!mSkipRoots.Contains(sCurrentRoot))
                    {
                        MxUtil.RemoveLast(commands);
                        commands.Add(CreateCommandString(sCurrentLineTrimmed, lCurrentParents));
                    }
                    lCurrentParents.Add(sCurrentLineTrimmed);
                }
                else
                {
                    MxUtil.RemoveLast(lCurrentParents);
                    if (!mSkipRoots.Contains(sCurrentRoot))
                    {
                        commands.Add(CreateCommandString(sCurrentLineTrimmed, lCurrentParents));
                    }
                    lCurrentParents.Add(sCurrentLineTrimmed);
                }
            }

            return commands;
        }

        [Interactive(Summary: "Invokes the menu item in the specified path")]
        public static void ExecutMenuItem()
        {
            CompletingRead("Menu Item: ", MenuItemLists(), (command, _) =>
            {
                EditorApplication.ExecuteMenuItem(command);
            });
        }

        [Interactive(Summary: "List the EditorPref key & value; then copy the key to clipboard")]
        public static void EditorPrefGetKey()
        {
            const PrefType type = PrefType.Editor;

            Dictionary<string, string> prefss = Prefs.GetPrefsString(type);

            CompletingRead("Get EditorPref key: ", prefss, (key, _) =>
            { 
                key.CopyToClipboard();
                UnityEngine.Debug.Log("Copied EditorPref key: " + key);
            });
        }

        [Interactive(Summary: "List the PlayerPref key & value; then copy the key to clipboard")]
        public static void PlayerPrefGetKey()
        {
            const PrefType type = PrefType.Player;

            Dictionary<string, string> prefss = Prefs.GetPrefsString(type);

            CompletingRead("Get PlayerPref key: ", prefss, (key, _) =>
            { 
                key.CopyToClipboard();
                UnityEngine.Debug.Log("Copied PlayerPref key: " + key);
            });
        }

        private static string GetPrefPrefix(PrefType type)
        {
            return (type == PrefType.Editor) ? "[EditorPrefs] " : "[PlayerPrefs] ";
        }

        private static void CreatePref(PrefType type, string key)
        {
            CompletingRead("Select type: ", new Dictionary<string, string>()
            {
                { "Bool"  , "True or False" },
                { "Float" , "Any decimal number (10.0f, 30.0f)" },
                { "Int"   , "Any integer number (10, 30)" },
                { "String", "" },
            },
            (createType, _) =>
            {
                ReadString(GetPrefPrefix(type) + "Create `" + key + "` with value", (input, _) =>
                {
                    switch (createType)
                    {
                        case "Bool":
                            Prefs.SetBool(type, key, bool.Parse(input));
                            break;
                        case "Float":
                            Prefs.SetFloat(type, key, float.Parse(input));
                            break;
                        case "Int":
                            Prefs.SetInt(type, key, int.Parse(input));
                            break;
                        case "String":
                            Prefs.SetString(type, key, input);
                            break;
                    }
                });
            });
        }

        [Interactive(Summary: "Create or update EditorPrefs")]
        public static void EditorPrefSetKey()
        {
            const PrefType type = PrefType.Editor;

            Dictionary<string, string> prefss = Prefs.GetPrefsString(type);
            Dictionary<string, Type> prefst = Prefs.GetPrefsType(type);

            CompletingRead(GetPrefPrefix(type) + "Set value: ", prefss, (key, _) =>
            {
                if (prefst.ContainsKey(key))
                {
                    ReadString(GetPrefPrefix(type) + "Update `" + key + "`'s value to ", (input, _) =>
                    { Prefs.Set(type, prefst, key, input); });

                    return;
                }

                CreatePref(type, key);
            }, 
            requiredMatch: false);
        }

        [Interactive(Summary: "Create or update PlayerPrefs")]
        public static void PlayerPrefSetKey()
        {
            const PrefType type = PrefType.Player;

            Dictionary<string, string> prefss = Prefs.GetPrefsString(type);
            Dictionary<string, Type> prefst = Prefs.GetPrefsType(type);

            CompletingRead(GetPrefPrefix(type) + "Set value: ", prefss, (key, _) =>
            {
                if (prefst.ContainsKey(key))
                {
                    ReadString(GetPrefPrefix(type) + "Update `" + key + "`'s value to ", (input, _) =>
                    { Prefs.Set(type, prefst, key, input); });

                    return;
                }

                CreatePref(type, key);
            },
            requiredMatch: false);
        }

        [Interactive(Summary: "Delete key in EditorPrefs")]
        public static void EditorPrefDeleteKey()
        {
            const PrefType type = PrefType.Editor;

            Dictionary<string, string> prefss = Prefs.GetPrefsString(type);

            CompletingRead(GetPrefPrefix(type) + "Delet key: ", prefss, (key, _) =>
            { Prefs.DeleteKey(type, key); });
        }

        [Interactive(Summary: "Delete key in PlayerPrefs")]
        public static void PlayerPrefDeleteKey()
        {
            const PrefType type = PrefType.Player;

            Dictionary<string, string> prefss = Prefs.GetPrefsString(type);

            CompletingRead(GetPrefPrefix(type) + "Delet key: ", prefss, (key, _) =>
            { Prefs.DeleteKey(type, key); });
        }

        [Interactive(Summary: "Delete all keys in EditorPrefs")]
        public static void EditorPrefDeleteAll()
        {
            const PrefType type = PrefType.Editor;

            YesOrNo(GetPrefPrefix(type) + "Delet all keys: ", (yes, _) =>
            {
                if (yes == "Yes") Prefs.DeleteAll(type);
            });
        }

        [Interactive(Summary: "Delete all keys in PlayerPrefs")]
        public static void PlayerPrefDeleteAll()
        {
            const PrefType type = PrefType.Player;

            YesOrNo(GetPrefPrefix(type) + "Delet all keys: ", (yes, _) =>
            {
                if (yes == "Yes") Prefs.DeleteAll(type);
            });
        }
    }
}
#endif