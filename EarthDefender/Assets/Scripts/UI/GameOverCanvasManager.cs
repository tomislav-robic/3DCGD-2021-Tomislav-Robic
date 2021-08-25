using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverCanvasManager : MonoBehaviour
{
    public void Retry()
    {
        if (DifficultyHandler.i != null) Destroy(DifficultyHandler.i.gameObject);
        if (PointsSystem.i != null) Destroy(PointsSystem.i.gameObject);
        if (SpaceshipHealth.i != null) Destroy(SpaceshipHealth.i.gameObject);
        if (EarthHealth.i != null) Destroy(EarthHealth.i.gameObject);
        SceneManager.LoadScene(1);
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
