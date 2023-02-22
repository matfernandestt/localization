using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LocalizableText : MonoBehaviour
{
    [HideInInspector] public int selectedTextIndex;

    private void Awake()
    {
        UpdateText();
        LocalizationManager.OnChangeLanguage += UpdateText;
    }

    private void OnDestroy()
    {
        LocalizationManager.OnChangeLanguage -= UpdateText;
    }

    private void UpdateText()
    {
        var manager = LocalizationManager.GetInstance();
        var grid = CSVReader.ReadCSV(manager.StringTables[manager.CurrentLanguage]);
        var keys = new List<string>();
        for (var i = 0; i < grid.GetLength(1) - 1; i++)
        {
            keys.Add(grid[0, i]);
        }
        SetText(grid[1, selectedTextIndex]);
    }

    public void SetText(string text)
    {
        var textComponent = GetComponent<Text>();
        if (textComponent != null)
        {
            textComponent.text = text;
        }
        var textMeshComponent = GetComponent<TextMeshProUGUI>();
        if (textMeshComponent != null)
        {
            textMeshComponent.text = text;
        }
    }
}
