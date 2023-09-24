#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEngine;
using Mx;

public class Commands2 : Mx.Mx
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
        ReadString("Try read string: ",
        (answer) =>
        {
            Debug.Log("Answer: " + answer);
        });

    }
}
#endif
