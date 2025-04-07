using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class MenuSound : MonoBehaviour
{
    public static MenuSound Instance { get; private set; }

    [Header("UI Settings")]
    [SerializeField] private GameObject pauseMenuPanel;
    [SerializeField] private Button pauseButton;
    [SerializeField] private Button resumeButton;

    [Header("Audio Settings")]
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider masterVolumeSlider;
    [SerializeField] private Slider musicVolumeSlider;
    [SerializeField] private Slider sfxVolumeSlider;
    [SerializeField] private Toggle muteToggle;

    private bool isPaused = false;
    private bool isMuted = false;
    private float preMuteMasterVolume = 0.75f;
    private float preMuteMusicVolume = 0.75f;
    private float preMuteSFXVolume = 0.75f;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        InitializeButtons();
        InitializeAudioSettings();

        if (pauseMenuPanel != null)
            pauseMenuPanel.SetActive(false);
    }

    private void InitializeButtons()
    {
        if (pauseButton != null)
        {
            pauseButton.onClick.RemoveAllListeners();
            pauseButton.onClick.AddListener(TogglePause);
            pauseButton.gameObject.SetActive(true);
        }

        if (resumeButton != null)
        {
            resumeButton.onClick.RemoveAllListeners();
            resumeButton.onClick.AddListener(TogglePause);
        }
    }

    private void InitializeAudioSettings()
    {
        // Загрузка сохраненных настроек
        LoadAudioSettings();

        // Настройка обработчиков событий
        if (masterVolumeSlider != null)
            masterVolumeSlider.onValueChanged.AddListener(SetMasterVolume);

        if (musicVolumeSlider != null)
            musicVolumeSlider.onValueChanged.AddListener(SetMusicVolume);

        if (sfxVolumeSlider != null)
            sfxVolumeSlider.onValueChanged.AddListener(SetSFXVolume);

        if (muteToggle != null)
            muteToggle.onValueChanged.AddListener(ToggleMute);
    }

    private void LoadAudioSettings()
    {
        // Загрузка сохраненных значений или установка значений по умолчанию
        float masterVolume = PlayerPrefs.GetFloat("MasterVolume", 0.75f);
        float musicVolume = PlayerPrefs.GetFloat("MusicVolume", 0.75f);
        float sfxVolume = PlayerPrefs.GetFloat("SFXVolume", 0.75f);
        isMuted = PlayerPrefs.GetInt("IsMuted", 0) == 1;

        // Применение значений к слайдерам
        if (masterVolumeSlider != null)
            masterVolumeSlider.value = masterVolume;

        if (musicVolumeSlider != null)
            musicVolumeSlider.value = musicVolume;

        if (sfxVolumeSlider != null)
            sfxVolumeSlider.value = sfxVolume;

        if (muteToggle != null)
            muteToggle.isOn = isMuted;

        // Применение значений к аудиомикшеру
        if (isMuted)
        {
            MuteAllSounds();
        }
        else
        {
            SetMasterVolume(masterVolume);
            SetMusicVolume(musicVolume);
            SetSFXVolume(sfxVolume);
        }
    }

    public void SetMasterVolume(float volume)
    {
        // Если звук включен, но громкость ушла в 0 - включаем mute
        if (!isMuted && volume <= 0.001f)
        {
            muteToggle.isOn = true;
            return;
        }

        // Если звук был выключен, но громкость увеличили - выключаем mute
        if (isMuted && volume > 0.001f)
        {
            muteToggle.isOn = false;
            return;
        }

        if (isMuted) return;

        audioMixer.SetFloat("MasterVolume", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("MasterVolume", volume);
        preMuteMasterVolume = volume;
    }

    public void SetMusicVolume(float volume)
    {
        if (isMuted) return;

        audioMixer.SetFloat("MusicVolume", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("MusicVolume", volume);
        preMuteMusicVolume = volume;
    }

    public void SetSFXVolume(float volume)
    {
        if (isMuted) return;

        audioMixer.SetFloat("SFXVolume", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("SFXVolume", volume);
        preMuteSFXVolume = volume;
    }

    public void ToggleMute(bool mute)
    {
        isMuted = mute;
        PlayerPrefs.SetInt("IsMuted", mute ? 1 : 0);

        if (mute)
        {
            // Сохраняем текущие значения громкости
            audioMixer.GetFloat("MasterVolume", out float currentMasterVol);
            audioMixer.GetFloat("MusicVolume", out float currentMusicVol);
            audioMixer.GetFloat("SFXVolume", out float currentSFXVol);

            preMuteMasterVolume = Mathf.Pow(10, currentMasterVol / 20);
            preMuteMusicVolume = Mathf.Pow(10, currentMusicVol / 20);
            preMuteSFXVolume = Mathf.Pow(10, currentSFXVol / 20);

            MuteAllSounds();
        }
        else
        {
            UnmuteAllSounds();
        }
    }

    private void MuteAllSounds()
    {
        audioMixer.SetFloat("MasterVolume", -80f);
        audioMixer.SetFloat("MusicVolume", -80f);
        audioMixer.SetFloat("SFXVolume", -80f);

        // Не меняем значения слайдеров при mute, только визуально
        if (muteToggle != null)
            muteToggle.isOn = true;
    }

    private void UnmuteAllSounds()
    {
        SetMasterVolume(preMuteMasterVolume);
        SetMusicVolume(preMuteMusicVolume);
        SetSFXVolume(preMuteSFXVolume);

        if (muteToggle != null)
            muteToggle.isOn = false;
    }

    public void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
            PauseGame();
        else
            ResumeGame();
    }

    private void PauseGame()
    {
        if (pauseMenuPanel == null) return;

        isPaused = true;
        Time.timeScale = 0f;
        pauseMenuPanel.SetActive(true);

        if (pauseButton != null)
            pauseButton.gameObject.SetActive(false);
    }

    private void ResumeGame()
    {
        if (pauseMenuPanel == null) return;

        isPaused = false;
        Time.timeScale = 1f;
        pauseMenuPanel.SetActive(false);

        if (pauseButton != null)
            pauseButton.gameObject.SetActive(true);
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        ResumeGame();
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.Save();
    }
}