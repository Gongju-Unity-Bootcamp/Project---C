using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.UIElements;
using Util;

public class ResourceManager : MonoBehaviour
{

    public Dictionary<string, GameObject> Prefabs { get; private set; }
    public Dictionary<string, AudioClip> AudioClips { get; private set; }
    public Dictionary<string, AnimationClip> AnimClips { get; private set; }
    public Dictionary<string, Sprite> Sprite { get; private set; }

    public void Init()
    {
        Prefabs = new Dictionary<string, GameObject>();
        AudioClips = new Dictionary<string, AudioClip>();
        AnimClips = new Dictionary<string, AnimationClip>();
        Sprite = new Dictionary<string, Sprite>();
    }

    //�ٸ� Ŭ�������� ������Ʈ ��û�ϴ� �޼ҵ�
    public GameObject LoadPrefab(string path) => Load(Prefabs, string.Concat(Define.Path.PREFAB, path));
    public AudioClip LoadAudioClips(string path) => Load(AudioClips, string.Concat(Define.Path.AUDIOCLIP, path));
    public Sprite LoadSprite(string path) => Load(Sprite, string.Concat(Define.Path.SPRITE, path));
    public AnimationClip LoadAnimClips(string path) => Load(AnimClips, string.Concat(Define.Path.ANIM, path));


    //�� Ÿ�Կ� �´� ��ųʸ��� ��ȸ�ؼ� ��û���� ������Ʈ�� ������ �װ� ��ȯ�ϰ�
    //��ųʸ� �ȿ� ������ ������Ų ������Ʈ�� �ش�Ÿ���� ��ųʸ��� ����.
    private T Load<T>(Dictionary<string, T> dic, string path) where T : Object
    {
        Debug.Log("Load");
        Debug.Log(Define.Path.AUDIOCLIP.ToString());
        if (false == dic.ContainsKey(path)) 
        {
            T resource = Resources.Load<T>(path);
            dic.Add(path, resource);
            return dic[path];
        }
        return dic[path];
    }


    //���� ������Ʈ ���� ���������ִ� �޼ҵ�
    public GameObject Instantiate(string path, Transform parent = null)
    {
        GameObject prefab = LoadPrefab(path);

        Debug.Assert(prefab != null);

        return Instantiate(prefab, parent);
    }

    public GameObject Instantiate(GameObject prefab, Transform parent = null)
    {
        GameObject go = Object.Instantiate(prefab, parent); 

        go.name = prefab.name;

        return go;
    }


    public GameObject Instantiate(GameObject prefab, Vector3? position = null)
    {
        
        GameObject go = Object.Instantiate(prefab, (Vector3)position, Quaternion.identity);

        go.name = prefab.name;

        return go;
    }
}
