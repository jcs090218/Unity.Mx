#if UNITY_EDITOR
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

    public override bool Enable() { return true; }

    [Interactive]
    public static void MxVersion() { }

    [Interactive(
        Icon: "d_PreMatCube",
        Summary: "Try completing read!")]
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
        Icon: "d_PreMatCube",
        Summary: "Try read string!")]
    public static void _TryReadString()
    {
        ReadString("Try read string: ", (answer, summary) =>
        {
            Debug.Log("String: " + answer);
        });
    }

    [Interactive(
        Icon: "d_PreMatCube",
        Summary: "Try read number!")]
    public static void _TryReadNumber()
    {
        ReadNumber("Try read number: ", (answer, summary) =>
        {
            Debug.Log("Number: " + answer);
        });
    }

    [Interactive(
       Icon: "d_PreMatCube",
       Summary: "Try yes or no!")]
    public static void _TryYesOrNo()
    {
        YesOrNo("Try yes or no: ", (answer, summary) =>
        {
            Debug.Log("Answer: " + answer);
        });
    }

    [Interactive(
       Icon: "d_PreMatCube",
       Summary: "Try nested completing!")]
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
#endif
