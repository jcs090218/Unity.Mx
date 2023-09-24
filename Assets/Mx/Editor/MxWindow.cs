#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using FlxCs;
using System.Drawing.Printing;
using static System.Collections.Specialized.BitVector32;

namespace MetaX
{
    public class MxWindow : EditorWindow
    {
        /* Variables */

        public static MxWindow instance = null;

        public static bool CYCLE = true;

        private const string DEFAULT_PROMPT = "M-x ";

#if UNITY_2022_3_OR_NEWER
        private const string mToolbarSearchTextFieldStyleName = "ToolbarSearchTextField";
        private const string mToolbarSearchCancelButtonStyleName = "ToolbarSearchCancelButton";
#else
        private const string mToolbarSearchTextFieldStyleName = "ToolbarSeachTextField";
        private const string mToolbarSearchCancelButtonStyleName = "ToolbarSeachCancelButton";
#endif

        private const string FIND_SEARCH_FIELD_CTRL_NAME = "FindEditorToolsSearchField";

        public static string OVERRIDE_PROMPT = null;
        public static List<string> OVERRIDE_COMMANDS = null;
        public static CompletingReadCallback OVERRIDE_EXECUTE_COMMAND = null;

        private List<Mx> mTypes = null;
        private List<MethodInfo> mMethods = null;

        private Dictionary<string, MethodInfo> mMethodsIndex = new();

        private string mSearchString = String.Empty;
        private int mSelected = 0;
        private float mScrollBar = 0.0f;

        private int mCommandsFilteredCount = 0;
        private List<string> mCommands = new();
        private List<string> mCommandsFiltered = new();

        private static List<string> mHistory = null;

        private enum InputType
        {
            None,
            Clear,
            Close,
            Execute,
            SelectionDown,
            SelectionUp,
        };

        public class ToolUsage
        {
            private int m_iValue;

            //

            public string Path
            {
                get;
                private set;
            }

            public int Count
            {
                get { return this.Favorite ? m_iValue ^ (1 << 30) : m_iValue; }
                set { m_iValue = this.CreateValue(value, this.Favorite); }
            }

            public bool Favorite
            {
                get { return (m_iValue & (1 << 30)) != 0; }
                set { m_iValue = this.CreateValue(this.Count, value); }
            }

            public int Value
            {
                get { return m_iValue; }
            }

            //

            public void ToggleFavorite()
            {
                this.Favorite = !this.Favorite;
            }

            private int CreateValue(int _iCount, bool _bFavorite)
            {
                return Mathf.Min(_iCount, (1 << 30) - 1) | (_bFavorite ? (1 << 30) : 0);
            }

            //

            public ToolUsage(string _sPath, int _iCount, bool _bFavorite)
            {
                Path = _sPath;
                m_iValue = this.CreateValue(_iCount, _bFavorite);
            }

            public ToolUsage(string _sPath, int _iValue)
            {
                Path = _sPath;
                m_iValue = _iValue;
            }
        }

        private const float c_fButtonStartPosition = 24.0f;
        private const float c_fButtonHeight = 16.0f;
        private const float c_fSrollbarWidth = 15.0f;
        private const float mIconWidth = 20.0f;
        private static readonly Color c_cHover = new Color32(38, 79, 120, 255);
        private static readonly Color c_cDefault = new Color32(46, 46, 46, 255);
        private GUIStyle m_guiStyleHover = new GUIStyle();
        private GUIStyle m_guiStyleDefault = new GUIStyle();

        private static readonly Color mDefaultText = new Color(46, 46, 46);  // #2E2E2E

        /* Setter & Getter */

        /* Functions */

        [MenuItem("Tools/MetaX/Window &x", false, -1000)]
        public static void ShowWindow() { GetWindow<MxWindow>("MetaX"); }

        private void OnEnable()
        {
            instance = this;

            mHistory = GetHistory();

            EditorApplication.quitting += OnQuitting;

            /* Initialize some UI components! */
            {
                m_guiStyleHover.normal.textColor = mDefaultText;
                m_guiStyleDefault.normal.textColor = mDefaultText;
            }

            Refresh();
        }

        private void OnDisable()
        {
            OVERRIDE_PROMPT = null;
            OVERRIDE_COMMANDS = null;
            OVERRIDE_EXECUTE_COMMAND = null;
        }

        private void OnQuitting()
        {
            ClearHistory();
        }

        private void OnGUI()
        {
            InputType input = this.UpdateEventBeforeDraw(Event.current);

            DrawSearchBar(input);
            DrawCompletion(input);
        }

        public void Refresh()
        {
            this.mTypes = GetMx();
            this.mMethods = GetMethods();

            RecreateCommandList();
        }

