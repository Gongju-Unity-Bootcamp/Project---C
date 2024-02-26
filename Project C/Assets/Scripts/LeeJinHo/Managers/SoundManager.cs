using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    [SerializeField] public AudioSource BGM;
    [SerializeField] private AudioSource SoundEffect;
    [SerializeField] private AudioListener Slistenert;

    public void Init()
    {
        BGM = gameObject.AddComponent<AudioSource>();
        SoundEffect = gameObject.AddComponent<AudioSource>();
        Slistenert = gameObject.AddComponent<AudioListener>();
        MugicQ();
    }
    private void MugicQ()
    {
        string sd = "basementLoop";
        BGM.clip = Managers.Resource.LoadAudioClips("basementLoop");
        PlayBGM();
    }

    public void ChangeBGM(string BGMName)
    {
        BGM.clip = Managers.Resource.LoadAudioClips($"{BGMName}");
        PlayBGM();
    }

    public void EffectSoundChange(string sound)
    {
        SoundEffect.Stop();
        SoundEffect.clip = Managers.Resource.LoadAudioClips(sound);
        SoundEffect.Play();
    }

    public void StopBGM()
    {
        BGM.Stop();
    }

    public void PlayBGM()
    {
        BGM.Play();
    }

    public void SetBGMVolume(float volume)
    {
        float bgmVolume = Mathf.Clamp01(volume); // 볼륨이 0에서 1 사이에 있도록 보장
        BGM.volume = bgmVolume;
    }
}