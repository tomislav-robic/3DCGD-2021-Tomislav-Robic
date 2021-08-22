using UnityEngine;
using UnityEngine.SceneManagement;

public class DifficultyHandler : MonoBehaviour
{
    [HideInInspector]
    public SettingsFile settings;
    public float diffFloat;
    Difficulty difficulty;

    public static DifficultyHandler i;

    void Awake()
    {
        settings = SettingsFileSystem.LoadSettings();
        difficulty = (Difficulty)settings.difficulty;
        if (difficulty == Difficulty.Easy) diffFloat = 0.75f;
        else if (difficulty == Difficulty.Normal) diffFloat = 1f;
        else if (difficulty == Difficulty.Hard) diffFloat = 1.25f;
        if (i == null)
            i = this;
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoad;
    }

    private void OnSceneLoad(Scene scene, LoadSceneMode sceneMode)
    {
        if (scene.buildIndex == 0 && i != null) Destroy(gameObject);
    }
}

public enum Difficulty
{
    Easy = 0,
    Normal = 1,
    Hard = 2
}