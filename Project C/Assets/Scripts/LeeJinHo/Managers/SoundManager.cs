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
}