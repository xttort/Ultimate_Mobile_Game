using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    private int scene;

    private void OnTriggerEnter(Collider other)
    {
        scene = PlayerPrefs.GetInt("Scene");
        Time.timeScale = 0f;
        if(scene == 2)
        {
            PlayerPrefs.SetInt("Scene", 3);
            PlayerPrefs.Save();
            SceneManager.LoadScene("FlappyBirds");
        }
        else if (scene == 5)
        {
            PlayerPrefs.SetInt("Scene", 6);
            PlayerPrefs.Save();
            SceneManager.LoadScene("FruitNinja");
        }
    }
}
