#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEngine;
using MetaX;

public class Commands2 : Mx
{
    /* Variables */

    /* Setter & Getter */

    /* Functions */

    [Interactive(
            Icon: "Animation.Record",
            Tooltip: "Log Mx version.")]
    private static void Mx_Version()
    {
        Debug.Log("Mx " + VERSION);
    }

    [Interactive(
            Icon: "d_PreMatCube",
            Tooltip: "Try completing read!")]
    private static void TryCompletingRead()
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
            Tooltip: "Try read string!")]
    private static void TryReadString()
    {
        ReadString("Try read string: ",
        (answer) =>
        {
            Debug.Log("Answer: " + answer);
        });

    }
}
#endif
