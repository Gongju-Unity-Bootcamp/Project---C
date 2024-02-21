using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using Util;

public class ResourceManager
{

    public Dictionary<string, GameObject> Prefabs { get; private set; }
    public Dictionary<string, AudioClip> AudioClips { get; private set; }
    public Dictionary<string, AnimationClip> AnimClips { get; private set; }

    public void Init()
    {
        Prefabs = new Dictionary<string, GameObject>();
        AudioClips = new Dictionary<string, AudioClip>();
        AnimClips = new Dictionary<string, AnimationClip>();
    }

    //다른 클래스에서 오브젝트 요청하는 메소드
    public GameObject LoadPrefab(string path) => Load(Prefabs, string.Concat(Define.Path.PREFAB, path));
    public AudioClip LoadAudioClips(string path) => Load(AudioClips, string.Concat(Define.Path.AUDIOCLIP, path));
    public AnimationClip LoadAnimClips(string path) => Load(AnimClips, string.Concat(Define.Path.ANIM, path));


    //각 타입에 맞는 딕셔너리를 순회해서 요청들어온 오브젝트가 있으면 그걸 반환하고
    //딕셔너리 안에 없으면 생성시킨 오브젝트를 해당타입의 딕셔너리에 넣음.
    private T Load<T>(Dictionary<string, T> dic, string path) where T : Object
    {
        if (false == dic.ContainsKey(path)) 
        { 
            T resource = Resources.Load<T>(path);
            dic.Add(path, resource);
            return dic[path];
        }
        return dic[path];
    }


    //게임 오브젝트 새로 생성시켜주는 메소드
    public GameObject Instantiate(string path, Transform parent = null)
    {
        GameObject prefab = LoadPrefab(path);

        Debug.Assert(prefab != null);

        return Instantiate(prefab, parent);
    }

    public GameObject Instantiate(GameObject prefab, Transform parent = null)
    {
        GameObject go = Object.Instantiate(prefab, parent); return go;

        go.name = prefab.name;

        return go;
    }
}
