[System.Serializable]
public class SettingsFile
{
    public float masterVolume, sfxVolume, musicVolume;
    public bool muteMaster, muteSfx, muteMusic;
    public string playerName;
    public float mouseSensitivity;
    public int difficulty;
    public int resolutionIndex;
    public bool fullscreen;
    public SettingsFile(SettingsManager settings)
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
    }
    public SettingsFile()
    {
        masterVolume = 1f;
        sfxVolume = 1f;
        musicVolume = 1f;
        muteMaster = false;
        muteSfx = false;
        muteMusic = false;
        playerName = "Player";
        mouseSensitivity = 1f;
        difficulty = 0;
        resolutionIndex = 0;
        fullscreen = false;
    }
}
