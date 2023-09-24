#if UNITY_EDITOR
using UnityEngine;
using MetaX;

public class Commands : Mx
{
    /* Variables */

    /* Setter & Getter */

    /* Functions */

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
