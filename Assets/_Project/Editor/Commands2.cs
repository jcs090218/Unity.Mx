#if UNITY_EDITOR
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
}
#endif
