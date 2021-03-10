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
        var manager = LocalizationManager.Instance;
        if (manager == null)
        {
            EditorGUILayout.HelpBox("SETUP THE LOCALIZATION MANAGER BEFORE START", MessageType.Error);
            return;
        }

        var nodeTypes = Enum.GetNames(typeof(NodeType));
        dialogueNode.nodeTypeSelectedIndex = EditorGUILayout.Popup("Node Type", dialogueNode.nodeTypeSelectedIndex, nodeTypes);
        var selectedType = (NodeType) dialogueNode.nodeTypeSelectedIndex;
        
        dialogueNode.isLocalizable = EditorGUILayout.Toggle("Is Localizable", dialogueNode.isLocalizable);
        
        switch (selectedType)
        {
            case NodeType.Speak:
                if (dialogueNode.isLocalizable)
                {
                    var grid = CSVReader.ReadCSV(manager.StringTables[manager.currentLanguage]);
                    var keys = new List<string>();
                    for (var i = 0; i < grid.GetLength(0) - 1; i++)
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
                break;
            case NodeType.Choose:
                if (GUILayout.Button("New Option"))
                {
                    dialogueNode.optionNodes.Add(LocalizationManager.Instance.BaseDialogueNode);
                }

                for (var i = 0; i < dialogueNode.optionNodes.Count; i++)
                {
                    EditorGUILayout.BeginVertical(EditorStyles.helpBox);
                    GUI.backgroundColor = Color.red;
                    var optionField = EditorGUILayout.ObjectField(dialogueNode.optionNodes[i], typeof(ScriptableObject), false);
                    dialogueNode.optionNodes[i] = (DialogueNode)optionField;
                    if (GUILayout.Button("Delete Option"))
                    {
                        dialogueNode.optionNodes.RemoveAt(i);
                    }
                    EditorGUILayout.EndVertical();
                }

                break;
            case NodeType.Option:
                if (dialogueNode.isLocalizable)
                {
                    var grid = CSVReader.ReadCSV(manager.StringTables[manager.currentLanguage]);
                    var keys = new List<string>();
                    for (var i = 0; i < grid.GetLength(0) - 1; i++)
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
                break;
            default:
                Debug.LogError("DIALOGUE SYSTEM: Node type not implemented.");
                break;
        }

        //DrawDefaultInspector();
        
        EditorGUILayout.LabelField("");
        EditorGUILayout.LabelField("type index: " + dialogueNode.nodeTypeSelectedIndex);
        EditorGUILayout.LabelField("text field: " + dialogueNode.textField);
        EditorGUILayout.LabelField("text field index: " + dialogueNode.serializedTextSpeechSelectedIndex);
    }
}
