using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class SettingsMenu : MonoBehaviour
{
    [Header("Audio Mixers")]
    public AudioMixer menuAudioMixer;
    public AudioMixer gameAudioMixer;
    
    private const string MainMenuVolumePrefsKey = "MainMenuVolume";
    private const string GameVolumePrefsKey = "GameVolume";

    [Header("Resolutions")]
    public TMPro.TMP_Dropdown resolutionDropdown;
    Resolution[] resolutions;

    void Start()
    {

        // The game and menu volume.
        float savedVolume = PlayerPrefs.GetFloat(MainMenuVolumePrefsKey, 1.0f);
        float savedGameVolume = PlayerPrefs.GetFloat(GameVolumePrefsKey, 1.0f);
        SetVolume(savedVolume);
        SetGameVolume(savedGameVolume);

        // Screens resolution.
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetVolume(float MainMenuVolume)
    {
        if (menuAudioMixer != null)
        {
            menuAudioMixer.SetFloat("MainMenuVolume", MainMenuVolume);
        }
        PlayerPrefs.SetFloat(MainMenuVolumePrefsKey, MainMenuVolume);
    }


    public void SetGameVolume(float gameVolume)
    {
        if (gameAudioMixer != null)
        {
            gameAudioMixer.SetFloat("GameVolume", gameVolume);
        }
        PlayerPrefs.SetFloat(GameVolumePrefsKey, gameVolume);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }
}
