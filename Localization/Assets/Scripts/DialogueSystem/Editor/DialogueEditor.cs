using System;
using System.Collections.Generic;
using DialogueSystem;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Dialogue))]
public class DialogueEditor : Editor
{
    private Texture breakSign;
    private Texture downArrow;
    
    private void Awake()
    {
        breakSign = Resources.Load<Texture>("Sprites/breakSign");
        downArrow = Resources.Load<Texture>("Sprites/downArrow");
    }

    public override void OnInspectorGUI()
    {
        var dialogue = (Dialogue) target;
        var manager = LocalizationManager.Instance;
        if (manager == null)
        {
            EditorGUILayout.HelpBox("SETUP THE LOCALIZATION MANAGER BEFORE START", MessageType.Error);
            return;
        }
        
        var style = new GUIStyle(GUI.skin.label) {alignment = TextAnchor.MiddleCenter};

        EditorGUILayout.BeginVertical(EditorStyles.helpBox);
        if (GUILayout.Button("New Node"))
        {
            dialogue.dialogueSequence.Add(dialogue.dialogueSequence.Count > 0 ? dialogue.dialogueSequence[dialogue.dialogueSequence.Count - 1] : LocalizationManager.Instance.BaseDialogueNode);
        }

        for (var i = 0; i < dialogue.dialogueSequence.Count; i++)
        {
            if (dialogue.dialogueSequence == null) break;
            var nodeType = ((NodeType) dialogue.dialogueSequence[i].nodeTypeSelectedIndex).ToString();
            switch (((NodeType) dialogue.dialogueSequence[i].nodeTypeSelectedIndex))
            {
                case NodeType.Speak:
                    GUI.backgroundColor = new Color(0.25f, 1f, 0f);
                    break;
                case NodeType.Choose:
                    GUI.backgroundColor = new Color(0f, 0.09f, 1f);
                    break;
                case NodeType.Option:
                    GUI.backgroundColor = new Color(0.39f, 0.77f, 1f);
                    break;
            }

            EditorGUILayout.BeginVertical(EditorStyles.helpBox);

            EditorGUILayout.HelpBox("NODE ID: " + i, MessageType.None);
            EditorGUILayout.LabelField(nodeType, style, GUILayout.ExpandWidth(true));
            var dialogueScriptableObjectCastField =
                (DialogueNode) EditorGUILayout.ObjectField(dialogue.dialogueSequence[i], typeof(ScriptableObject),
                    false);
            dialogue.dialogueSequence[i] = dialogueScriptableObjectCastField;

            switch (((NodeType) dialogue.dialogueSequence[i].nodeTypeSelectedIndex))
            {
                case NodeType.Speak:
                    EditorGUILayout.HelpBox(dialogue.dialogueSequence[i].TextField, MessageType.Info);
                    break;
                case NodeType.Choose:
                    if (dialogue.dialogueSequence[i].optionNodes.Count > 0)
                    {
                        for (var j = 0; j < dialogue.dialogueSequence[i].optionNodes.Count; j++)
                        {
                            var option = dialogue.dialogueSequence[i].optionNodes[j];
                            EditorGUILayout.BeginVertical();
                            GUI.backgroundColor = Color.red;
                            EditorGUILayout.HelpBox("(ID: " + j + ") - " + option.TextField, MessageType.None);
                            EditorGUILayout.EndVertical();
                        }
                    }
                    else
                    {
                        EditorGUILayout.HelpBox("YOU NEED TO INSERT OPTIONS IN THE CHOOSE NODE.", MessageType.Error);
                    }

                    break;
                case NodeType.Option:
                    break;
                default:
                    Debug.LogError("DIALOGUE SYSTEM: Node type may not exist.");
                    break;
            }

            EditorGUILayout.LabelField("");
            GUI.backgroundColor = Color.red;
            if (GUILayout.Button("Remove Node"))
            {
                dialogue.dialogueSequence.RemoveAt(i);
            }

            EditorGUILayout.EndVertical();
            if (i < dialogue.dialogueSequence.Count - 1)
            {
                switch (((NodeType) dialogue.dialogueSequence[i].nodeTypeSelectedIndex))
                {
                    case NodeType.Speak:
                        GUILayout.Box(downArrow, style);
                        break;
                    case NodeType.Choose:
                        GUILayout.Box(breakSign, style);
                        break;
                    case NodeType.Option:
                        break;
                }
            }
        }
        
        EditorGUILayout.EndVertical();
    }
}