#if UNITY_EDITOR
/**
 * Copyright (c) Jen-Chieh Shen. All rights reserved.
 * 
 * jcs090218@gmail.com
 */
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEditorInternal;
using OpenCover.Framework.Model;

namespace Mx
{
    public class MxCScene : Mx
    {
        /* Variables */

        /* Setter & Getter */

        /* Functions */

        public override bool Enable() { return true; }

        /// <summary>
        /// Return all scenes.
        /// </summary>
        /// <returns></returns>
        public static List<string> GetScenes()
        {
            string[] paths = Directory.GetFiles("Assets", "*.unity", SearchOption.AllDirectories);
            return paths.ToList();
        }

        [Interactive(
            Icon: "UnityLogo",
            Summary: "Switch to scene")]
        private static void SwitchScene()
        {
            CompletingRead("Switch to scene: ", GetScenes(),
            (answer, summary) =>
            {
                EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
                EditorSceneManager.OpenScene(answer);
            });
        }

        /// <summary>
        /// Convert list object GameObject to its instance ID.
        /// </summary>
        private static List<int> GetInstanceIDs(List<GameObject> objs)
        {
            return objs.Select(i => i.GetInstanceID()).ToList();
        }

        [Interactive(
            Summary: "Find GameObject in scene")]
        private static void FindGameObjectInScene()
        {
            var objs = Object.FindObjectsByType<GameObject>(FindObjectsSortMode.None).ToList();
            var ids = GetInstanceIDs(objs);

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

        [Interactive(
            Summary: "Find GameObject in scene by tag")]
        private static void FindGameObjectWithTag()
        {
            CompletingRead("Enter tag name: ", InternalEditorUtility.tags.ToList(),
                (tag, _) =>
                {
                    var objs = GameObject.FindGameObjectsWithTag(tag).ToList();
                    var ids = GetInstanceIDs(objs);

                    var objss = MxUtil.ToListString(objs);
                    var idss = MxUtil.ToListString(ids);

                    MxCompletionWindow.INHIBIT_CLOSE = true;

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
