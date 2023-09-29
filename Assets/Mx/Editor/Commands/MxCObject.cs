#if UNITY_EDITOR
/**
 * Copyright (c) Jen-Chieh Shen. All rights reserved.
 * 
 * jcs090218@gmail.com
 */
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEditorInternal;
using NUnit.Framework;
using System.Collections.Generic;

namespace Mx
{
    public class MxCObject : Mx
    {
        /* Variables */

        /* Setter & Getter */

        /* Functions */

        private static void OnFind(List<GameObject> objs, List<string> objss, string answer)
        {
            int index = objss.IndexOf(answer);

            GameObject obj = objs[index];

            MxEditorUtil.FocusInSceneView(obj);
        }

        [Interactive(Summary: "Find GameObject in scene")]
        public static void FindGameObjectInScene()
        {
            var objs = Object.FindObjectsByType<GameObject>(FindObjectsSortMode.None).ToList();

            var objss = MxUtil.ToListString(objs);

            for (int index = 0; index < objss.Count; ++index)
                objss[index] = "(" + objs[index].GetInstanceID() + ") " + objss[index];

            CompletingRead("Find GameObject in scene: ", objss,
                (answer, _) => { OnFind(objs, objss, answer); },
                (answer, _) => { OnFind(objs, objss, answer); });
        }

        [Interactive(Summary: "Find GameObject in scene by tag")]
        public static void FindGameObjectWithTag()
        {
            CompletingRead("Enter tag name: ", InternalEditorUtility.tags.ToList(),
                (tag, _) =>
                {
                    var objs = GameObject.FindGameObjectsWithTag(tag).ToList();
                    var ids = MxUtil.GetInstanceIDs(objs);

                    var objss = MxUtil.ToListString(objs);
                    var idss = MxUtil.ToListString(ids);

                    CompletingRead("Find GameObject with tag: (" + tag + ") ",
                        objss, idss,
                        (answer, summary) =>
                        {
                            int index = idss.IndexOf(summary);

                            GameObject obj = objs[index];

                            MxEditorUtil.FocusInSceneView(obj);
                        });
                });
        }
    }
}
#endif
