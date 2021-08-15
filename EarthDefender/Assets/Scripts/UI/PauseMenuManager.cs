using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuManager : MonoBehaviour
{
    public GameObject buttons;
    public GameObject settingsMenu;

    public void Continue()
    {
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        FindObjectOfType<SpaceshipController>().LockCursor();
        gameObject.SetActive(false);
    }
    
    public void Open()
    {
        buttons.SetActive(true);
        settingsMenu.SetActive(false);
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
