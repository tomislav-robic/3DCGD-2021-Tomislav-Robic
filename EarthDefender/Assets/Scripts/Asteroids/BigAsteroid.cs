using UnityEngine;

public class BigAsteroid : MonoBehaviour, IPooledObject
{
    public GameObject explosionParticle;
    public GameObject smallAsteroid;
    public float smallAsteroidSpeed = 2f;
    public float damage = 65f;
    public float pointsBonus = 300f;
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
        smallAsteroidSpeed *= difficulty.diffFloat;
        pointsBonus *= difficulty.diffFloat;
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
                SoundManager.i.Play(Sounds.Explosion);
                FindObjectOfType<PointsSystem>().AddPoints(pointsBonus);
                GameObject particle = objectPooler.SpawnFromPool(PooledObjects.BigExplosion, transform.position, Quaternion.identity);
                particle.GetComponent<ParticleSystem>().Play();
                Rigidbody asteroid1 = objectPooler.SpawnFromPool(PooledObjects.SmallAsteroid, transform.position + Vector3.right * 5, Quaternion.identity).GetComponent<Rigidbody>();
                Rigidbody asteroid2 = objectPooler.SpawnFromPool(PooledObjects.SmallAsteroid, transform.position - Vector3.right * 5, Quaternion.identity).GetComponent<Rigidbody>();
                asteroid1.GetComponent<SmallAsteroid>().SetGracePeriod(0.2f);
                asteroid2.GetComponent<SmallAsteroid>().SetGracePeriod(0.2f);
                asteroid1.velocity = -Vector3.forward * 25f * smallAsteroidSpeed;
                asteroid2.velocity = -Vector3.forward * 25f * smallAsteroidSpeed;
                gameObject.SetActive(false);
                OnDestroy();
            }
            other.transform.parent.gameObject.SetActive(false);
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
            GameObject particle = objectPooler.SpawnFromPool(PooledObjects.BigExplosion, transform.position, Quaternion.identity);
            particle.GetComponent<ParticleSystem>().Play();
            gameObject.SetActive(false);
            OnDestroy();
        }
        if (other.CompareTag("Enemy") && !exploded)
        {
            Explode();
        }
    }
    public void Explode()
    {
        exploded = true;
        SoundManager.i.Play(Sounds.Explosion);
        FindObjectOfType<PointsSystem>().AddPoints(pointsBonus);
        GameObject particle = objectPooler.SpawnFromPool(PooledObjects.BigExplosion, transform.position, Quaternion.identity);
        particle.GetComponent<ParticleSystem>().Play();
        gameObject.SetActive(false);
        OnDestroy();
    }
}
