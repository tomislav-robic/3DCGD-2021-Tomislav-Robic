using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class Spawner : MonoBehaviour
{
    public static int spawnedAsteroids;
    public static bool levelFailed = false;

    public GameObject bigAsteroid;
    public GameObject coin;
    public float asteroidSpeed = 1f;
    public float spawnDelay = 1f;
    public float XOffset = 15f;
    public float YOffset = 10f;
    public float levelTime = 120f;
    public bool useEnemies = false;
    public bool isBoss = false;
    public PowerUps powerUps;
    public float powerUpSpeed = 3f;
    [Range(0f,100f)]
    public float powerUpChance = 10f;
    [Range(0f, 100f)]
    public float coinChance = 6f;

    DifficultyHandler difficulty;
    ObjectPooler objectPooler;
    List<GameObject> enemies = new List<GameObject>();
    float time = 0f;
    bool spawnHealth = false;
    bool spawnShield = false;
    bool spawnEarth = false;
    bool healthChecked = false;
    bool levelComplete = false;
    bool executed = false;
    void Start()
    {
        spawnedAsteroids = 0;
        objectPooler = ObjectPooler.i;
        difficulty = FindObjectOfType<DifficultyHandler>();
        if (useEnemies)
        {
            foreach(GameObject child in GameObject.FindGameObjectsWithTag("Enemy"))
            {
                enemies.Add(child);
            }
        }
        asteroidSpeed *= difficulty.diffFloat;
        spawnDelay /= difficulty.diffFloat;
        powerUpSpeed *= difficulty.diffFloat;
        powerUpChance /= difficulty.diffFloat;
        coinChance /= difficulty.diffFloat;
        SoundManager.i.Play($"Level{SceneManager.GetActiveScene().buildIndex}Theme");
        InvokeRepeating("SpawnBigAsteroid", 1f, spawnDelay);
    }
    void Update()
    {
        if (!useEnemies && !isBoss)
        {
            time += Time.deltaTime;
            if (time >= levelTime && !levelComplete)
            {
                CancelInvoke();
                levelComplete = true;
            }
            //Debug.Log($"Time: {time}, Spawned Asteroids: {spawnedAsteroids}, Level Complete: {levelComplete}, Executed: {executed}, Level Failed: {levelFailed}");
            if (spawnedAsteroids == 0 && levelComplete && !executed && !levelFailed)
            {
                //Debug.Log($"Level Complete");
                FindObjectOfType<PlayableDirector>().Play();
                foreach (WeaponController weapon in FindObjectsOfType<WeaponController>()) weapon.enabled = false;
                SpaceshipController.canPause = false;
                executed = true;
                //Debug.Log($"Script executed");
            }
        } 
        else if (useEnemies)
        {
            enemies.RemoveAll(item => item == null);
            if (enemies.Count == 0)
            {
                if (!healthChecked)
                {
                    SpaceshipHealth spaceship = FindObjectOfType<SpaceshipHealth>();
                    EarthHealth earth = FindObjectOfType<EarthHealth>();
                    if (spaceship.health <= spaceship.maxHealth * 0.7) levelTime += 20f;
                    if (spaceship.shield <= spaceship.maxShield * 0.7) levelTime += 20f;
                    if (earth.health <= earth.maxHealth * 0.7) levelTime += 20f;
                    healthChecked = true;
                }
                time += Time.deltaTime;
                if (time >= levelTime && !levelComplete)
                {
                    CancelInvoke();
                    levelComplete = true;
                }
                if (spawnedAsteroids == 0 && levelComplete && !executed && !levelFailed)
                {
                    FindObjectOfType<PlayableDirector>().Play();
                    foreach (WeaponController weapon in FindObjectsOfType<WeaponController>()) weapon.enabled = false;
                    SpaceshipController.canPause = false;
                    executed = true;
                }
            }
        }
    }

    public void LoadNextScene()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void SpawnBigAsteroid()
    {
        spawnEarth = false;
        spawnHealth = false;
        spawnShield = false;
        GetConditions();
        float rng = Random.value;
        if(spawnEarth && rng >= 1f-(powerUpChance/100f))
        {
            GameObject _earthPowerUp = Instantiate(powerUps.earthPowerUp, new Vector3(transform.position.x + Random.Range(-XOffset, XOffset), transform.position.y + Random.Range(-YOffset, YOffset), transform.position.z), Quaternion.identity);
            _earthPowerUp.GetComponent<Rigidbody>().AddForce(-Vector3.forward * 2500f * powerUpSpeed, ForceMode.Impulse);
        }
        else if (spawnHealth && rng >= 1f - (powerUpChance / 100f))
        {
            GameObject _healthPowerUp = Instantiate(powerUps.healthPowerUp, new Vector3(transform.position.x + Random.Range(-XOffset, XOffset), transform.position.y + Random.Range(-YOffset, YOffset), transform.position.z), Quaternion.identity);
            _healthPowerUp.GetComponent<Rigidbody>().AddForce(-Vector3.forward * 2500f * powerUpSpeed, ForceMode.Impulse);
        }
        else if (spawnShield && rng >= 1f - (powerUpChance / 100f))
        {
            GameObject _shieldPowerUp = Instantiate(powerUps.shieldPowerUp, new Vector3(transform.position.x + Random.Range(-XOffset, XOffset), transform.position.y + Random.Range(-YOffset, YOffset), transform.position.z), Quaternion.identity);
            _shieldPowerUp.GetComponent<Rigidbody>().AddForce(-Vector3.forward * 2500f * powerUpSpeed, ForceMode.Impulse);
        }
        else if (rng >= 1f - (coinChance/100f))
        {
            GameObject _coin = Instantiate(coin, new Vector3(transform.position.x + Random.Range(-XOffset, XOffset), transform.position.y + Random.Range(-YOffset, YOffset), transform.position.z), coin.transform.rotation);
            _coin.GetComponent<Rigidbody>().AddForce(-Vector3.forward * 2500f * powerUpSpeed, ForceMode.Impulse);
        }
        else
        {
            GameObject _bigAsteroid = objectPooler.SpawnFromPool(PooledObjects.BigAsteroid, new Vector3(transform.position.x + Random.Range(-XOffset, XOffset), transform.position.y + Random.Range(-YOffset, YOffset), transform.position.z), Quaternion.identity);
            _bigAsteroid.GetComponent<Rigidbody>().velocity = -Vector3.forward * 25f * asteroidSpeed;
        }
    }

    void GetConditions()
    {
        SpaceshipHealth spaceship = FindObjectOfType<SpaceshipHealth>();
        EarthHealth earth = FindObjectOfType<EarthHealth>();
        if (spaceship != null && earth != null)
        {
            if (earth.health <= earth.maxHealth * 0.7f) spawnEarth = true;
            else if (spaceship.health <= spaceship.maxHealth * 0.7f) spawnHealth = true;
            else if (spaceship.shield <= spaceship.maxShield * 0.7f) spawnShield = true;
            else
            {
                spawnEarth = false;
                spawnHealth = false;
                spawnShield = false;
            }
        }
    }

    private void OnDestroy()
    {
        if (SoundManager.i != null) SoundManager.i.Stop($"Level{SceneManager.GetActiveScene().buildIndex}Theme");
    }
}

[System.Serializable]
public class PowerUps {
    public GameObject healthPowerUp;
    public GameObject shieldPowerUp;
    public GameObject earthPowerUp;
}