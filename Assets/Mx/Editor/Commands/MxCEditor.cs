#if UNITY_EDITOR
/**
 * Copyright (c) Jen-Chieh Shen. All rights reserved.
 * 
 * jcs090218@gmail.com
 */
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

        public static void ListEditorPref()
        {

        }
    }
}
#endif