using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public static int spawnedAsteroids;

    public GameObject bigAsteroid;
    public GameObject smallAsteroid;
    public float asteroidSpeed = 1f;
    public float spawnDelay = 1f;
    public float XOffset = 15f;
    public float YOffset = 10f;
    public float levelTime = 120f;
    float time = 0f;
    bool levelComplete = false;
    void Start()
    {
        InvokeRepeating("SpawnBigAsteroid", 1f, spawnDelay);
    }
    void Update()
    {
        time += Time.deltaTime;
        if (time >= levelTime && !levelComplete)
        {
            CancelInvoke();
            levelComplete = true;
        }
        if(spawnedAsteroids == 0 && levelComplete)
        {
            Debug.Log("Level complete");
        }
    }

    public void SpawnBigAsteroid()
    {
        GameObject _bigAsteroid = Instantiate(bigAsteroid, new Vector3(transform.position.x + Random.Range(-XOffset, XOffset), transform.position.y + Random.Range(-YOffset, YOffset), transform.position.z), Quaternion.identity);
        _bigAsteroid.GetComponent<Rigidbody>().AddForce(-Vector3.forward * 2500f * asteroidSpeed, ForceMode.Impulse);
    }
}
