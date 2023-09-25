#if UNITY_EDITOR
/**
 * Copyright (c) Jen-Chieh Shen. All rights reserved.
 * 
 * jcs090218@gmail.com
 */
using UnityEngine;

namespace Mx
{
    public class MxCInput : Mx
    {
        /* Variables */

        /* Setter & Getter */

        /* Functions */

        public override bool Enable() { return true; }


        [Interactive(
            Summary: "List out key code")]
        public static void ListKeyCode()
        {
            var result = MxUtil.EnumTuple(typeof(KeyCode));
            CompletingRead("Key code: ", result, null);
        }
    }
}
#endif
