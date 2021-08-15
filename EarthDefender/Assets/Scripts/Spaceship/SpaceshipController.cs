using UnityEngine;
using UnityEngine.SceneManagement;

public class SpaceshipController : MonoBehaviour
{
    public PauseMenuManager pauseMenuManager;
    public float speed = 3f;
    public float rotationSpeed = 100f;
    public float mouseSensitivity = 1f;
    public float maxRotation = 30f;
    bool lockedCursor = false;
    private void Awake()
    {
        lockedCursor = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = !lockedCursor;
    }

    private void Start()
    {
        SettingsFile settings = SettingsFileSystem.LoadSettings();
        mouseSensitivity = settings.mouseSensitivity;
        transform.position = Vector3.zero;
    }

    public void LockCursor()
    {
        lockedCursor = !lockedCursor;
        if (lockedCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
        }
        Cursor.visible = !lockedCursor;
    }

    public void EnableAnimator()
    {
        GetComponent<Animator>().enabled = true;
    }
    public void LoadNextScene()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Pause()
    {
        if (lockedCursor) Time.timeScale = 1f;
        else Time.timeScale = 0f;
        pauseMenuManager.gameObject.SetActive(!lockedCursor);
        if (!lockedCursor) pauseMenuManager.Open();
        SettingsFile settings = SettingsFileSystem.LoadSettings();
        mouseSensitivity = settings.mouseSensitivity;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pauseMenuManager == null) pauseMenuManager = Resources.FindObjectsOfTypeAll<PauseMenuManager>()[0];
            LockCursor();
            Pause();
        }
        float moveX = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        float moveY = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        Vector3 moveVector = Vector3.right * moveX + Vector3.up * moveY;
        transform.position += moveVector;
        transform.Rotate(new Vector3(-Input.GetAxis("Mouse Y") * mouseSensitivity * rotationSpeed * Time.deltaTime, Input.GetAxis("Mouse X") * mouseSensitivity * rotationSpeed * Time.deltaTime, 0f));
        transform.eulerAngles = ClampedTargetAngles(transform.eulerAngles);
    }
    
    private Vector3 ClampedTargetAngles(Vector3 eulerAngles)
    {
        return new Vector3
        {
            x = Mathf.Clamp(
                (eulerAngles.x > 180) ? eulerAngles.x - 360 : eulerAngles.x,
                -maxRotation,
                maxRotation
            ),
            y = Mathf.Clamp(
                (eulerAngles.y > 180) ? eulerAngles.y - 360 : eulerAngles.y,
                -maxRotation,
                maxRotation
            ),
            z = 0f
        };
    }
}
