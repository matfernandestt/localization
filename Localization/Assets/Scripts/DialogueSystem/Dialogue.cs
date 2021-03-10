using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dialogue", menuName = "Dialogue System/Dialogue")]
public class Dialogue : ScriptableObject
{
    [HideInInspector] public List<DialogueNode> dialogueSequence;
    [HideInInspector] public DictionaryIntOptionIndexer optionIndexers;

    [ContextMenu("Clear Option Indexers")]
    public void ClearOptionIndexers()
    {
        optionIndexers.Clear();
    }
}
