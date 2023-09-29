#if UNITY_EDITOR
/**
 * Copyright (c) Jen-Chieh Shen. All rights reserved.
 * 
 * jcs090218@gmail.com
 */
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditorInternal;
using UnityEditor.IMGUI.Controls;
using UnityEditor;
using UnityEngine.SceneManagement;

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

                    var objss = MxUtil.ToListString(objs);

                    for (int index = 0; index < objss.Count; ++index)
                        objss[index] = "(" + objs[index].GetInstanceID() + ") " + objss[index];

                    CompletingRead("Find GameObject with tag: (" + tag + ") ",
                        objss,
                        (answer, summary) => { OnFind(objs, objss, answer); },
                        (answer, summary) => { OnFind(objs, objss, answer); });
                });
        }

        [Interactive(Summary: "Collapse GmaeObjects in hierarchy view")]
        public static void CollapseGameObjects()
        {
            EditorWindow hierarchyWindow = typeof(EditorWindow).Assembly.GetType("UnityEditor.SceneHierarchyWindow").GetField("s_LastInteractedHierarchy", MxEditorUtil.STATIC_FLAGS).GetValue(null) as EditorWindow;
            if (hierarchyWindow)
            {
#if UNITY_2018_3_OR_NEWER
                object hierarchyTreeOwner = hierarchyWindow.GetType().GetField("m_SceneHierarchy", MxEditorUtil.INSTANCE_FLAGS).GetValue(hierarchyWindow);
#else
			    object hierarchyTreeOwner = hierarchyWindow;
#endif
                object hierarchyTree = hierarchyTreeOwner.GetType().GetField("m_TreeView", MxEditorUtil.INSTANCE_FLAGS).GetValue(hierarchyTreeOwner);
                if (hierarchyTree != null)
                {
                    List<int> expandedSceneIDs = new List<int>(4);
                    foreach (string expandedSceneName in (IEnumerable<string>)hierarchyTreeOwner.GetType().GetMethod("GetExpandedSceneNames", MxEditorUtil.INSTANCE_FLAGS).Invoke(hierarchyTreeOwner, null))
                    {
                        Scene scene = SceneManager.GetSceneByName(expandedSceneName);
                        if (scene.IsValid())
                            expandedSceneIDs.Add(scene.GetHashCode()); // GetHashCode returns m_Handle which in turn is used as the Scene's instanceID by SceneHierarchyWindow
                    }

                    MxEditorUtil.CollapseTreeViewController(hierarchyWindow, hierarchyTree, (TreeViewState)hierarchyTreeOwner.GetType().GetField("m_TreeViewState", MxEditorUtil.INSTANCE_FLAGS).GetValue(hierarchyTreeOwner), expandedSceneIDs);
                }
            }
        }
    }
}
#endif
