using System.Collections.Generic;
using UnityEditor;

[CustomEditor(typeof(LocalizableText))]
public class LocalizableTextEditor : Editor
{
    public override void OnInspectorGUI()
    {
        var localizableText = (LocalizableText) target;
        var manager = LocalizationManager.GetInstance();
        if (manager == null)
        {
            EditorGUILayout.HelpBox("SETUP THE LOCALIZATION MANAGER BEFORE START", MessageType.Error);
            return;
        }
        
        var grid = CSVReader.ReadCSV(manager.StringTables[manager.CurrentLanguage]);
        var keys = new List<string>();
        for (var i = 0; i < grid.GetLength(1) - 1; i++)
        {
            keys.Add(grid[0, i]);
        }

        localizableText.selectedTextIndex = EditorGUILayout.Popup("Text String Table Key", localizableText.selectedTextIndex, keys.ToArray());
        localizableText.SetText(grid[1, localizableText.selectedTextIndex]);
    }
}