        /// <summary>
        /// Return a list of Mx subclasses.
        /// </summary>
        private List<Mx> GetMx()
        {
            return AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(assembly => assembly.GetTypes())
                .Where(type => type.IsSubclassOf(typeof(Mx)))
                .Select(type => Activator.CreateInstance(type) as Mx)
                .ToList();
        }

        /// <summary>
        /// Return a list of methods from Mx subclasses.
        /// </summary>
        private List<MethodInfo> GetMethods()
        {
            var bindings = BindingFlags.Static
                | BindingFlags.Public
                | BindingFlags.NonPublic;

            return mTypes.SelectMany(t => t.GetType().GetMethods(bindings))
                .Where(m => m.GetCustomAttributes(typeof(InteractiveAttribute), false).Length > 0)
                .ToList();
        }

        private void DrawSearchBar(InputType input)
        {
            EditorGUI.BeginChangeCheck();
            {
                EditorGUILayout.BeginHorizontal();
                {
                    string prompt = OVERRIDE_PROMPT ?? DEFAULT_PROMPT;

                    float width = EditorStyles.label.CalcSize(new GUIContent(prompt)).x;

                    EditorGUILayout.LabelField(prompt, GUILayout.Width(width));

                    GUI.SetNextControlName(FIND_SEARCH_FIELD_CTRL_NAME);
                    mSearchString = EditorGUILayout.TextField(mSearchString, GUI.skin.FindStyle(mToolbarSearchTextFieldStyleName));

                    if (GUILayout.Button(string.Empty, GUI.skin.FindStyle(mToolbarSearchCancelButtonStyleName)) && mSearchString != string.Empty)
                    {
                        Event evt = new Event();
                        evt.type = EventType.KeyDown;
                        evt.keyCode = KeyCode.Escape;
                        this.SendEvent(evt);
                    }

                    if (String.IsNullOrEmpty(mSearchString) || input == InputType.Clear)
                    {
                        EditorGUI.FocusTextInControl(FIND_SEARCH_FIELD_CTRL_NAME);
                    }
                }
                EditorGUILayout.EndHorizontal();
            }

            if (EditorGUI.EndChangeCheck())
            {
                this.RecreateFilteredList();
                mSelected = Mathf.Clamp(mSelected, 0, mCommandsFilteredCount - 1);
            }
        }

        private void DrawCompletion(InputType input)
        {
            Rect rectPosition = this.position;
            float fButtonAreaHeight = rectPosition.height - c_fButtonStartPosition;
            float fButtonCount = fButtonAreaHeight / c_fButtonHeight;
            int iButtonCountFloor = Mathf.Min(Mathf.FloorToInt(fButtonCount), mCommandsFilteredCount); // used for showing the scrollbar
            int iButtonCountCeil = Mathf.Min(Mathf.CeilToInt(fButtonCount), mCommandsFilteredCount);   // used to additionally show the last button (even if visible only in half)
            bool bScrollbar = iButtonCountCeil < mCommandsFilteredCount;

            //

            if (bScrollbar)
            {
                mScrollBar = GUI.VerticalScrollbar(
                    new Rect(
                        rectPosition.width - c_fSrollbarWidth,
                        c_fButtonStartPosition,
                        c_fSrollbarWidth,
                        rectPosition.height - c_fButtonStartPosition
                    ),
                    mScrollBar,
                    iButtonCountFloor * c_fButtonHeight,
                    0.0f,
                    mCommandsFilteredCount * c_fButtonHeight
                );
            }

            int iBase = bScrollbar ? Mathf.RoundToInt(mScrollBar / c_fButtonHeight) : 0;

            for (int i = iBase, j = 0, imax = Mathf.Min(iBase + iButtonCountCeil, mCommandsFilteredCount); i < imax; ++i, ++j)
            {
                string name = mCommandsFiltered[i];

                bool bSelected = (i == mSelected);

                Rect rect = new Rect(
                    mIconWidth,
                    c_fButtonStartPosition + j * c_fButtonHeight,
                    rectPosition.width - (bScrollbar ? c_fSrollbarWidth : 0.0f) - mIconWidth,
                    c_fButtonHeight - 1.0f
                );

                EditorGUI.DrawRect(rect, bSelected ? c_cHover : c_cDefault);

                float fIndentation = EditorGUI.IndentedRect(rect).x - rect.x;
                m_guiStyleHover.fixedWidth = rect.width - fIndentation;
                m_guiStyleDefault.fixedWidth = rect.width - fIndentation;

                if (IsCompletingRead())
                {
                    EditorGUI.LabelField(rect, name, bSelected ? m_guiStyleHover : m_guiStyleDefault);

                    Rect rectIcon = new Rect(0.0f, c_fButtonStartPosition + j * c_fButtonHeight, mIconWidth, c_fButtonHeight - 1.0f);
                    EditorGUI.DrawRect(rectIcon, bSelected ? c_cHover : c_cDefault);
                }
                else
                {
                    InteractiveAttribute attr = GetAttribute(name);

                    EditorGUI.LabelField(rect, new GUIContent(name, null, attr.tooltip), bSelected ? m_guiStyleHover : m_guiStyleDefault);

                    Rect rectIcon = new Rect(0.0f, c_fButtonStartPosition + j * c_fButtonHeight, mIconWidth, c_fButtonHeight - 1.0f);
                    EditorGUI.DrawRect(rectIcon, bSelected ? c_cHover : c_cDefault);
                    EditorGUI.LabelField(rectIcon, new GUIContent(attr.texture, attr.tooltip));
                }
            }

            this.UpdateEventAfterDraw(Event.current, input, iBase, bScrollbar);
        }

