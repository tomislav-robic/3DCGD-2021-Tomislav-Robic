using System.Collections;
using UnityEngine;

public class SmallAsteroid : MonoBehaviour, IPooledObject
{
    public GameObject explosionParticle;
    public float damage = 20f;
    public float pointsBonus = 100f;
    bool exploded = false;
    DifficultyHandler difficulty;
    ObjectPooler objectPooler;
    public void OnObjectSpawn()
    {
        Spawner.spawnedAsteroids++;
        exploded = false;
    }
    private void Start()
    {
        objectPooler = ObjectPooler.i;
        difficulty = FindObjectOfType<DifficultyHandler>();
        damage *= difficulty.diffFloat;
        pointsBonus *= difficulty.diffFloat;
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
            SoundManager.i.Play(Sounds.Explosion);
            FindObjectOfType<PointsSystem>().AddPoints(pointsBonus);
            GameObject particle = objectPooler.SpawnFromPool(PooledObjects.SmallExplosion, transform.position, Quaternion.identity);
            particle.GetComponent<ParticleSystem>().Play();
            other.transform.parent.gameObject.SetActive(false);
            gameObject.SetActive(false);
            OnDestroy();
        }
        if (other.CompareTag("Earth") && !exploded)
        {
            exploded = true;
            SoundManager.i.Play(Sounds.Explosion);
            other.GetComponentInParent<EarthHealth>().TakeDamage(damage);
            gameObject.SetActive(false);
            OnDestroy();
        }
        if (other.CompareTag("Spaceship") && !exploded)
        {
            exploded = true;
            SoundManager.i.Play(Sounds.Explosion);
            other.GetComponentInParent<SpaceshipHealth>().TakeDamage(damage);
            GameObject particle = objectPooler.SpawnFromPool(PooledObjects.SmallExplosion, transform.position, Quaternion.identity);
            particle.GetComponent<ParticleSystem>().Play();
            gameObject.SetActive(false);
            OnDestroy();
        }
        if (other.CompareTag("Enemy") && !exploded)
        {
            Explode();
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

    public void Explode()
    {
        exploded = true;
        SoundManager.i.Play(Sounds.Explosion);
        FindObjectOfType<PointsSystem>().AddPoints(pointsBonus);
        GameObject particle = objectPooler.SpawnFromPool(PooledObjects.SmallExplosion, transform.position, Quaternion.identity);
        particle.GetComponent<ParticleSystem>().Play();
        gameObject.SetActive(false);
        OnDestroy();
    }
}
