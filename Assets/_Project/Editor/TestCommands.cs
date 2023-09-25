#if UNITY_EDITOR
/**
 * Copyright (c) Jen-Chieh Shen. All rights reserved.
 * 
 * jcs090218@gmail.com
 */
using System.Collections.Generic;
using System;
using System.Linq;
using Mx;
using UnityEditor;

public class TestCommands : Mx.Mx
{
    /* Variables */

    /* Setter & Getter */

    /* Functions */

    public override bool Enable() { return true; }

    [Interactive]
    private static void MxVersion() { }

    [Interactive]
    private static void ListComponents()
    {
        List<Type> lst = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(assembly => assembly.GetTypes())
            .Where(type => type.IsSubclassOf(typeof(UnityEngine.Object)))
            .ToList();

        var strs = MxUtil.ToListString<Type>(lst);

        CompletingRead("Components: ", strs, null);
    }

    private static void ToggleConsoleCollapse()
    {
        
    }
}
#endif
