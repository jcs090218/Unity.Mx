#if UNITY_EDITOR
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Mx
{
    public class MxSettings : ScriptableObject
    {
        /* Variables */

        public const string MxSettingsPath = "Assets/Mx/Editor/Mx.asset";

        [SerializeField]
        public int m_Number;

        [SerializeField]
        public string m_SomeString;

        /* Setter & Getter */

        /* Functions */

        internal static MxSettings GetOrCreateSettings()
        {
            var settings = AssetDatabase.LoadAssetAtPath<MxSettings>(MxSettingsPath);
            if (settings == null)
            {
                settings = CreateInstance<MxSettings>();
                {
                    settings.m_Number = 42;
                    settings.m_SomeString = "The answer to the universe";
                }
                AssetDatabase.CreateAsset(settings, MxSettingsPath);
                AssetDatabase.SaveAssets();
            }
            return settings;
        }

        internal static SerializedObject GetSerializedSettings()
        {
            return new SerializedObject(GetOrCreateSettings());
        }
    }


    // Create MyCustomSettingsProvider by deriving from SettingsProvider:
    class MxSettingsProvider : SettingsProvider
    {
        private SerializedObject mSettings;

        class Styles
        {
            public static GUIContent number = new GUIContent("My Number");
            public static GUIContent someString = new GUIContent("Some string");
        }
        public const string MxSettingsPath = "Assets/Mx/Editor/Mx.asset";
        public MxSettingsProvider(string path, SettingsScope scope = SettingsScope.User)
            : base(path, scope) { }

        public static bool IsSettingsAvailable()
        {
            return File.Exists(MxSettingsPath);
        }

        public override void OnActivate(string searchContext, VisualElement rootElement)
        {
            // This function is called when the user clicks on the MyCustom element in the Settings window.
            mSettings = MxSettings.GetSerializedSettings();
        }

        public override void OnGUI(string searchContext)
        {
            // Use IMGUI to display UI:
            EditorGUILayout.PropertyField(mSettings.FindProperty("m_Number"), Styles.number);
            EditorGUILayout.PropertyField(mSettings.FindProperty("m_SomeString"), Styles.someString);
        }

        // Register the SettingsProvider
        [SettingsProvider]
        public static SettingsProvider CreateMxSettingsProvider()
        {
            if (IsSettingsAvailable())
            {
                var provider = new MxSettingsProvider("Project/Mx", SettingsScope.Project);

                // Automatically extract all keywords from the Styles.
                provider.keywords = GetSearchKeywordsFromGUIContentProperties<Styles>();
                return provider;
            }

            // Settings Asset doesn't exist yet; no need to display anything in the Settings window.
            return null;
        }
    }
}
#endif
