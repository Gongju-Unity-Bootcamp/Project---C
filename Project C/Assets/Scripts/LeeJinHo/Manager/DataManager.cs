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
    public Dictionary<IsaacID, IsaacData> Isaac { get; private set; } //플레이어 데이터
    //public Dictionary<SoundID, SoundData> Sound { get; private set; } 
    //아이템 테이블 번호를 저장하는 테이블 추가

    public void Init()
    {
        Item = ParseToDict<ItemID, ItemData>("Assets/Resource/Data/Item.csv", data => data.ID);
        Isaac = ParseToDict<IsaacID, IsaacData>("Assets/Resource/Data/Isaac.csv", data => data.ID);
        //Sound = ParseToDict<SoundID, SoundData>("Assets/Resource/Data/Sound", data => data.ID);
    }

    private Dictionary<TKey, TItem> ParseToDict<TKey, TItem>([NotNull] string path,Func<TItem, TKey> KeySelector)
    {
        using var reader = new StreamReader(path);

        using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

        return csv.GetRecords<TItem>().ToDictionary(KeySelector);
    }
}
