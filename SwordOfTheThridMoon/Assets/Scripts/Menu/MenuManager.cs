using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MenuManager : MonoBehaviour
{
    [SerializeField] GameObject menuPanel;
    [SerializeField] GameObject settingsPanel;
    public float _currentSensitivity;
    private int _currentResolutionIndex;
    private bool _continue = false;

    [Header("Settings Menu")]
    [SerializeField] private AudioMixer _audioMixer;
    [SerializeField] private TMP_Dropdown _resolutionDropdown;
    [SerializeField] private TMP_Dropdown _qualityDropdown;
    [SerializeField] private Toggle _toggle;
    [SerializeField] private Slider _sensitivity;
    [SerializeField] private Slider _volume;
    [SerializeField] private GameObject _continueButton;
    private Resolution[] _resolutions;

    private void Awake()
    {
        _continue = ContinueData.boolContinue;
    }
    private void Start()
    {
        _continue = ContinueData.boolContinue;
        PopulateResolutionDropdown();
        //Menu();
        LoadSettings(_currentResolutionIndex);
        if (_continue)
        {
            _continueButton.SetActive(true);
        }
        else
        {
            _continueButton.SetActive(false);
        }
    }

    //Метод загрузки разных сцен
    public void OpenScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    //Выход из игры
    public void Quit()
    {
        Application.Quit();
    }

    //Кнопка игры
    public void Play()
    {
        OpenScene("GameScene");
        ContinueData.boolContinue = false;
    }

    //Открытие меню
    public void Menu()
    {
        settingsPanel.gameObject.SetActive(false);
        menuPanel.gameObject.SetActive(true);
    }

    //Сохранение настроек
    public void SaveSettings()
    {
        PlayerPrefs.SetInt("QualitySettings", _qualityDropdown.value);
        PlayerPrefs.SetInt("Resolution", _resolutionDropdown.value);

        int isOn = System.Convert.ToInt32(Screen.fullScreen);
        PlayerPrefs.SetInt("Fullscreen", isOn);

        float volume;
        float sensitivity = _sensitivity.value;
        _audioMixer.GetFloat("MasterVolume", out volume);
        _volume.value = volume;
        PlayerPrefs.SetFloat("Volume", volume);
        PlayerPrefs.SetFloat("Sensitivity", sensitivity);
    }

    //Загрузка настроек
    public void LoadSettings(int currentResolutionIndex)
    {
        if (PlayerPrefs.HasKey("QualitySettings"))
            _qualityDropdown.value = PlayerPrefs.GetInt("QualitySettings");
        else
            _qualityDropdown.value = 3;
        SetQuality(_qualityDropdown.value);

        if (PlayerPrefs.HasKey("Resolution"))
            _resolutionDropdown.value = PlayerPrefs.GetInt("Resolution");
        else
            _resolutionDropdown.value = currentResolutionIndex;
        SetResolution(_resolutionDropdown.value);

        if (PlayerPrefs.HasKey("Fullscreen"))
            Screen.fullScreen = System.Convert.ToBoolean(PlayerPrefs.GetInt("Fullscreen"));
        else
            Screen.fullScreen = true;
        _toggle.isOn = Screen.fullScreen;

        if (PlayerPrefs.HasKey("Volume"))
        {
            _audioMixer.SetFloat("MasterVolume", PlayerPrefs.GetFloat("Volume"));
            float volumeSlide = Mathf.Pow(10, (PlayerPrefs.GetFloat("Volume") / 20));
            _volume.value = volumeSlide;
        }
        else
        {
            _audioMixer.SetFloat("MasterVolume", 0);
            _volume.value = 1;
        }

        if (PlayerPrefs.HasKey("Sensitivity"))
            _sensitivity.value = PlayerPrefs.GetFloat("Sensitivity");
        else
            _sensitivity.value = 1;
    }

    //Методы настроек
    public void Settings()
    {
        menuPanel.gameObject.SetActive(false);
        settingsPanel.gameObject.SetActive(true);
        LoadSettings(_currentResolutionIndex);
    }

    //Метод настройки чувствительности мыши
    public void SetSensitivity(float deseredSensitivity)
    {
        PlayerPrefs.SetFloat("Sensitivity", deseredSensitivity);
    }

    //Метод настройки звука
    public void SetVolume(float desiredVolume)
    {
        _audioMixer.SetFloat("MasterVolume", Mathf.Log10(desiredVolume) * 20);
    }

    //Метод настройки графики
    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
        _qualityDropdown.value = qualityIndex;
    }

    //Метод добавления популярных разрешений экрана
    private void PopulateResolutionDropdown()
    {
        _resolutionDropdown.ClearOptions();
        List<string> options = new List<string>();
        _resolutions = Screen.resolutions;
        _currentResolutionIndex = 0;

        for (int i = 0; i < _resolutions.Length; i++)
        {
            string option = _resolutions[i].width + "x" + _resolutions[i].height;
            options.Add(option);
            if (_resolutions[i].width == Screen.currentResolution.width && _resolutions[i].height == Screen.currentResolution.height)
                _currentResolutionIndex = i;
        }
        _resolutionDropdown.AddOptions(options);
        _resolutionDropdown.RefreshShownValue();
    }

    //Метод настройки разрешения экрана
    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = _resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        _resolutionDropdown.value = resolutionIndex;
    }

    //Метод настройки полноэкранного режима
    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    //Метод сброса настроек к стандартным
    public void ResetSettings()
    {
        SetQuality(2);
        SetResolution(_currentResolutionIndex);
        Screen.fullScreen = true;
        _toggle.isOn = true;
        _volume.value = 1;
        _sensitivity.value = 1;
        Debug.Log("Reset Correct");
    }
    //Метод кнопки "Продолжить"
    public void ContinueButton()
    {
        LoadSettings(_currentResolutionIndex);
        ContinueData.boolContinue = true;
        OpenScene("GameScene");
    }

}