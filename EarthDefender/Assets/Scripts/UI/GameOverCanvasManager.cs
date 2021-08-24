using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverCanvasManager : MonoBehaviour
{
    public void Retry()
    {
        Destroy(DifficultyHandler.i.gameObject);
        Destroy(PointsSystem.i.gameObject);
        Destroy(SpaceshipHealth.i.gameObject);
        SceneManager.LoadScene(1);
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
