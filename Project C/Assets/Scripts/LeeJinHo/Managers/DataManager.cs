using CsvHelper;
using System;
using System.Collections.Generic;
//using System.Diagnostics;
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
        Item = ParseToDict<ItemID, ItemData>("Assets/Resources/Data/Item.csv", data => data.Id);
        //Isaac = ParseToDict<IsaacID, IsaacData>("Assets/Resources/Data/Isaac.csv", data => data.ID);
        Sound = ParseToDict<SoundID, SoundData>("Assets/Resources/Data/Sound.csv", data => data.Id);
        Debug.Log("�Ľ̿Ϸ�");
    }

    private Dictionary<TKey, TItem> ParseToDict<TKey, TItem>([NotNull] string path,Func<TItem, TKey> KeySelector)
    {
        using var reader = new StreamReader(path);

        using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

        return csv.GetRecords<TItem>().ToDictionary(KeySelector);
    }
}
