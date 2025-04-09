using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuStart : MonoBehaviour
{
    public float timeWait = 15f;

    void Update()
    {
        if(timeWait <= 0)
        {
            SceneManager.LoadScene("Main");
        }
        timeWait -= Time.deltaTime;
    }
}
