using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchBarrier : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Игрок столкнулся с объектом!");
            Time.timeScale = 0;
        }
    }
}
