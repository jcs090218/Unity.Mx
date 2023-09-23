#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace MetaX
{
    public class MxWindow : EditorWindow
    {
        /* Variables */
        
        public static MxWindow instance = null;

        private List<Mx> mTypes = null;

        private List<MethodInfo> mMethods = null;

        /* Setter & Getter */

        /* Functions */

        [MenuItem("Tools/MetaX/Window &x", false, -1000)]
        public static void ShowWindow() { GetWindow<MxWindow>("MetaX"); }

        private void OnEnable()
        {
            instance = this;

            mTypes = GetMetaX();

            Debug.Log(mTypes.Count);

            mMethods = GetMethods();

            foreach (MethodInfo method in mMethods)
            {
                Debug.Log(method);
            }
        }

        private void OnGUI()
        {

        }

        private List<Mx> GetMetaX()
        {
            return AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(assembly => assembly.GetTypes())
                .Where(type => type.IsSubclassOf(typeof(Mx)))
                .Select(type => Activator.CreateInstance(type) as Mx)
                .ToList();
        }

        private List<MethodInfo> GetMethods()
        {
            var bindings = BindingFlags.Instance 
                | BindingFlags.Static 
                | BindingFlags.Public 
                | BindingFlags.NonPublic;

            return mTypes.SelectMany(t => t.GetType().GetMethods(bindings))
                .Where(m => m.GetCustomAttributes(typeof(InteractiveAttribute), false).Length > 0)
                .ToList();
        }
    }
}
#endif
