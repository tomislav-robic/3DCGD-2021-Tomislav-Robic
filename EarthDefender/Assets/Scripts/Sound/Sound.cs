using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;

    [Range(0f,1f)]
    public float volume = 1f;
    [Range(-3f,3f)]
    public float pitch = 1f;
    public bool loop;
    public AudioMixerGroup mixer;

    [HideInInspector]
    public AudioSource source;
}

public struct Sounds
{
    public static string Shoot = "Shoot";
    public static string Explosion = "Explosion";
    public static string Level1Theme = "Level1Theme";
    public static string Level2Theme = "Level2Theme";
    public static string Level3Theme = "Level3Theme";
    public static string MenuTheme = "MenuTheme";
}