using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PreloadManager : MonoBehaviour
{

    public int scene;
    public GameObject player;
    public Animator animPlayer;
    Player scriptPlayer;
    PlayerTimer scriptPlayerTimer;
    public float time = 58.1f;
    public AudioClip[] soundClips;
    public AudioClip[] music;
    public AudioSource PlayerAudioSource;
    public AudioSource mainSource;


    private void Awake()
    {
        Time.timeScale = 0f;
        scene = PlayerPrefs.GetInt("Scene", 1);
        scriptPlayer = player.GetComponent<Player>();
        scriptPlayerTimer = player.GetComponent<PlayerTimer>();
        //SceneManager.SetActiveScene(SceneManager.GetSceneByName("RunOnRoad"));

    }
    void Start()
    {
        //SceneManager.SetActiveScene(SceneManager.GetSceneByName("RunOnRoad"));
        Time.timeScale = 1f;
        switch (scene)
        {
            case 1:
                {
                    PlayerAudioSource.Stop();
                    PlayerAudioSource.clip = music[0];
                    PlayerAudioSource.Play();
                    player.transform.position = new Vector3(-37f, 1f, -32.50065f); //установка позиции
                    StayPlayer();
                    RenderSettings.fogColor = new Color32(40, 66, 188, 255); //цвет тумана
                    time = 58.1f;
                    break;
                }
            case 2:
                {
                    PlayerAudioSource.Stop();
                    PlayerAudioSource.clip = music[1];
                    PlayerAudioSource.Play();
                    player.transform.position = new Vector3(-37f, 1f, -32.50065f); //установка позиции
                    RunPlayer();
                    RenderSettings.fogColor = new Color32(40, 66, 188, 255); //цвет тумана
                    break;
                }
            case 4:
                {
                    PlayerAudioSource.Stop();
                    PlayerAudioSource.clip = music[2];
                    PlayerAudioSource.Play();
                    player.transform.position = new Vector3(-37f, 1f, 1016f); //установка позиции
                    StayPlayer();
                    RenderSettings.fogColor = new Color32(255, 215, 71, 255); //цвет тумана
                    time = 58.1f;
                    break;
                }
            case 5:
                {
                    PlayerAudioSource.Stop();
                    PlayerAudioSource.clip = music[3];
                    PlayerAudioSource.Play();
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
            if(TimeOut() && PlayerAudioSource.clip == music[0])
            {
                PlayerAudioSource.Stop();
                PlayerAudioSource.clip = music[1];
                PlayerAudioSource.Play();
                PlayerPrefs.SetInt("Scene", 2);
                PlayerPrefs.Save();
                RunPlayer();
                Debug.Log("!");

            }
        }
        else if(scene == 4)
        {
            if (TimeOut() && PlayerAudioSource.clip == music[2])
            {
                PlayerAudioSource.Stop();
                PlayerAudioSource.clip = music[3];
                PlayerAudioSource.Play();
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
