using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public GameObject dregs;

    private void OnDestroy()
    {
        if(Application.isPlaying)
        {
            Debug.Log("Àå¾Ö¹° ÆÄ±«");
            GameObject obj = Instantiate(dregs, transform.position, Quaternion.identity) as GameObject;
        }

    }
}
