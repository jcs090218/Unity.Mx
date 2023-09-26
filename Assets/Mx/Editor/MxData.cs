#if UNITY_EDITOR
/**
 * Copyright (c) Jen-Chieh Shen. All rights reserved.
 * 
 * jcs090218@gmail.com
 */
using System;
using UnityEditor;
using UnityEngine;

namespace Mx
{
    public enum SortType
    {
        None,

        Alphabetic,
        Length,
    }

    [Serializable]
    public class MxData
    {
        /* Variables */

        private float mSummaryRatio = 40.0f;
        private bool mCycle = true;
        private SortType mInitialSortingOrder = SortType.Alphabetic;

        private int mMinWindowWidthRatio = 50;
        private int mMinWindowHeightRatio = 25;

        /* Setter & Getter */

        public float SummaryRatio { get { return mSummaryRatio; } }
        public bool Cycle { get { return mCycle; } }
        public SortType InitialSortingOrder { get { return mInitialSortingOrder; } }
        public int MinWindowWidthRatio { get { return mMinWindowWidthRatio; } }
        public int MinWindowHeightRatio { get { return mMinWindowHeightRatio; } }

        /* Functions */

        public string FormKey(string name)
        {
            return MxUtil.FormKey("Data.") + name;
        }

        public void Init()
        {
            mSummaryRatio = EditorPrefs.GetFloat(FormKey("mSummaryRatio"), mSummaryRatio);
            mCycle = EditorPrefs.GetBool(FormKey("mCycle"), mCycle);
            mInitialSortingOrder = (SortType)EditorPrefs.GetInt(FormKey("mInitialSortingOrder"), (int)mInitialSortingOrder);
            mMinWindowWidthRatio = EditorPrefs.GetInt(FormKey("mMinWindowWidthRatio"), mMinWindowWidthRatio);
            mMinWindowHeightRatio = EditorPrefs.GetInt(FormKey("mMinWindowHeightRatio"), mMinWindowHeightRatio);
        }

        public void Draw()
        {
            GUILayoutOption[] options = {
                GUILayout.MaxWidth(150.0f),
                GUILayout.ExpandWidth(false),
            };

            MxEditorUtil.BeginHorizontal(() =>
            {
                EditorGUILayout.LabelField("Summary Ratio", options);
                mSummaryRatio = EditorGUILayout.Slider(mSummaryRatio, 20.0f, 80.0f);

                MxEditorUtil.ResetButton(() => mSummaryRatio = 40.0f);
                GUILayout.FlexibleSpace();
            });

            MxEditorUtil.BeginHorizontal(() =>
            {
                EditorGUILayout.LabelField("Cycle", options);
                mCycle = EditorGUILayout.Toggle(mCycle);
                EditorGUILayout.LabelField("", GUILayout.MaxWidth(138));

                MxEditorUtil.ResetButton(() => mCycle = true);
                GUILayout.FlexibleSpace();
            });

            MxEditorUtil.BeginHorizontal(() =>
            {
                EditorGUILayout.LabelField("Initial Sorting Order", options);
                mInitialSortingOrder = (SortType)EditorGUILayout.EnumPopup(mInitialSortingOrder);
                EditorGUILayout.LabelField("", GUILayout.MaxWidth(102));

                MxEditorUtil.ResetButton(() => mInitialSortingOrder = SortType.Alphabetic);
                GUILayout.FlexibleSpace();
            });

            EditorGUILayout.LabelField("Window", EditorStyles.boldLabel);

            MxEditorUtil.Indent(() =>
            {
                MxEditorUtil.BeginHorizontal(() =>
                {
                    EditorGUILayout.LabelField("Minimum Width Ratio", options);
                    mMinWindowWidthRatio = EditorGUILayout.IntSlider(mMinWindowWidthRatio, 10, 80);

                    MxEditorUtil.ResetButton(() => mMinWindowWidthRatio = 50);
                    GUILayout.FlexibleSpace();
                });

                MxEditorUtil.BeginHorizontal(() =>
                {
                    EditorGUILayout.LabelField("Minimum Height Ratio", options);
                    mMinWindowHeightRatio = EditorGUILayout.IntSlider(mMinWindowHeightRatio, 10, 80);

                    MxEditorUtil.ResetButton(() => mMinWindowHeightRatio = 25);
                    GUILayout.FlexibleSpace();
                });
            });
        }

        public void SavePref()
        {
            EditorPrefs.SetFloat(FormKey("mSummaryRatio"), mSummaryRatio);
            EditorPrefs.SetBool(FormKey("mCycle"), mCycle);
            EditorPrefs.SetInt(FormKey("mInitialSortingOrder"), (int)mInitialSortingOrder);
            EditorPrefs.SetInt(FormKey("mMinWindowWidthRatio"), mMinWindowWidthRatio);
            EditorPrefs.SetInt(FormKey("mMinWindowHeightRatio"), mMinWindowHeightRatio);
        }
    }
}
#endif
