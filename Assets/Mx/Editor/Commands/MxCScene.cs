#if UNITY_EDITOR
/**
 * Copyright (c) Jen-Chieh Shen. All rights reserved.
 * 
 * jcs090218@gmail.com
 */
using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEditorInternal;
using UnityEngine;

namespace Mx
{
    public class MxCScene : Mx
    {
        /* Variables */

        /* Setter & Getter */

        /* Functions */

        public override bool Enable() { return true; }

        [Interactive(
            Icon: "UnityLogo",
            Summary: "Switch to scene")]
        public static void SwitchToScene()
        {
            CompletingRead("Switch to scene: ", MxEditorUtil.GetFiles("*.unity"),
                (answer, summary) =>
                {
                    if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
                        EditorSceneManager.OpenScene(answer);
                });
        }

        public static List<GameObject> FindObjectsByType<T>(FindObjectsSortMode sortMode = FindObjectsSortMode.None)
            where T : UnityEngine.Object
        {
            return UnityEngine.Object.FindObjectsByType<T>(sortMode)
                .Select(x => x.GetComponent<Transform>().gameObject)
                .ToList();
        }
        public static List<GameObject> FindObjectsByType(Type type, FindObjectsSortMode sortMode = FindObjectsSortMode.None)
        {
            return UnityEngine.Object.FindObjectsByType(type, sortMode)
                .Select(x => x.GetComponent<Transform>().gameObject)
                .ToList();
        }

        public static List<string> ConcatInstanceID(List<GameObject> objs)
        {
            var objss = MxUtil.ToListString(objs);

            for (int index = 0; index < objss.Count; ++index)
                objss[index] = "(" + objs[index].GetInstanceID() + ") " + objss[index];

            // XXX: Remove duplicate is probably not a good idea.
            objss = objss.Distinct().ToList();

            return objss;
        }

        /// <summary>
        /// Execution when hovering a GameObject in the scene.
        /// </summary>
        private static void OnFind(List<GameObject> objs, List<string> objss, string answer)
        {
            int index = objss.IndexOf(answer);

            var obj = objs[index];

            MxEditorUtil.FocusInSceneView(obj);
        }

        [Interactive(Summary: "Find GameObject in scene")]
        public static void FindGameObjectInScene()
        {
            var objs = FindObjectsByType<GameObject>();

            var objss = ConcatInstanceID(objs);

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

                    var objss = ConcatInstanceID(objs);

                    CompletingRead("Find GameObject with tag: (" + tag + ") ",
                        objss,
                        (answer, summary) => { OnFind(objs, objss, answer); },
                        (answer, summary) => { OnFind(objs, objss, answer); });
                });
        }

        [Interactive(Summary: "Find GameObject in sceen by type")]
        public static void FindGameObjectsByType()
        {
            var tuple = MxEditorUtil.CompletionComponents();

            var dic1 = tuple.Item1;
            var dic2 = tuple.Item2;

            CompletingRead("Enter component name: ", dic2, (name, _) =>
            {
                Type type = dic1[name];

                var objs = FindObjectsByType(type);

                var objss = ConcatInstanceID(objs);

                CompletingRead("Find " + type + " in scene: ", objss,
                    (answer, _) => { OnFind(objs, objss, answer); },
                    (answer, _) => { OnFind(objs, objss, answer); });
            });
        }
    }
}
#endif
