/**
 * Copyright (c) Jen-Chieh Shen. All rights reserved.
 * 
 * jcs090218@gmail.com
 */
using UnityEditor;
using UnityEngine;

namespace Mx
{
    public class MxCInput : Mx
    {
        /* Variables */

        /* Setter & Getter */

        /* Functions */

        public override bool Enable() { return true; }


        [Interactive(Summary: "List out key code for exploration")]
        public static void ListKeyCode()
        {
            var result = MxUtil.EnumTuple(typeof(KeyCode));
            CompletingRead("Key code: ", result, null);
        }

        public static void SwitchTheme()
        {
            
        }
    }
}
