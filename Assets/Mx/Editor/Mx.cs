#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using UnityEngine;

namespace MetaX
{
    public delegate void CompletingReadCallback(string answer);

    public class Mx
    {
        /* Variables */

        public const string NAME = "Mx";
        public static readonly Version VERSION = new Version(Application.version);

        /* Setter & Getter */

        /* Functions */
        
        public static void CompletionRead(string prompt, List<string> candidates, CompletingReadCallback callback)
        {
            MxWindow.OverrideIt(prompt, candidates, callback);
        }

        public static void ReadString(string prompt, CompletingReadCallback callback)
        {
            MxWindow.OverrideIt(prompt, null, callback);
        }
    }
}
#endif