        private InputType UpdateEventBeforeDraw(Event evt)
        {
            if (evt == null)
                return InputType.None;

            switch (evt.type)
            {
                case EventType.KeyDown:

                    switch (evt.keyCode)
                    {
                        case KeyCode.Return:
                            evt.Use();
                            return InputType.Execute;

                        case KeyCode.DownArrow:
                            evt.Use();
                            return InputType.SelectionDown;

                        case KeyCode.UpArrow:
                            evt.Use();
                            return InputType.SelectionUp;

                        case KeyCode.Escape:
                            {
                                if (mSearchString == string.Empty)
                                {
                                    evt.Use();
                                    this.Close();
                                    return InputType.Close;
                                }
                                else
                                {
                                    return InputType.Clear;
                                }
                            }
                    }
                    break;
                default:
                    break;
            }

            return InputType.None;
        }

        private void UpdateEventAfterDraw(Event evt, InputType input, int currentBase, bool scrollbar)
        {
            if (evt == null)
                return;

            switch (input)
            {
                case InputType.Close:
                    return;

                case InputType.Execute:
                    {
                        string asnwer = mSearchString;

                        if (mCommandsFilteredCount > 0 && 
                            mSelected < mCommandsFilteredCount)
                        {
                            asnwer = mCommandsFiltered[mSelected];
                        }

                        this.ExecuteCommand(asnwer);
                    }
                    return;

                case InputType.SelectionDown:
                    {
                        ++mSelected;

                        if (CYCLE)
                        {
                            if (mSelected >= mCommandsFilteredCount)
                                mSelected = 0;
                        }
                        else
                        {
                            mSelected = Mathf.Clamp(mSelected, 0, mCommandsFilteredCount - 1);
                        }

                        if (scrollbar)
                            this.CheckScrollToSelected();
                        this.Repaint();
                    }
                    return;

                case InputType.SelectionUp:
                    {
                        --mSelected;

                        if (CYCLE)
                        {
                            if (mSelected < 0)
                                mSelected = mCommandsFilteredCount - 1;
                        }
                        else
                        {
                            mSelected = Mathf.Clamp(mSelected, 0, mCommandsFilteredCount - 1);
                        }

                        if (scrollbar)
                            this.CheckScrollToSelected();
                        this.Repaint();
                    }
                    return;

                default:
                    break;
            }

            switch (evt.type)
            {
                case EventType.MouseDown:
                    {
                        int iIndex = currentBase + Mathf.FloorToInt((evt.mousePosition.y - c_fButtonStartPosition) / c_fButtonHeight);
                        if (iIndex >= 0 && iIndex < mCommandsFilteredCount && (!scrollbar || evt.mousePosition.x < this.position.width - c_fSrollbarWidth))
                        {
                            this.ExecuteCommand(mCommandsFiltered[iIndex]);
                        }
                    }
                    break;

                case EventType.ScrollWheel:
                    {
                        if (scrollbar)
                        {
                            mScrollBar += evt.delta.y * c_fButtonHeight;
                            this.Repaint();
                        }
                    }
                    break;

                default:
                    break;
            }
        }

        private void CheckScrollToSelected()
        {
            float fButtonAreaHeight = this.position.height - c_fButtonStartPosition;
            float fButtonCount = fButtonAreaHeight / c_fButtonHeight;
            int iBase = Mathf.RoundToInt(mScrollBar / c_fButtonHeight);
            int iButtonCountFloor = Mathf.Min(Mathf.FloorToInt(fButtonCount), mCommandsFilteredCount);

            if (mSelected >= Mathf.Min(iBase + iButtonCountFloor, mCommandsFilteredCount))
            {
                mScrollBar = c_fButtonHeight * (mSelected - iButtonCountFloor + 1);
            }
            else if (mSelected < iBase)
            {
                mScrollBar = c_fButtonHeight * mSelected;
            }
        }

