using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChooseScenes : MonoBehaviour
{
    private int scene;
    // Start is called before the first frame update
    void Start()
    {
        scene = PlayerPrefs.GetInt("Scene", 1);
    }

    public void ChooseScene()
    {
        if (scene == 1 || scene == 2 || scene == 4 || scene == 5)
            SceneManager.LoadScene("RunOnRoad");
        else if (scene == 3)
            SceneManager.LoadScene("FlappyBirds");
        else if (scene == 6)
            SceneManager.LoadScene("FruitNinja");
    }
}
