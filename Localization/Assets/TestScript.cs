using System;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    public Dialogue a;
    
    private void Update()
    {
        Debug.Log(LocalizationManager.Instance.currentLanguage + " --- " + a.dialogueSequence[0].TextField);
    }

    [ContextMenu("1")]
    private void assa1()
    {
        LocalizationManager.ChangeGameLanguage(0);
    }
    
    [ContextMenu("2")]
    private void ass2a()
    {
        LocalizationManager.ChangeGameLanguage(1);
    }
    
    [ContextMenu("3")]
    private void assa3()
    {
        LocalizationManager.ChangeGameLanguage(2);
    }
}