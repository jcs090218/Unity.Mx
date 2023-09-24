#if UNITY_EDITOR
using UnityEngine;
using MetaX;

public class Commands : Mx
{
    /* Variables */

    /* Setter & Getter */

    /* Functions */

    public override bool Enable() { return false; }

    [Interactive(
            Icon: "Animation.Record",
            Tooltip: "Log Mx version.")]
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
