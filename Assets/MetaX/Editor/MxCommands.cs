#if UNITY_EDITOR
using UnityEngine;

namespace MetaX
{
    public class MxCommands : Mx
    {
        /* Variables */

        /* Setter & Getter */

        /* Functions */

        [Interactive]
        private void MetaX_Version()
        {
            Debug.Log("MetaX 0.1.0");
        }
    }
}
#endif
