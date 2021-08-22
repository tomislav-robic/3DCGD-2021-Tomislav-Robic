using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    private bool timelinePlaying = false;

    private void Start()
    {
        Time.timeScale = 1f;
        SoundManager.i.Play(Sounds.MenuTheme);
    }

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
        SoundManager.i.Stop(Sounds.MenuTheme);
        timelinePlaying = true;
    }
    private void Update()
    {
        if (timelinePlaying && (Input.GetKeyUp(KeyCode.Space) || Input.GetMouseButtonUp(0))) StartGame(1);
    }
}
