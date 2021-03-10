using System.Linq;
using UnityEditor;

[CustomEditor(typeof(LocalizationManager))]
public class LocalizationManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        var localizationManager = (LocalizationManager) target;

        var nodeTypes = localizationManager.StringTables.Keys.ToArray();
        localizationManager.currentLanguageSelectedIndex = EditorGUILayout.Popup("Current Language", localizationManager.currentLanguageSelectedIndex, nodeTypes);
        localizationManager.currentLanguage = localizationManager.StringTables.Keys.ToArray()[localizationManager.currentLanguageSelectedIndex];


        DrawDefaultInspector();
        if(localizationManager.BaseDialogueNode == null)
            EditorGUILayout.HelpBox("BASE DIALOGUE NODE MUST NOT BE EMPTY.", MessageType.Error);
        EditorGUILayout.LabelField("");
        EditorGUILayout.LabelField("manager current language: " + localizationManager.currentLanguage);
        EditorGUILayout.LabelField("current language index: " + localizationManager.currentLanguageSelectedIndex);
    }
}
