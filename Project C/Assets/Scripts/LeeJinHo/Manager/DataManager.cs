using CsvHelper;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Linq;

public class DataManager
{
    public Dictionary<ItemID, ItemData> Item { get; private set; }
    //public Dictionary<IsaacID, IsaacData> Isaac { get; private set; }
    //public Dictionary<SoundID,SoundData> Sound { get; private set; }

    public void Init()
    {
        Item = ParseToDict<ItemID, ItemData>("Assets/Resource/Data/Item", data => data.ID);
        //Isaac = ParseToDict<ItemID, ItemData>("Assets/Resource/Data/Isaac", data => data.ID);
        //Sound = ParseToDict<ItemID, ItemData>("Assets/Resource/Data/Sound", data => data.ID);
    }

    private Dictionary<TKey, TItem> ParseToDict<TKey, TItem>([NotNull] string path,Func<TItem, TKey> KeySelector)
    {
        using var reader = new StreamReader(path);

        using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

        return csv.GetRecords<TItem>().ToDictionary(KeySelector);
    }
}
