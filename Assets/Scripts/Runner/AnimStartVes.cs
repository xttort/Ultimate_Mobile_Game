using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimStartVes : MonoBehaviour
{
    public Animator VesAnim;
    public AudioSource laught;
    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Player>())
        {
            VesAnim.SetTrigger("start");
            laught.Play();
        }
    }

}
