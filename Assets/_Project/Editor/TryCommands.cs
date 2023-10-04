/**
 * Copyright (c) Jen-Chieh Shen. All rights reserved.
 * 
 * jcs090218@gmail.com
 */
using System.Collections.Generic;
using UnityEngine;
using Mx;

public class TryCommands : Mx.Mx
{
    /* Variables */

    /* Setter & Getter */

    /* Functions */

    public override bool Enable() { return false; }

    [Interactive(summary: "Test command with same name", enabled: false)]
    public static void MxVersion() { }

    [Interactive(
        icon: "d_PreMatCube",
        summary: "Try completing read!")]
    public static void _TryCompletingRead()
    {
        CompletingRead("Try compleing read: ", new List<string>()
        {
            "Item 1",
            "Item 2",
            "Item 3",
        },
        (answer, summary) =>
        {
            Debug.Log("Chosen: " + answer);
        });
    }

    [Interactive(
        icon: "d_PreMatCube",
        summary: "Try read string!")]
    public static void _TryReadString()
    {
        ReadString("Try read string: ", (answer, summary) =>
        {
            Debug.Log("String: " + answer);
        });
    }

    [Interactive(
        icon: "d_PreMatCube",
        summary: "Try read number!")]
    public static void _TryReadNumber()
    {
        ReadNumber("Try read number: ", (answer, summary) =>
        {
            Debug.Log("Number: " + answer);
        });
    }

    [Interactive(
       icon: "d_PreMatCube",
       summary: "Try yes or no!")]
    public static void _TryYesOrNo()
    {
        YesOrNo("Try yes or no: ", (answer, summary) =>
        {
            Debug.Log("Answer: " + answer);
        });
    }

    [Interactive(
       icon: "d_PreMatCube",
       summary: "Try nested completing!")]
    public static void _TryNestedCompleting()
    {
        YesOrNo("Try yes or no: ", (answer, summary) =>
        {
            CompletingRead("Try compleing read: ", new List<string>()
            {
                "Item 1",
                "Item 2",
                "Item 3",
            },
            (answer, summary) =>
            {
                Debug.Log("Chosen: " + answer);
            });
        });
    }
}
