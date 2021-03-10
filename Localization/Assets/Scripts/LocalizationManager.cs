using System;
using System.Linq;
using RotaryHeart.Lib.SerializableDictionary;
using UnityEngine;

[CreateAssetMenu(fileName = "LocalizationManager", menuName = "Localization/Localization Manager")]
public class LocalizationManager : SingletonScriptableObject<LocalizationManager>
{
    public static Action OnChangeLanguage;
    
    public DictionaryStringTextAsset StringTables;

    [HideInInspector] public string currentLanguage;

    public string CurrentLanguage
    {
        get
        {
            currentLanguage = StringTables.Keys.ToArray()[currentLanguageSelectedIndex];
            return currentLanguage;
        }
    }

    #region Setting

    [HideInInspector] public int currentLanguageSelectedIndex;

    #endregion
    
    public static LocalizationManager GetInstance()
    {
        return GetInstance("LocalizationManager");
    }

    public static void ChangeGameLanguage(int languageCodeIndex)
    {
        GetInstance().currentLanguageSelectedIndex = languageCodeIndex;
        GetInstance().currentLanguage = GetInstance().StringTables.Keys.ToArray()[languageCodeIndex];
        OnChangeLanguage?.Invoke();
    }
}
