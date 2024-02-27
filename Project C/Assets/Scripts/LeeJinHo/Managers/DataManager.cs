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
        // Item ������ �ε�
        string itemPath = Path.Combine(Application.streamingAssetsPath, "Data", "Item.csv");
        Item = ParseToDict<ItemID, ItemData>(itemPath, data => data.Id);

        // Sound ������ �ε�
        string soundPath = Path.Combine(Application.streamingAssetsPath, "Data", "Sound.csv");
        Sound = ParseToDict<SoundID, SoundData>(soundPath, data => data.Id);

        Debug.Log("�Ľ̿Ϸ�");
    }

    private Dictionary<TKey, TItem> ParseToDict<TKey, TItem>([NotNull] string path, Func<TItem, TKey> KeySelector)
    {
        // �����Ϳ��� ���� ���̸� StreamingAssetsPath�� ����Ͽ� �а�, �� �ܿ��� Application.dataPath�� ���
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
