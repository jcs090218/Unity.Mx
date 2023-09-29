#if UNITY_EDITOR
/**
 * Copyright (c) Jen-Chieh Shen. All rights reserved.
 * 
 * jcs090218@gmail.com
 */
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace Mx
{
    public class MxCFile : Mx
    {
        /* Variables */

        /* Setter & Getter */

        /* Functions */

        public override bool Enable() { return true; }

        /// <summary>
        /// Return a list of file path excludes .meta files.
        /// </summary>
        private static List<string> DefaultFiles(string pat = "*.*")
        {
            return MxUtil.GetFiles(pat)
                .Where(name => !name.EndsWith(".meta"))
                .ToList();
        }

        [Interactive(Summary: "Find file")]
        public static void FindFile()
        {
            CompletingRead("Find file: ", DefaultFiles(),
                (path, _) =>
                { InternalEditorUtility.OpenFileAtLineExternal(path, 1); },
                (path, _) =>
                { Selection.activeObject = AssetDatabase.LoadMainAssetAtPath(path); });
        }

        [Interactive(Summary: "Find the file and open it externally")]
        public static void FindFileExternal()
        {
            CompletingRead("Find file externally: ", DefaultFiles(),
                (path, _) => { Application.OpenURL(path); },
                (path, _) =>
                { MxEditorUtil.HighlightAsset(path); });
        }

        [Interactive(Summary: "Find file by type of the file")]
        public static void FindFileByType()
        {
            CompletingRead("Find file by type: ", new Dictionary<string, string>()
            {
                { "Scene"    , "*.unity" },
                { "Prefab"   , "*.prefab" },
                { "Texture"  , "*.png" },
                { "Material" , "*.mat" },
                { "Mesh"     , "*.mesh" },
                { "C# script", "*.cs" },
                { "Text"     , "*.txt" },
            },
            (type, pattern) =>
            {
                CompletingRead("Find file by type: (" + type + ") ",
                    DefaultFiles(pattern),
                    (path, _) =>
                    { InternalEditorUtility.OpenFileAtLineExternal(path, 1); }, 
                    (path, _) => 
                    { MxEditorUtil.HighlightAsset(path); });
            });
        }

        [Interactive(Summary: "Find file by type of the file")]
        public static void FindFileByWildcard()
        {
            ReadString("Wildcard pattern: ", (pattern, _) =>
            {
                CompletingRead("Find file by wildcard: (" + pattern + ") ",
                   DefaultFiles(pattern),
                   (path, _) =>
                   { InternalEditorUtility.OpenFileAtLineExternal(path, 1); },
                   (path, _) =>
                   { MxEditorUtil.HighlightAsset(path); });
            });
        }
    }
}
#endif
