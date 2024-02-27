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
        // Item 데이터 로드
        string itemPath = Path.Combine(Application.streamingAssetsPath, "Data", "Item.csv");
        Item = ParseToDict<ItemID, ItemData>(itemPath, data => data.Id);

        // Sound 데이터 로드
        string soundPath = Path.Combine(Application.streamingAssetsPath, "Data", "Sound.csv");
        Sound = ParseToDict<SoundID, SoundData>(soundPath, data => data.Id);

        Debug.Log("파싱완료");
    }

    private Dictionary<TKey, TItem> ParseToDict<TKey, TItem>([NotNull] string path, Func<TItem, TKey> KeySelector)
    {
        // 에디터에서 실행 중이면 StreamingAssetsPath를 사용하여 읽고, 그 외에는 Application.dataPath를 사용
#if UNITY_EDITOR
        string fullPath = path;
#else
        string fullPath = Path.Combine(Application.dataPath, "StreamingAssets", "Data", Path.GetFileName(path));
#endif

        using (var reader = new StreamReader(fullPath))
        using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
        {
            return csv.GetRecords<TItem>().ToDictionary(KeySelector);
        }
    }
}
