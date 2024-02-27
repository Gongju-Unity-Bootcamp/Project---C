using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class NextstageTest : MonoBehaviour
{

    public VideoPlayer vidioPlayer;
    private GameObject _UI;

    private GameObject child1;
    private GameObject child2;

    void Start()
    {
        vidioPlayer = GetComponent<VideoPlayer>();
        _UI = GameObject.Find("InGameCanvas");
        child1 = transform.Find("BasementDoorGetIn").gameObject;
        child2 = transform.Find("BasementDoorOpen").gameObject;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.SetActive(false);
            StartCoroutine(BossClearVidio());
        }
    }
    
    IEnumerator BossClearVidio()
    {
        child1.SetActive(false);
        child2.SetActive(true);
        yield return new WaitForSeconds(1.1f);

        _UI.SetActive(false);
        vidioPlayer.clip = Resources.Load<VideoClip>("AudioClip/Vidio_Stage1_Clear");
        vidioPlayer.renderMode = VideoRenderMode.CameraNearPlane;
        vidioPlayer.Play();

        Time.timeScale = 0f;

        while (vidioPlayer.isPlaying)
        {
            yield return null;

        }

        Application.Quit();
    }
}
