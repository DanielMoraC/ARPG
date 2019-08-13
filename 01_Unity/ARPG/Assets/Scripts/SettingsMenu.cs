using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class SettingsMenu : MonoBehaviour
{
    [Header("Menu")]
    //Menu
    public GameObject Menu;
    //0 = Audio, 1 = Graphics, 2 = Gameplay, 3 = Controls
    public GameObject[] menuType;
    
    [Header("Audio")]
    //Audio
    //Master Audio mixer
    public AudioMixerGroup MainAudio;
    public AudioMixerGroup MusicAudio;
    public AudioMixerGroup SoundAudio;
    public AudioMixerGroup AmbientAudio;
    
    [Header("Gameplay")]
    //Gameplay
    public TMP_Text FOV;
    public Slider FOVSlider;

    [Header("Graphics")]
    //Graphics
    //Resolutions
    Resolution ResolutionFrame;
    Resolution[] resolutions;
    public TMP_Text resolutionText;
    private const string RESOLUTION_PREF_KEY = "resolution";
    private int currentResolutionIndex = 0;
    //Screen Type
    public TMPro.TMP_Dropdown screenDropdown;
    //graphic quality
    public TMPro.TMP_Dropdown qualityDropdown;
    
    //Check if teh player can move
    static public bool playing = true;
    
    
    void Start()
    {
        //Set the fov value
        FOVSlider.value = PlayerPrefs.GetFloat("FOV", 60);
        
        //Show FOV value
        FOV.text = FOVSlider.value.ToString();
        
        //Hide the cursor and lock it to center screen
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        
        //Set the graphic quality 
        qualityDropdown.value = PlayerPrefs.GetInt("Quality", 0);
        
        //Set the resolution
        /////////////////////////resolutionDropdown.value = PlayerPrefs.GetInt("Resolution", 0);
        
        //Set the screen type
        screenDropdown.value = PlayerPrefs.GetInt("Screen", 0);

        //Resolution
        resolutions = Screen.resolutions;
        currentResolutionIndex = PlayerPrefs.GetInt(RESOLUTION_PREF_KEY, 0);
        SetResolutionText(resolutions[currentResolutionIndex]);
        QualitySettings.vSyncCount = 0;
    }

    //Resolution
    private int GetNextWrappedIndex<T>(IList<T> collection, int currentIndex)
    {
        if (collection.Count < 1) return 0;
        return (currentIndex + 1) % collection.Count;
    }
    
    private int GetPreviusWrappedIndex<T>(IList<T> collection, int currentIndex)
    {
        if (collection.Count < 1) return 0;
        if ((currentIndex - 1) < 0) return collection.Count - 1;
        return (currentIndex - 1) % collection.Count;
    }

    private void SetResolutionText(Resolution resolution)
    {
        resolutionText.text = resolution.width + " x " + resolution.height + " @ " + resolution.refreshRate;
        Application.targetFrameRate = resolution.refreshRate;
    }

    public void SetNextResolution()
    {
        currentResolutionIndex = GetNextWrappedIndex(resolutions, currentResolutionIndex);
        SetResolutionText(resolutions[currentResolutionIndex]);
    }
    
    public void SetPreviousResolution()
    {
        currentResolutionIndex = GetPreviusWrappedIndex(resolutions, currentResolutionIndex);
        SetResolutionText(resolutions[currentResolutionIndex]);
    }

    private void SetAndApplyResolution(int newResolutionIndex)
    {
        currentResolutionIndex = newResolutionIndex;
        ApplyCurrentResolution();
    }
    
    private void ApplyCurrentResolution()
    {
        ApplyResolution(resolutions[currentResolutionIndex]);
    }
    
    private void ApplyResolution(Resolution resolution)
    {
        SetResolutionText(resolution);
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        PlayerPrefs.SetInt(RESOLUTION_PREF_KEY, currentResolutionIndex);
    }

    public void ApplyChanges()
    {
        SetAndApplyResolution(currentResolutionIndex);
    }

    // Update is called once per frame
    void Update()
    {
        //open and close menu and show or hide the cursor
        if (Input.GetKeyDown(KeyCode.Escape) && !Menu.activeSelf)
        {
            playing = false;
            Time.timeScale = 0;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            Menu.SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && Menu.activeSelf)
        {
            playing = true;
            Time.timeScale = 1;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            Menu.SetActive(false);
        }
    }

    //Main Audio
    public void SetVolumeMain(float volumeMain)
    {
        MainAudio.audioMixer.SetFloat("MainVolume", volumeMain);
    }
    
    //Music Audio
    public void SetVolumeMusic(float volumeMusic)
    {
        MusicAudio.audioMixer.SetFloat("MusicVolume", volumeMusic);
    }
    
    //Sound Audio
    public void SetVolumeSound(float volumeSound)
    {
        SoundAudio.audioMixer.SetFloat("SoundVolume", volumeSound);
    }
    
    //Ambient Audio
    public void SetVolumeAmbient(float volumeAmbient)
    {
        AmbientAudio.audioMixer.SetFloat("AmbientVolume", volumeAmbient);
    }

    //Graphic Quality
    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
        QualitySettings.antiAliasing = 2;
    }

    //Set the player pref for graphic quality
    public void OnValueChangeGraphic()
    {
        PlayerPrefs.SetInt("Quality", qualityDropdown.value);
    }

    //Set screen
    public void SetScreen()
    {
        if (screenDropdown.value == 0)
        {
            Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
            PlayerPrefs.SetInt("Screen", screenDropdown.value);
        }
        else if (screenDropdown.value == 1)
        {
            Screen.fullScreenMode = FullScreenMode.Windowed;
            PlayerPrefs.SetInt("Screen", screenDropdown.value);
        }
        else if (screenDropdown.value == 2)
        {
            Screen.fullScreenMode = FullScreenMode.MaximizedWindow;
            PlayerPrefs.SetInt("Screen", screenDropdown.value);
        }
    }

    //Activate menus
    public void AudioMenu()
    {
        menuType[0].SetActive(true);
        menuType[1].SetActive(false);
        menuType[2].SetActive(false);
        menuType[3].SetActive(false);
    }
    
    public void GraphicsMenu()
    {
        menuType[0].SetActive(false);
        menuType[1].SetActive(true);
        menuType[2].SetActive(false);
        menuType[3].SetActive(false);
    }
    
    public void GameplayMenu()
    {
        menuType[0].SetActive(false);
        menuType[1].SetActive(false);
        menuType[2].SetActive(true);
        menuType[3].SetActive(false);
    }
    
    public void ControlMenu()
    {
        menuType[0].SetActive(false);
        menuType[1].SetActive(false);
        menuType[2].SetActive(false);
        menuType[3].SetActive(true);
    }
    
    //FOV
    public void FOVSettings(float FOVValue)
    {
        Camera.main.fieldOfView = FOVValue;
        FOV.text = FOVValue.ToString();
        PlayerPrefs.SetFloat("FOV", FOVSlider.value);
    }
}
