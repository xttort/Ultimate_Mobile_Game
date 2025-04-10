using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dust : MonoBehaviour
{
    public AudioClip takeC;
    private void OnDestroy()
    {
        //AudioSource takeNew = Instantiate(take);
        //takeNew.transform.position = 
        AudioSource.PlayClipAtPoint(takeC, transform.position, 1f);
    }

}
