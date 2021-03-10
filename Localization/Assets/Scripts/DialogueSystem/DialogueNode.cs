using System.Collections.Generic;
using DialogueSystem;
using UnityEngine;

[CreateAssetMenu(fileName = "DialogueNode", menuName = "Dialogue System/Dialogue Node")]
public class DialogueNode : ScriptableObject
{
    [HideInInspector] public bool isLocalizable;

    #region Speech Variables
    [HideInInspector] public string textField;
    [HideInInspector] public AudioClip waveField;

    public string TextField
    {
        get
        {
            if (isLocalizable)
            {
                var manager = LocalizationManager.GetInstance();
                var grid = CSVReader.ReadCSV(manager.StringTables[manager.CurrentLanguage]);
                var keys = new List<string>();
                for (var i = 0; i < grid.GetLength(1) - 1; i++)
                {
                    keys.Add(grid[0, i]);
                }
                textField = grid[1, serializedTextSpeechSelectedIndex];
            }
            return textField;
        }
    }

    public NodeType TypeOfNode => (NodeType) nodeTypeSelectedIndex;

    #endregion
    
    #region Choose
    [HideInInspector] public List<DialogueNode> optionNodes;
    #endregion

    #region Setting
    [HideInInspector] public int nodeTypeSelectedIndex;
    [HideInInspector] public int serializedTextSpeechSelectedIndex;

    #endregion
}
