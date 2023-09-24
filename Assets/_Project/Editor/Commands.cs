#if UNITY_EDITOR
using UnityEngine;
using Mx;

public class Commands : Mx.Mx
{
    /* Variables */

    /* Setter & Getter */

    /* Functions */

    public override bool Enable() { return false; }

    [Interactive(
            Icon: "Animation.Record",
            Summary: "Log Mx version.")]
    private static void Mx_Version()
    {
        Debug.Log("Mx " + VERSION);
    }

    [Interactive]
    private static void TestPrivate()
    {
        Debug.Log("From test private");
    }

    [Interactive]
    private static void TestPublic()
    {

    }

    [Interactive]
    private static void TestSomeFunctionExterme()
    {

    }
}
#endif
