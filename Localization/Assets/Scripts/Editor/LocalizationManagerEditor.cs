using System.Linq;
using UnityEditor;
using UnityEngine;

[ExecuteAlways]
[CustomEditor(typeof(LocalizationManager))]
public class LocalizationManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        var localizationManager = (LocalizationManager) target;

        if (localizationManager.StringTables.Keys.ToArray().Length > 0)
        {
            var nodeTypes = localizationManager.StringTables.Keys.ToArray();
            localizationManager.currentLanguageSelectedIndex = EditorGUILayout.Popup("Current Language", localizationManager.currentLanguageSelectedIndex, nodeTypes);
            localizationManager.currentLanguage = localizationManager.StringTables.Keys.ToArray()[localizationManager.currentLanguageSelectedIndex];
        }
        else
        {
            if (GUILayout.Button("New String Table"))
            {
                localizationManager.StringTables.Add("empty", null);
            }
        }
        DrawDefaultInspector();
        EditorGUILayout.LabelField("");
        EditorGUILayout.LabelField("manager current language: " + localizationManager.CurrentLanguage);
        EditorGUILayout.LabelField("current language index: " + localizationManager.currentLanguageSelectedIndex);
    }
}
