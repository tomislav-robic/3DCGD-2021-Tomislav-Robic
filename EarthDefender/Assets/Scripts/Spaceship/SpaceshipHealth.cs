using Cinemachine;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.Timeline;
using UnityEngine.UI;

public class SpaceshipHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    public float maxShield = 100f;
    public Canvas gameOverCanvas;
    public GameObject explosionParticle;

    [HideInInspector]
    public float health;
    [HideInInspector]
    public float shield;

    Slider healthSlider;
    Slider shieldSlider;
    bool destroyed = false;
    DifficultyHandler difficulty;

    public static SpaceshipHealth i;

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
        healthSlider = GameObject.Find("SpaceshipHealth").GetComponent<Slider>();
        shieldSlider = GameObject.Find("SpaceshipShield").GetComponent<Slider>();
        maxHealth = Mathf.Floor(maxHealth / difficulty.diffFloat);
        maxShield = Mathf.Floor(maxShield / difficulty.diffFloat);
        shield = maxShield;
        health = maxHealth;
        healthSlider.maxValue = maxHealth;
        shieldSlider.maxValue = maxShield;
        healthSlider.value = health;
        shieldSlider.value = shield;
    }
    private void OnSceneLoad(Scene scene, LoadSceneMode sceneMode)
    {
        if (scene.buildIndex > 0)
        {
            CinemachineVirtualCamera vcam = GameObject.Find("VCam").GetComponent<CinemachineVirtualCamera>();
            vcam.Follow = transform;
            vcam.LookAt = transform;
            if (scene.buildIndex == 2) Destroy(GetComponent<PlayableDirector>());
            transform.position = Vector3.zero;
            if (GetComponent<PlayableDirector>() != null) GetComponent<PlayableDirector>().Stop();
            foreach (WeaponController weapon in FindObjectsOfType<WeaponController>()) weapon.enabled = true;
            GetComponent<Animator>().enabled = false;
            if (GameObject.Find("Warp") != null) GameObject.Find("Warp").SetActive(false);
            transform.Find("Point Light").GetComponent<Light>().intensity = 60f;
            healthSlider = GameObject.Find("SpaceshipHealth").GetComponent<Slider>();
            shieldSlider = GameObject.Find("SpaceshipShield").GetComponent<Slider>();
            healthSlider.maxValue = maxHealth;
            shieldSlider.maxValue = maxShield;
            healthSlider.value = health;
            shieldSlider.value = shield;
        }
        else Destroy(gameObject);
    }
    public void TakeDamage(float damage)
    {
        if (shield > 0f)
        {
            shield -= damage;
            if (shield < 0f)
            {
                health += shield;
                shield = 0f;
            }
        } else
        {
            health -= damage;
        }
        if (health <= 0f && !destroyed)
        {
            health = 0f;
            Die();
            destroyed = true;
        }
        healthSlider.value = health;
        shieldSlider.value = shield;
    }

    public void Heal(float _health)
    {
        health += _health;
        if (health >= maxHealth) health = maxHealth;
        healthSlider.value = health;
    }

    public void ReplenishShield(float _shield)
    {
        shield += _shield;
        if (shield >= maxShield) shield = maxShield;
        shieldSlider.value = shield;
    }

    public void Die()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        GameObject.Find("HUD").SetActive(false);
        Instantiate(gameOverCanvas);
        SoundManager.i.Play(Sounds.Explosion);
        GameObject particle = Instantiate(explosionParticle, transform.position, Quaternion.identity);
        Destroy(particle, 5.5f);
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoad;
    }
    
}
