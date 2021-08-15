using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinCanvasManager : MonoBehaviour
{
    public Text pointsText;

    private void Awake()
    {
        PointsSystem pointsSystem = FindObjectOfType<PointsSystem>();
        pointsText.text = $"Points: {pointsSystem.points}";
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
