#if UNITY_EDITOR
/**
 * Copyright (c) Jen-Chieh Shen. All rights reserved.
 * 
 * jcs090218@gmail.com
 */
using System.Collections.Generic;
using System;
using System.Linq;
using System.Reflection;
using UnityEditor;
using Mx;

public class TestCommands : Mx.Mx
{
    /* Variables */

    /* Setter & Getter */

    /* Functions */

    public override bool Enable() { return true; }

    [Interactive(Enabled: false)]
    public static void MxVersion() { }

    [Interactive]
    public static void ListComponents()
    {
        List<Type> lst = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(assembly => assembly.GetTypes())
            .Where(type => type.IsSubclassOf(typeof(UnityEngine.Object)))
            .ToList();

        var strs = MxUtil.ToListString<Type>(lst);

        CompletingRead("Components: ", strs, null);
    }

    [Interactive]
    public static void ToggleConsoleCollapse()
    {
        var assembly = Assembly.GetAssembly(typeof(Editor));
        var type = assembly.GetType("UnityEditor.LogEntries");
        var method = type.GetMethod("Collapse");
        method.Invoke(new object(), null);
    }
}
#endif
