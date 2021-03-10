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
        var manager = LocalizationManager.GetInstance();
        if (manager == null)
        {
            EditorGUILayout.HelpBox("SETUP THE LOCALIZATION MANAGER BEFORE START", MessageType.Error);
            return;
        }
        
        var style = new GUIStyle(GUI.skin.label) {alignment = TextAnchor.MiddleCenter};

        EditorGUILayout.BeginVertical(EditorStyles.helpBox);
        if (GUILayout.Button("New Node"))
        {
            dialogue.dialogueSequence.Add(dialogue.dialogueSequence.Count > 0 ? dialogue.dialogueSequence[dialogue.dialogueSequence.Count - 1] : BaseNodeReferencer.GetInstance().baseNode);
            EditorUtility.SetDirty(dialogue);
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
            var dialogueScriptableObjectCastField = (DialogueNode) EditorGUILayout.ObjectField(dialogue.dialogueSequence[i], typeof(ScriptableObject), false);
            dialogue.dialogueSequence[i] = dialogueScriptableObjectCastField;

            switch (((NodeType) dialogue.dialogueSequence[i].nodeTypeSelectedIndex))
            {
                case NodeType.Speak:
                    EditorGUILayout.HelpBox(dialogue.dialogueSequence[i].TextField, MessageType.Info);
                    break;
                case NodeType.Choose:
                    if (dialogue.dialogueSequence[i].optionNodes.Count > 0)
                    {
                        if (!dialogue.optionIndexers.ContainsKey(i))
                        {
                            var optionIndexer = new OptionIndexer { indexes = new int[dialogue.dialogueSequence[i].optionNodes.Count] };
                            dialogue.optionIndexers.Add(i, optionIndexer);
                        }
                        for (var j = 0; j < dialogue.dialogueSequence[i].optionNodes.Count; j++)
                        {
                            var option = dialogue.dialogueSequence[i].optionNodes[j];
                            GUI.backgroundColor = Color.red;
                            EditorGUILayout.BeginHorizontal(EditorStyles.helpBox);
                            
                            EditorGUILayout.HelpBox("(ID:" + j + ")\n" + option.TextField, MessageType.None);

                            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
                            GUILayout.Label("Jump to node with ID");
                            dialogue.optionIndexers[i].indexes[j] = EditorGUILayout.IntField(dialogue.optionIndexers[i].indexes[j]);
                            EditorGUILayout.EndVertical();

                            EditorGUILayout.EndHorizontal();
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
                EditorUtility.SetDirty(dialogue);
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