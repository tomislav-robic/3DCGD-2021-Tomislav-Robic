using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    #region Audio Variables
    [HideInInspector]
    public float masterVolume = 1f, sfxVolume = 1f, musicVolume = 1f;
    [HideInInspector]
    public bool muteMaster = false, muteSfx = false, muteMusic = false;
    #endregion
    #region Gameplay Variables
    [HideInInspector]
    public string playerName = "Player";
    [HideInInspector]
    public float mouseSensitivity = 1f;
    [HideInInspector]
    public int difficulty = 0;
    #endregion
    #region Video Variables
    [HideInInspector]
    public int resolutionIndex = 0;
    [HideInInspector]
    public bool fullscreen = true;
    #endregion

    public AudioMixer mixer;
    public Slider masterVolumeSlider, sfxVolumeSlider, musicVolumeSlider;
    public Toggle masterMuteToggle, sfxMuteToggle, musicMuteToggle;
    public InputField playerNameField;
    public Slider mouseSensitivitySlider;
    public Text mouseSensitivityText;
    public Dropdown difficultyDropdown, resolutionDropdown;
    public Toggle fullscreenToggle;

    Resolution[] resolutions;

    private void Start()
    {
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();
        int currResolutionIndex = resolutionIndex;
        List<string> options = new List<string>();
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = $"{resolutions[i].width} x {resolutions[i].height}@{resolutions[i].refreshRate}Hz";
            options.Add(option);
            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height) currResolutionIndex = i;
        }
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currResolutionIndex;
        resolutionDropdown.RefreshShownValue();
        resolutionIndex = currResolutionIndex;

        InitializeSettings();
    }

    private void InitializeSettings()
    {
        SettingsFile settings = SettingsFileSystem.LoadSettings();
        if (settings != null)
        {
            masterVolume = settings.masterVolume;
            sfxVolume = settings.sfxVolume;
            musicVolume = settings.musicVolume;
            muteMaster = settings.muteMaster;
            muteSfx = settings.muteSfx;
            muteMusic = settings.muteMusic;
            playerName = settings.playerName;
            mouseSensitivity = settings.mouseSensitivity;
            difficulty = settings.difficulty;
            resolutionIndex = settings.resolutionIndex;
            fullscreen = settings.fullscreen;
            SetMasterVolume(masterVolume);
            SetSFXVolume(sfxVolume);
            SetMusicVolume(musicVolume);
            masterVolumeSlider.value = masterVolume;
            sfxVolumeSlider.value = sfxVolume;
            musicVolumeSlider.value = musicVolume;
            masterMuteToggle.isOn = muteMaster;
            sfxMuteToggle.isOn = muteSfx;
            musicMuteToggle.isOn = muteMusic;
            playerNameField.text = playerName;
            mouseSensitivitySlider.value = mouseSensitivity;
            difficultyDropdown.value = difficulty;
            resolutionDropdown.value = resolutionIndex;
            fullscreenToggle.isOn = fullscreen;
        }
    }
    public void SetMasterVolume(float volume)
    {
        if (!muteMaster)
        {
            mixer.SetFloat("MasterVolume", Mathf.Log10(volume) * 40f);
            masterVolume = volume;
        } else
        {
            mixer.SetFloat("MasterVolume", -80f);
        }
    }
    public void SetSFXVolume(float volume)
    {
        if (!muteSfx)
        {
            mixer.SetFloat("SFXVolume", Mathf.Log10(volume) * 40f);
            sfxVolume = volume;
        } else
        {
            mixer.SetFloat("SFXVolume", -80f);
        }
    }

    public void SetMusicVolume(float volume)
    {
        if (!muteMusic)
        {
            mixer.SetFloat("MusicVolume", Mathf.Log10(volume) * 40f);
            musicVolume = volume;
        } else
        {
            mixer.SetFloat("MusicVolume", -80f);
        }
    }
    public void SetMuteMaster(bool isOn)
    {
        muteMaster = isOn;
        if (isOn) mixer.SetFloat("MasterVolume", -80f);
        else mixer.SetFloat("MasterVolume", masterVolume);
    }
    public void SetMuteSfx(bool isOn)
    {
        muteSfx = isOn;
        if (isOn) mixer.SetFloat("SFXVolume", -80f);
        else mixer.SetFloat("SFXVolume", sfxVolume);
    }
    public void SetMuteMusic(bool isOn)
    {
        muteMusic = isOn;
        if (isOn) mixer.SetFloat("MusicVolume", -80f);
        else mixer.SetFloat("MusicVolume", musicVolume);
    }
    public void SetPlayerName(string playername)
    {
        playerName = playername;
    }
    public void SetMouseSensitivity(float mousesens)
    {
        mouseSensitivity = mousesens;
        mouseSensitivityText.text = mousesens.ToString();
    }

    public void SetDifficulty(int value)
    {
        difficulty = value;
    }
    public void SetResolution(int resIndex)
    {
        Resolution res = resolutions[resIndex];
        Screen.SetResolution(res.width, res.height, Screen.fullScreen);
        resolutionIndex = resIndex;
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
        fullscreen = isFullscreen;    
    }

    public void ApplySettings()
    {
        masterVolume = masterVolumeSlider.value;
        sfxVolume = sfxVolumeSlider.value;
        musicVolume = musicVolumeSlider.value;
        muteMaster = masterMuteToggle.isOn;
        muteSfx = sfxMuteToggle.isOn;
        muteMusic = musicMuteToggle.isOn;
        playerName = playerNameField.text;
        difficulty = difficultyDropdown.value;
        resolutionIndex = resolutionDropdown.value;
        fullscreen = fullscreenToggle.isOn;
        SettingsFileSystem.SaveSettings(this);
    }
    public void CancelChanges()
    {
        InitializeSettings();
    }
}
