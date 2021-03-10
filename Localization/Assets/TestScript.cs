using System;
using System.Collections;
using System.Linq;
using DialogueSystem;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class TestScript : MonoBehaviour
{
    public Text localTextField;
    
    public Dialogue a;

    private IEnumerator Start()
    {
        for (var i = 0; i < a.dialogueSequence.Count; i++)
        {
            localTextField.text = "";
            var dialogue = a.dialogueSequence[i];
            switch (dialogue.TypeOfNode)
            {
                case NodeType.Speak:
                    localTextField.text = dialogue.TextField;
                    break;
                case NodeType.Choose:
                    var t = "";
                    foreach (var option in dialogue.optionNodes)
                    {
                        t += "> " + option.TextField + "\n";
                    }
                    localTextField.text = t;
                    i = a.optionIndexers[i].indexes[0] - 1;
                    break;
                case NodeType.Option:
                    break;
                default:
                    break;
            }

            yield return null;
            while (!Input.GetKeyDown(KeyCode.Space))
                yield return null;
        }
        while (!Input.GetKeyDown(KeyCode.Space))
            yield return null;
        localTextField.text = "";
    }

    [ContextMenu("0")]
    private void a0()
    {
        LocalizationManager.ChangeGameLanguage(0);
    }
    
    [ContextMenu("1")]
    private void a1()
    {
        LocalizationManager.ChangeGameLanguage(1);
    }
    
    [ContextMenu("2")]
    private void a2()
    {
        LocalizationManager.ChangeGameLanguage(2);
    }
}