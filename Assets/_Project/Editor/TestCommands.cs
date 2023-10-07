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
using UnityEngine;
using UnityEditor.TestTools.TestRunner.Api;
using NUnit.Framework;

public class TestCommands : Mx.Mx
{
    /* Variables */

    /* Setter & Getter */

    /* Functions */

    public override bool Enable() { return true; }

    [Interactive(enabled: false)]
    public static void MxVersion() { }

    [Interactive(enabled: false)]
    public static void ListComponents()
    {
        CompletingRead("Components: ", MxEditorUtil.CompletionComponents().Item2, (name, _) =>
        {
            MxEditorUtil.CopyToClipboard(name);
        });
    }

    [Interactive(enabled: false)]
    public static void ToggleConsoleCollapse()
    {
        var assembly = Assembly.GetAssembly(typeof(Editor));
        var type = assembly.GetType("UnityEditor.ConsoleWindow");
        var method = type.GetMethod("Collapse");
        method.Invoke(null, null);
    }

    [Interactive(summary: "Just a test command")]
    public static void _MyTest()
    {
        var files = MxEditorUtil.DefaultFiles();

        UnityEngine.Object asset = AssetDatabase.LoadAssetAtPath(files[0], null);

        Debug.Log(asset);
        //Debug.Log(MxUtil.FindTexture(asset));
    }
}
