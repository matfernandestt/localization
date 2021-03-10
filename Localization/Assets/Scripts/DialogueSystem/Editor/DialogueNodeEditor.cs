using System;
using System.Collections.Generic;
using DialogueSystem;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(DialogueNode))]
public class DialogueNodeEditor : Editor
{
    public override void OnInspectorGUI()
    {
        var dialogueNode = (DialogueNode) target;
        var manager = LocalizationManager.GetInstance();
        if (manager == null)
        {
            EditorGUILayout.HelpBox("SETUP THE LOCALIZATION MANAGER BEFORE START", MessageType.Error);
            return;
        }

        EditorGUILayout.BeginVertical(EditorStyles.helpBox);
        var nodeTypes = Enum.GetNames(typeof(NodeType));
        dialogueNode.nodeTypeSelectedIndex = EditorGUILayout.Popup("Node Type", dialogueNode.nodeTypeSelectedIndex, nodeTypes);
        var selectedType = dialogueNode.TypeOfNode;
        EditorGUILayout.EndVertical();
        EditorGUILayout.LabelField("");

        switch (selectedType)
        {
            case NodeType.Speak:
                EditorGUILayout.BeginVertical(EditorStyles.helpBox);
                GUI.backgroundColor = new Color(0.25f, 1f, 0f);
                EditorGUILayout.BeginHorizontal(EditorStyles.helpBox);
                dialogueNode.isLocalizable = EditorGUILayout.Toggle("Is Localizable", dialogueNode.isLocalizable);
                EditorGUILayout.EndHorizontal();
                if (dialogueNode.isLocalizable)
                {
                    var grid = CSVReader.ReadCSV(manager.StringTables[manager.CurrentLanguage]);
                    var keys = new List<string>();
                    for (var i = 0; i < grid.GetLength(1) - 1; i++)
                    {
                        keys.Add(grid[0, i]);
                    }
                    dialogueNode.serializedTextSpeechSelectedIndex = EditorGUILayout.Popup("Speech String Table Key", dialogueNode.serializedTextSpeechSelectedIndex, keys.ToArray());
                    dialogueNode.textField = grid[1, dialogueNode.serializedTextSpeechSelectedIndex];
                }
                else
                {
                    dialogueNode.textField = EditorGUILayout.TextField("Speech Text", dialogueNode.textField);
                }
                dialogueNode.waveField = (AudioClip)EditorGUILayout.ObjectField("Wave", dialogueNode.waveField, typeof(AudioClip), false);
                EditorGUILayout.EndVertical();
                break;
            case NodeType.Choose:
                EditorGUILayout.BeginVertical(EditorStyles.helpBox);
                if (GUILayout.Button("New Option"))
                {
                    dialogueNode.optionNodes.Add(BaseNodeReferencer.GetInstance().baseNode);
                }
                EditorGUILayout.LabelField("");
                EditorGUILayout.BeginVertical(EditorStyles.helpBox);
                for (var i = 0; i < dialogueNode.optionNodes.Count; i++)
                {
                    if(i != 0) EditorGUILayout.LabelField("");
                    EditorGUILayout.BeginVertical(EditorStyles.helpBox);
                    var optionField = EditorGUILayout.ObjectField(dialogueNode.optionNodes[i], typeof(ScriptableObject), false);
                    dialogueNode.optionNodes[i] = (DialogueNode)optionField;
                    GUI.backgroundColor = Color.red;
                    if (GUILayout.Button("Delete Option"))
                    {
                        dialogueNode.optionNodes.RemoveAt(i);
                    }
                    EditorGUILayout.EndVertical();
                }
                EditorGUILayout.EndVertical();
                EditorGUILayout.EndVertical();

                break;
            case NodeType.Option:
                EditorGUILayout.BeginVertical(EditorStyles.helpBox);
                GUI.backgroundColor = new Color(0.39f, 0.77f, 1f);
                EditorGUILayout.BeginHorizontal(EditorStyles.helpBox);
                dialogueNode.isLocalizable = EditorGUILayout.Toggle("Is Localizable", dialogueNode.isLocalizable);
                EditorGUILayout.EndHorizontal();
                if (dialogueNode.isLocalizable)
                {
                    var grid = CSVReader.ReadCSV(manager.StringTables[manager.CurrentLanguage]);
                    var keys = new List<string>();
                    for (var i = 0; i < grid.GetLength(1) - 1; i++)
                    {
                        keys.Add(grid[0, i]);
                    }
                    dialogueNode.serializedTextSpeechSelectedIndex = EditorGUILayout.Popup("Option String Table Key", dialogueNode.serializedTextSpeechSelectedIndex, keys.ToArray());
                    dialogueNode.textField = grid[1, dialogueNode.serializedTextSpeechSelectedIndex];
                }
                else
                {
                    dialogueNode.textField = EditorGUILayout.TextField("Option Text", dialogueNode.textField);
                }
                dialogueNode.waveField = (AudioClip)EditorGUILayout.ObjectField("Wave", dialogueNode.waveField, typeof(AudioClip), false);
                EditorGUILayout.EndVertical();
                break;
            default:
                Debug.LogError("DIALOGUE SYSTEM: Node type not implemented.");
                break;
        }

        //DrawDefaultInspector();
        
        EditorGUILayout.LabelField("");
        GUI.backgroundColor = Color.red;
        EditorGUILayout.BeginVertical(EditorStyles.helpBox);
        EditorGUILayout.LabelField("type index: " + dialogueNode.nodeTypeSelectedIndex);
        EditorGUILayout.LabelField("text field: " + dialogueNode.textField);
        EditorGUILayout.LabelField("text field index: " + dialogueNode.serializedTextSpeechSelectedIndex);
        EditorGUILayout.EndVertical();
    }
}
