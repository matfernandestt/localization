using System.Linq;
using RotaryHeart.Lib.SerializableDictionary;
using UnityEngine;

[CreateAssetMenu(fileName = "LocalizationManager", menuName = "Localization/Localization Manager")]
public class LocalizationManager : SingletonScriptableObject<LocalizationManager>
{
    public DictionaryStringTextAsset StringTables;
    public DialogueNode BaseDialogueNode;

    [HideInInspector] public string currentLanguage;

    #region Setting

    [HideInInspector] public int currentLanguageSelectedIndex;

    #endregion

    public static void ChangeGameLanguage(int languageCodeIndex)
    {
        Instance.currentLanguageSelectedIndex = languageCodeIndex;
        Instance.currentLanguage = Instance.StringTables.Keys.ToArray()[languageCodeIndex];
    }
}
