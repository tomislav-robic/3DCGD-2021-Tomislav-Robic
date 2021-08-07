using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigAsteroid : MonoBehaviour
{
    public GameObject explosionParticle;
    public GameObject smallAsteroid;
    public float smallAsteroidSpeed = 2f;
    public float damage = 65f;
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
        if (other.CompareTag("Bullet"))
        {
            if (!exploded)
            {
                exploded = true;
                GameObject particle = Instantiate(explosionParticle, transform.position, Quaternion.identity);
                Rigidbody asteroid1 = Instantiate(smallAsteroid, transform.position + Vector3.right * 5, Quaternion.identity).GetComponent<Rigidbody>();
                Rigidbody asteroid2 = Instantiate(smallAsteroid, transform.position - Vector3.right * 5, Quaternion.identity).GetComponent<Rigidbody>();
                asteroid1.GetComponent<SmallAsteroid>().SetGracePeriod(0.2f);
                asteroid2.GetComponent<SmallAsteroid>().SetGracePeriod(0.2f);
                asteroid1.AddForce(-Vector3.forward * 2500f * smallAsteroidSpeed, ForceMode.Impulse);
                asteroid2.AddForce(-Vector3.forward * 2500f * smallAsteroidSpeed, ForceMode.Impulse);
                Destroy(particle, 5.5f);
                Destroy(gameObject);
            }
            Destroy(other.gameObject);
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
}
