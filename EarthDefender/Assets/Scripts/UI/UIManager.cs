using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    private bool timelinePlaying = false;

    public void Exit()
    {
        Application.Quit();
    }
    public void StartGame(int sceneID)
    {
        SceneManager.LoadSceneAsync(sceneID);
    }
    public void TimelineStarted()
    {
        timelinePlaying = true;
    }
    private void Update()
    {
        if (timelinePlaying && (Input.GetKeyUp(KeyCode.Space) || Input.GetMouseButtonUp(0))) StartGame(1);
    }
}