        private void ExecuteCommand(string name)
        {
            if (IsCompletingRead())
            {
                if (OVERRIDE_EXECUTE_COMMAND != null)
                    OVERRIDE_EXECUTE_COMMAND.Invoke(name);

                this.Close();
                return;
            }

            MoveToLast(mHistory, name);
            UpdateHistory();

            MethodInfo method = GetMethod(name);
            method.Invoke(null, null);

            if (IsCompletingRead())
            {
                mSearchString = "";
                RecreateCommandList();
                return;
            }

            this.Close();
        }

        private string FormName(MethodInfo info, bool full = false)
        {
            if (full)
                return info.DeclaringType + "." + info.Name;
            return info.Name;
        }

        private void RecreateCommandList()
        {
            if (IsCompletingRead())
            {
                mCommands = OVERRIDE_COMMANDS;
                RecreateFilteredList();
                return;
            }

            mCommands.Clear();
            mMethodsIndex.Clear();

            HashSet<string> repeated = new();

            foreach (MethodInfo method in mMethods)
            {
                string name = FormName(method);
                string nameFull = FormName(method, true);

                if (repeated.Contains(name))
                {
                    if (mMethodsIndex.ContainsKey(name))
                    {
                        MethodInfo temp = mMethodsIndex[name];

                        mMethodsIndex.Remove(name);
                        mCommands.Remove(name);

                        //string _name = FormName(temp);
                        string _nameFull = FormName(temp, true);

                        mMethodsIndex.Add(_nameFull, temp);
                        mCommands.Add(_nameFull);
                    }

                    name = nameFull;
                }
                else
                {
                    repeated.Add(name);
                }

                mMethodsIndex.Add(name, method);
                mCommands.Add(name);
            }

            // Sort history in the correct order!
            mCommands = mCommands.OrderByDescending(d => mHistory.IndexOf(d)).ToList();

            RecreateFilteredList();
        }

        private void RecreateFilteredList()
        {
            if (mCommands == null)
            {
                mCommandsFiltered = new List<string>();
            }
            else if (String.IsNullOrEmpty(mSearchString))
            {
                mCommandsFiltered = new List<string>(mCommands);
            }
            else
            {
                mCommandsFiltered.Clear();

                SortedDictionary<int, List<string>> scores = new();

                for (int i = 0, imax = mCommands.Count; i < imax; ++i)
                {
                    string cmd = mCommands[i];

                    FlxCs.Score score = Flx.Score(cmd, mSearchString);

                    // First filtered out low score!
                    if (score != null && score.score > 0)
                    {
                        if (!scores.ContainsKey(score.score))
                            scores.Add(score.score, new List<string>());

                        scores[score.score].Add(cmd);
                    }
                }

                // The sort it by the score!
                foreach (int key in scores.Keys.Reverse())
                {
                    scores[key] = scores[key].OrderBy(i => i).ToList();

                    foreach (string cmd in scores[key])
                    {
                        mCommandsFiltered.Add(cmd);
                    }
                }
            }

            mCommandsFilteredCount = mCommandsFiltered.Count;
        }

        private static void MoveToFront<T>(List<T> lst, T obj)
        {
            lst.Remove(obj);
            lst.Insert(0, obj);
        }

        private static void MoveToLast<T>(List<T> lst, T obj)
        {
            lst.Remove(obj);
            lst.Add(obj);
        }

        public MethodInfo GetMethod(string candidate)
        {
            return mMethodsIndex[candidate];
        }

        public InteractiveAttribute GetAttribute(string candidate)
        {
            return mMethodsIndex[candidate].GetCustomAttribute(typeof(InteractiveAttribute), false) as InteractiveAttribute;
        }

        #region History
        public static readonly string PK_HISTORY = MxEditorUtil.FormKey("History");

        public static void UpdateHistory()
        {
            MxEditorUtil.SetList(PK_HISTORY, mHistory);
        }

        public static List<string> GetHistory()
        {
            return MxEditorUtil.GetList(PK_HISTORY);
        }

        public static void ClearHistory()
        {
            mHistory.Clear();
            UpdateHistory();
        }
        #endregion

        public static void OverrideIt(string prompt, List<string> candidates, CompletingReadCallback callback)
        {
            OVERRIDE_PROMPT = prompt;
            OVERRIDE_COMMANDS = candidates;
            OVERRIDE_EXECUTE_COMMAND = callback;
        }

        public static bool IsCompletingRead()
        {
            return OVERRIDE_PROMPT != null
                || OVERRIDE_COMMANDS != null
                || OVERRIDE_EXECUTE_COMMAND != null;
        }
    }
}
#endif
