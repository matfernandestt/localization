using System;
using System.Collections.Generic;
using RotaryHeart.Lib.SerializableDictionary;
using UnityEngine;

[Serializable]
public class DictionaryStringTextAsset : SerializableDictionaryBase<string, TextAsset> { }

[Serializable]
public class DictionaryIntOptionIndexer : SerializableDictionaryBase<int, OptionIndexer> { }

[Serializable]
public class OptionIndexer
{
    public int[] indexes;
}