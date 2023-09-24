#if UNITY_EDITOR
using System;
using UnityEditor;
using UnityEngine;

namespace Mx
{
    [Serializable]
    public class MxData
    {
        /* Variables */

        private float mSummaryRatio = 40.0f;

        private bool mCycle = true;

        private int mMinWindowWidthRatio = 50;
        private int mMinWindowHeightRatio = 25;

        /* Setter & Getter */

        public float SummaryRatio { get { return mSummaryRatio; } }
        public bool Cycle { get { return mCycle; } }
        public int MinWindowWidthRatio { get { return mMinWindowWidthRatio; } }
        public int MinWindowHeightRatio { get { return mMinWindowHeightRatio; } }

        /* Functions */

        public string FormKey(string name)
        {
            return MxUtil.FormKey("Data.") + name;
        }

        public void Init()
        {
            mSummaryRatio = EditorPrefs.GetFloat(FormKey("mSummaryRatio"));
            mCycle = EditorPrefs.GetBool(FormKey("mCycle"));
            mMinWindowWidthRatio = EditorPrefs.GetInt(FormKey("mMinWindowWidthRatio"));
            mMinWindowHeightRatio = EditorPrefs.GetInt(FormKey("mMinWindowHeightRatio"));
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
                mCycle = EditorGUILayout.Toggle(mCycle, GUILayout.MaxWidth(310.0f));

                MxEditorUtil.ResetButton(() => mCycle = true);
                GUILayout.FlexibleSpace();
            });

            EditorGUILayout.LabelField("Window", EditorStyles.boldLabel);

            MxEditorUtil.Indent(() =>
            {
                MxEditorUtil.BeginHorizontal(() =>
                {
                    EditorGUILayout.LabelField("Minimum width ratio", options);
                    mMinWindowWidthRatio = EditorGUILayout.IntSlider(mMinWindowWidthRatio, 10, 80);

                    MxEditorUtil.ResetButton(() => mMinWindowWidthRatio = 50);
                    GUILayout.FlexibleSpace();
                });

                MxEditorUtil.BeginHorizontal(() =>
                {
                    EditorGUILayout.LabelField("Minimum height ratio", options);
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
            EditorPrefs.SetInt(FormKey("mMinWindowWidthRatio"), mMinWindowWidthRatio);
            EditorPrefs.SetInt(FormKey("mMinWindowHeightRatio"), mMinWindowHeightRatio);
        }

        private void Reset()
        {
            mSummaryRatio = 40.0f;

            mMinWindowWidthRatio = 50;
            mMinWindowHeightRatio = 25;

            SavePref();
        }
    }
}
#endif
