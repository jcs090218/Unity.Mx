/**
 * Copyright (c) Jen-Chieh Shen. All rights reserved.
 * 
 * jcs090218@gmail.com
 */
using System.Collections.Generic;
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
        
        [Interactive(summary: "Find file")]
        public static void FindFile()
        {
            CompletingRead("Find file: ", MxEditorUtil.DefaultFiles(),
                (path, _) =>
                { InternalEditorUtility.OpenFileAtLineExternal(path, 1); },
                (path, _) =>
                { MxEditorUtil.HighlightAsset(path); });
        }

        [Interactive(summary: "Find the file and open it externally")]
        public static void FindFileExternal()
        {
            CompletingRead("Find file externally: ", MxEditorUtil.DefaultFiles(),
                (path, _) => { Application.OpenURL(path); },
                (path, _) =>
                { MxEditorUtil.HighlightAsset(path); });
        }

        [Interactive(summary: "Find file by type of the file")]
        public static void FindFileByType()
        {
            CompletingRead("Find file by type: ", new Dictionary<string, string>()
            {
                { "All"      , "*.*" },
                { "Scene"    , "*.unity" },
                { "Prefab"   , "*.prefab" },
                { "Texture"  , "*.png" },
                { "Material" , "*.mat" },
                { "Mesh"     , "*.mesh" },
                { "C# script", "*.cs" },
                { "Text"     , "*.txt" },
                { "Font"     , "*.ttf" },
            },
            (type, item) =>
            {
                string pattern = item.summary;
                string[] patterns = pattern.Split('|');

                CompletingRead("Find file by type: (" + type + ") ",
                    MxEditorUtil.DefaultFiles(pattern),
                    (path, _) =>
                    { InternalEditorUtility.OpenFileAtLineExternal(path, 1); }, 
                    (path, _) => 
                    { MxEditorUtil.HighlightAsset(path); });
            });
        }

        [Interactive(summary: "Find file by wildcard pattern")]
        public static void FindFileByWildcard()
        {
            ReadString("Wildcard pattern: ", (pattern, _) =>
            {
                CompletingRead("Find file by wildcard: (" + pattern + ") ",
                   MxEditorUtil.DefaultFiles(pattern),
                   (path, _) =>
                   { InternalEditorUtility.OpenFileAtLineExternal(path, 1); },
                   (path, _) =>
                   { MxEditorUtil.HighlightAsset(path); });
            });
        }
    }
}
