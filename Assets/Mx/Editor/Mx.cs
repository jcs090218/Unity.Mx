#if UNITY_EDITOR
/**
 * Copyright (c) Jen-Chieh Shen. All rights reserved.
 * 
 * jcs090218@gmail.com
 */
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Mx
{
    public delegate void CompletingReadCallback(string answer);

    public abstract class Mx
    {
        /* Variables */

        public const string NAME = "Mx";
        public static readonly Version VERSION = new Version(Application.version);

        /* Setter & Getter */

        /* Functions */

        public virtual bool Enable() { return true; }

        public static void CompletionRead(
            string prompt, List<string> candidates, CompletingReadCallback callback,
            bool requiredMatch = true)
        {
            MxCompletionWindow.OverrideIt(prompt, candidates, callback, requiredMatch);
        }

        public static void ReadString(
            string prompt, CompletingReadCallback callback)
        {
            MxCompletionWindow.OverrideIt(prompt, null, callback, false);
        }

        public static void ReadNumber(
            string prompt, CompletingReadCallback callback)
        {
            MxCompletionWindow.OverrideIt(prompt, null, (answer) =>
                {
                    float number;

                    if (!float.TryParse(answer, out number))
                    {
                        MxCompletionWindow.INHIBIT_CLOSE = true;
                        Debug.LogError("Invalid number: " + answer);
                        return;
                    }

                    callback.Invoke(answer);
                }, false);
        }
    }
}
#endif
