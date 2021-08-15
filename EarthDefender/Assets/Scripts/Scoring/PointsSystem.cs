using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PointsSystem : MonoBehaviour
{
    public string playerName = "Player";
    public float points = 0f;

    public static PointsSystem i;

    private void Awake()
    {
        SettingsFile settings = SettingsFileSystem.LoadSettings();
        playerName = settings.playerName;
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
        if (scene.buildIndex == 0)
        {
            Destroy(gameObject);
            return;
        }
        UpdatePointsUI();
    }

    public void AddPoints(float _points)
    {
        points += _points;
        UpdatePointsUI();
    }

    public void UpdatePointsUI()
    {
        Text text = GameObject.Find("PointsText").GetComponent<Text>();
        text.text = $"Points: {points}";
    }

    public void SavePoints()
    {
        PointsFile pointsFile = new PointsFile(points, playerName);
        PointsFileSystem.SaveScore(pointsFile);
    }
}
