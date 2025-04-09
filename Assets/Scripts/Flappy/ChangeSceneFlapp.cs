using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneFlapp : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        PlayerPrefs.SetInt("Scene", 4);
        PlayerPrefs.Save();
        SceneManager.LoadScene("RunOnRoad");
    }
}
