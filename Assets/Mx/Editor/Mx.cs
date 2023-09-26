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
    public abstract class Mx
    {
        /* Variables */

        public const string NAME = "Mx";
        public static readonly Version VERSION = new Version("0.1.0");

        /* Setter & Getter */

        /* Functions */

        public virtual bool Enable() { return true; }


        public static void CompletingRead(
            string prompt,
            List<string> candidates,
            List<string> summaries,
            CompletingReadCallback callback,
            bool requiredMatch = true)
        {
            MxCompletionWindow.OverrideIt(prompt, candidates, summaries, callback, requiredMatch);
        }

        public static void CompletingRead<T, Y>(
            string prompt,
            List<T> candidates,
            List<Y> summaries,
            CompletingReadCallback callback,
            bool requiredMatch = true)
        {
            var _candidates = MxUtil.ToListString(candidates);
            var _summaries = MxUtil.ToListString(summaries);
            CompletingRead(prompt, _candidates, _summaries, callback, requiredMatch);
        }

        public static void CompletingRead(
            string prompt, 
            List<string> candidates, 
            CompletingReadCallback callback,
            bool requiredMatch = true)
        {
            CompletingRead(prompt, candidates, null, callback, requiredMatch);
        }

        public static void CompletingRead<T>(
            string prompt,
            List<T> candidates,
            CompletingReadCallback callback,
            bool requiredMatch = true)
        {
            var _candidates = MxUtil.ToListString(candidates);
            CompletingRead(prompt, _candidates, null, callback, requiredMatch);
        }

        public static void CompletingRead(
            string prompt,
            (List<string>, List<string>) collection,
            CompletingReadCallback callback,
            bool requiredMatch = true)
        {
            CompletingRead(prompt, collection.Item1, collection.Item2, callback, requiredMatch);
        }

        public static void ReadString(
            string prompt, CompletingReadCallback callback)
        {
            CompletingRead(prompt, null, callback, false);
        }

        public static void ReadNumber(
            string prompt, CompletingReadCallback callback)
        {
            CompletingRead(prompt, null, (answer, summary) =>
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

        public static void YesOrNo(string prompt, CompletingReadCallback callback)
        {
            CompletingRead(prompt, new List<string>() { "Yes", "No"}, callback);
        }
    }
}
#endif
