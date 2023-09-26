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

namespace Mx
{
    public class MxCObject : Mx
    {
        /* Variables */

        /* Setter & Getter */

        /* Functions */

        [Interactive(Summary: "Find GameObject in scene")]
        public static void FindGameObjectInScene()
        {
            var objs = Object.FindObjectsByType<GameObject>(FindObjectsSortMode.None).ToList();
            var ids = MxUtil.GetInstanceIDs(objs);

            var objss = MxUtil.ToListString(objs);
            var idss = MxUtil.ToListString(ids);

            CompletingRead("Find GameObject in scene: ", objss, idss,
                (answer, summary) =>
                {
                    int index = idss.IndexOf(summary);

                    GameObject obj = objs[index];

                    Selection.activeGameObject = obj;
                    SceneView.FrameLastActiveSceneView();
                });
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

                            Selection.activeGameObject = obj;
                            SceneView.FrameLastActiveSceneView();
                        });
                });
        }
    }
}
#endif
