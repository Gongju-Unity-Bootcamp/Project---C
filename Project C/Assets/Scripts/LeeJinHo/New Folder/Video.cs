using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class Video : MonoBehaviour
{
    public VideoPlayer vidioPlayer;
    // Start is called before the first frame update
    void Start()
    {
        vidioPlayer = GetComponent<VideoPlayer>();
        vidioPlayer.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
