using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Manager : MonoBehaviour
{
    private static Manager s_instance;
    public static DataManager Data { get; private set; }
    public static UIManager UI { get; private set; }
    public static ItemManager Item { get; private set; }
    public static SoundManager Sound { get; private set; }
    public static DungeonManager Dunjeon {  get; private set; } 
    public static SpawnManager Spawn { get; private set; }
    public static ResourceManager Resource {  get; private set; }

    private void Awake()
    {
        if (s_instance == null) 
        { 
            s_instance = this; 
            DontDestroyOnLoad(this); 
        }
        else 
            { Destroy(gameObject); }

        Data = new DataManager();
        UI = new UIManager();
        Item = new ItemManager();
        Resource = new ResourceManager();

        GameObject go;

        go = new GameObject(nameof(SoundManager));
        go.transform.parent = transform;
        Sound = go.GetComponent<SoundManager>();

        go = new GameObject(nameof(DungeonManager));
        go.transform.parent = transform;
        Dunjeon = go.GetComponent<DungeonManager>();

        go = new GameObject(nameof(SpawnManager));
        go.transform.parent = transform;
        Spawn = go.GetComponent<SpawnManager>();

        Data.Init();
        UI.Init();
        Item.Init();
        Sound.Init();
        Dunjeon.Init();
        Spawn.Init();
        Resource.Init();

    }
}
