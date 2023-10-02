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

        public static (Dictionary<string, GameObject>, Dictionary<string, MxItem>)
            CompletionGameObjects(List<GameObject> objs)
        {
            Dictionary<string, GameObject> dic1 = new();
            Dictionary<string, MxItem> dic2 = new();

            for (int index = 0; index < objs.Count; ++index)
            {
                GameObject obj = objs[index];
                string name = "(" + objs[index].GetInstanceID() + ") " + obj.name;

                if (dic1.ContainsKey(name))
                    continue;

                dic1.Add(name, obj);
                dic2.Add(name, new MxItem(Icon: MxUtil.FindTexture(obj)));
            }

            return (dic1, dic2);
        }

        /// <summary>
        /// Execution when hovering a GameObject in the scene.
        /// </summary>
        private static void OnFind(Dictionary<string, GameObject> objs, string name)
        {
            GameObject obj = objs[name];
            MxEditorUtil.FocusInSceneView(obj);
        }

        [Interactive(Summary: "Find GameObject in scene")]
        public static void FindGameObjectInScene()
        {
            var objs = FindObjectsByType<GameObject>();

            var tuple2 = CompletionGameObjects(objs);

            var dic21 = tuple2.Item1;
            var dic22 = tuple2.Item2;

            CompletingRead("Find GameObject in scene: ",
                dic22,
                (name, _) => { OnFind(dic21, name); },
                (name, _) => { OnFind(dic21, name); });
        }

        [Interactive(Summary: "Find GameObject in scene by tag")]
        public static void FindGameObjectWithTag()
        {
            CompletingRead("Enter tag name: ", InternalEditorUtility.tags.ToList(),
                (tag, _) =>
                {
                    var objs = GameObject.FindGameObjectsWithTag(tag).ToList();

                    var tuple2 = CompletionGameObjects(objs);

                    var dic21 = tuple2.Item1;
                    var dic22 = tuple2.Item2;

                    CompletingRead("Find GameObject with tag: (" + tag + ") ",
                        dic22,
                        (name, _) => { OnFind(dic21, name); },
                        (name, _) => { OnFind(dic21, name); });
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

                var tuple2 = CompletionGameObjects(objs);

                var dic21 = tuple2.Item1;
                var dic22 = tuple2.Item2;

                CompletingRead("Find " + type + " in scene: ", dic22,
                    (name, _) => { OnFind(dic21, name); },
                    (name, _) => { OnFind(dic21, name); });
            });
        }
    }
}
#endif
