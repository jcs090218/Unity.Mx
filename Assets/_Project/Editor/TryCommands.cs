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

    [Interactive(
        Icon: "Animation.Record",
        Summary: "Log Mx version.")]
    private static void Mx_Version()
    {
        Debug.Log("Mx " + VERSION);
    }

    [Interactive(
        Icon: "d_PreMatCube",
        Summary: "Try completing read!")]
    private static void _TryCompletingRead()
    {
        CompletionRead("Try compleing read: ", new List<string>()
        {
            "Item 1",
            "Item 2",
            "Item 3",
        },
        (answer) =>
        {
            Debug.Log("Chosen: " + answer);
        });
    }

    [Interactive(
        Icon: "d_PreMatCube",
        Summary: "Try read string!")]
    private static void _TryReadString()
    {
        ReadString("Try read string: ", (answer) =>
        {
            Debug.Log("String: " + answer);
        });
    }

    [Interactive(
        Icon: "d_PreMatCube",
        Summary: "Try read number!")]
    private static void _TryReadNumber()
    {
        ReadNumber("Try read number: ", (answer) =>
        {
            Debug.Log("Number: " + answer);
        });
    }

    [Interactive(
       Icon: "d_PreMatCube",
       Summary: "Try yes or no!")]
    private static void _TryYesOrNo()
    {
        YesOrNo("Try yes or no: ", (answer) =>
        {
            Debug.Log("Answer: " + answer);
        });
    }
}
#endif
