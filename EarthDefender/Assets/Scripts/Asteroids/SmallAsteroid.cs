using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallAsteroid : MonoBehaviour
{
    public GameObject explosionParticle;
    public float damage = 20f;
    EarthHealth earth;
    SpaceshipHealth spaceship;
    bool exploded = false;
    private void Awake()
    {
        earth = FindObjectOfType<EarthHealth>();
        spaceship = FindObjectOfType<SpaceshipHealth>();
        Spawner.spawnedAsteroids++;
    }
    private void OnDestroy()
    {
        Spawner.spawnedAsteroids--;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Bullet") && !exploded)
        {
            exploded = true;
            GameObject particle = Instantiate(explosionParticle, transform.position, Quaternion.identity);
            Destroy(particle, 5.5f);
            Destroy(other.gameObject); // Convert to pooling later
            Destroy(gameObject);
        }
        if (other.CompareTag("Earth") && !exploded)
        {
            exploded = true;
            earth.TakeDamage(damage);
            Destroy(gameObject);
        }
        if (other.CompareTag("Spaceship") && !exploded)
        {
            exploded = true;
            spaceship.TakeDamage(damage);
            GameObject particle = Instantiate(explosionParticle, transform.position, Quaternion.identity);
            Destroy(particle, 5.5f);
            Destroy(gameObject);
        }
    }

    public void SetGracePeriod(float time)
    {
        StartCoroutine(GracePeriod(time));
    }

    IEnumerator GracePeriod(float time)
    {
        GetComponent<Collider>().enabled = false;
        yield return new WaitForSeconds(time);
        GetComponent<Collider>().enabled = true;
    }
}
