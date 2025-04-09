using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreloadManager : MonoBehaviour
{

    public int scene;
    public GameObject player;
    public Animator animPlayer;
    Player scriptPlayer;
    PlayerTimer scriptPlayerTimer;
    public float time = 5f;
    public AudioClip[] soundClips;
    public AudioSource mainSource;


    private void Awake()
    {
        Time.timeScale = 0f;
        scene = PlayerPrefs.GetInt("Scene", 1);
        scriptPlayer = player.GetComponent<Player>();
        scriptPlayerTimer = player.GetComponent<PlayerTimer>();

    }
    void Start()
    {
        Time.timeScale = 1f;
        switch (scene)
        {
            case 1:
                {
                    player.transform.position = new Vector3(-37f, 1f, -32.50065f); //установка позиции
                    StayPlayer();
                    RenderSettings.fogColor = new Color32(71, 215, 255, 255); //цвет тумана
                    time = 5f;
                    break;
                }
            case 2:
                {
                    player.transform.position = new Vector3(-37f, 1f, -32.50065f); //установка позиции
                    RunPlayer();
                    RenderSettings.fogColor = new Color32(71, 215, 255, 255); //цвет тумана
                    break;
                }
            case 4:
                {
                    player.transform.position = new Vector3(-37f, 1f, 1016f); //установка позиции
                    StayPlayer();
                    RenderSettings.fogColor = new Color32(255, 215, 71, 255); //цвет тумана
                    time = 5f;
                    break;
                }
            case 5:
                {
                    player.transform.position = new Vector3(-37f, 1f, 1016f); //установка позиции
                    RunPlayer();
                    RenderSettings.fogColor = new Color32(255, 215, 71, 255); //цвет тумана
                    break;
                }
        }
    }

    // Update is called once per frame
    void Update()
    {/* звук
        if (scene == 1)
            mainSource.PlayOneShot(soundClips[0]);
        else if (scene == 2)
        {
            mainSource.PlayOneShot(soundClips[1]);
        }
        else if (scene == 4)
            mainSource.PlayOneShot(soundClips[2]);
        else if (scene == 5)
            mainSource.PlayOneShot(soundClips[3]);
        */
        if (scene == 1)
        {
            if(TimeOut())
            {
                PlayerPrefs.SetInt("Scene", 2);
                PlayerPrefs.Save();
                RunPlayer();

            }
        }
        else if(scene == 4)
        {
            if (TimeOut())
            {
                PlayerPrefs.SetInt("Scene", 5);
                PlayerPrefs.Save();
                RunPlayer();
            }
        }
    }

    void StayPlayer()
    {
        animPlayer.SetBool("isActive", false);
        scriptPlayer.enabled = false; //выключение скриптов игрока
        scriptPlayerTimer.enabled = false; //выключение скриптов игрока таймера
    }

    void RunPlayer()
    {
        animPlayer.SetBool("isActive", true);
        scriptPlayer.enabled = true; //включение скриптов игрока
        scriptPlayerTimer.enabled = true; //включение скриптов игрока таймера
    }

    bool TimeOut()
    {
        if (time <= 0)
            return true;
        else
        {
            time -= Time.deltaTime;
            return false;
        }
    }
}
