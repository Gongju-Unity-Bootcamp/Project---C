using CsvHelper;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Linq;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public Dictionary<ItemID, ItemData> Item { get; private set; }
    public Dictionary<IsaacID, IsaacData> Isaac { get; private set; }
    public Dictionary<SoundID, SoundData> Sound { get; private set; }

    private void Awake()
    {
        Init();
    }

    public void Init()
    {
#if UNITY_EDITOR
        Item = ParseToDict<ItemID, ItemData>("Assets/Resources/Data/Item.csv", data => data.Id);
        Sound = ParseToDict<SoundID, SoundData>("Assets/Resources/Data/Sound.csv", data => data.Id);
#else
        TextAsset itemCSV = Resources.Load<TextAsset>("Data/Item");
        Debug.Log(itemCSV);
        Item = ParseToDict<ItemID, ItemData>(itemCSV.text, data => data.Id);

        TextAsset soundCSV = Resources.Load<TextAsset>("Data/Sound");
        Debug.Log(soundCSV);
        Sound = ParseToDict<SoundID, SoundData>(soundCSV.text, data => data.Id);
#endif

    }

    private Dictionary<TKey, TItem> ParseToDict<TKey, TItem>([NotNull] string path, Func<TItem, TKey> KeySelector)
    {
        string fullPath = path;
#if UNITY_EDITOR
        using (var reader = new StreamReader(fullPath))
#else
        using (var reader = new StringReader(fullPath))
#endif
        using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
        {
            return csv.GetRecords<TItem>().ToDictionary(KeySelector);
        }
    }
}