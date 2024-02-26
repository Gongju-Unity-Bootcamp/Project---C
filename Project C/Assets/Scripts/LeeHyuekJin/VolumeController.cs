using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
     

public class VolumeController : MonoBehaviour
{
    public Sprite[] numbers;

    private Image BGMVolume;
    int volumeSize = 9;
    private void Awake()
    {
        BGMVolume = transform.GetChild(1).GetComponent<Image>();
    }
    public void UpBGMVolume()
    {
        if(volumeSize < numbers.Length - 1)
        {
            volumeSize++;
            BGMVolume.sprite = numbers[volumeSize];
            Managers.Sound.SetBGMVolume(volumeSize / 9.0f);
        }
    }

    public void DownBGMVolume()
    {
        if (volumeSize > 0)
        {
            volumeSize--;
            BGMVolume.sprite = numbers[volumeSize];
            Managers.Sound.SetBGMVolume(volumeSize / 9.0f);
        }
    }

    public void UpSFXVolume()
    {
        if (volumeSize < numbers.Length - 1)
        {
            volumeSize++;
            BGMVolume.sprite = numbers[volumeSize];
            Managers.Sound.SetSFXVolume(volumeSize / 9.0f);
        }
    }

    public void DownSFXVolume()
    {
        if (volumeSize > 0)
        {
            volumeSize--;
            BGMVolume.sprite = numbers[volumeSize];
            Managers.Sound.SetSFXVolume(volumeSize / 9.0f);
        }
    }
}
