using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EarthHealth : MonoBehaviour
{
    public float maxHealth = 200f;
    public Canvas gameOverCanvas;

    [HideInInspector]
    public float health;
    Slider healthSlider;
    DifficultyHandler difficulty;

    public static EarthHealth i;

    private void Awake()
    {
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

    private void Start()
    {
        difficulty = FindObjectOfType<DifficultyHandler>();
        maxHealth = Mathf.Floor(maxHealth / difficulty.diffFloat);
        healthSlider = GameObject.Find("EarthHealth").GetComponent<Slider>();
        health = maxHealth;
        healthSlider.maxValue = maxHealth;
        healthSlider.value = health;
    }

    private void OnSceneLoad(Scene scene, LoadSceneMode sceneMode)
    {
        if (scene.buildIndex > 0)
        {
            healthSlider = GameObject.Find("EarthHealth").GetComponent<Slider>();
            healthSlider.maxValue = maxHealth;
            healthSlider.value = health;
        }
        else Destroy(gameObject);
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0f)
        {
            health = 0f;
            Die();
        }
        healthSlider.value = health;
    }

    public void Heal(float _health)
    {
        health += _health;
        if (health >= maxHealth) health = maxHealth;
        healthSlider.value = health;
    }

    public void Die()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        GameObject.Find("HUD").SetActive(false);
        Instantiate(gameOverCanvas);
        FindObjectOfType<SpaceshipController>().enabled = false;
        FindObjectOfType<SpaceshipHealth>().enabled = false;
        Destroy(gameObject);
    }
    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoad;
    }
}
