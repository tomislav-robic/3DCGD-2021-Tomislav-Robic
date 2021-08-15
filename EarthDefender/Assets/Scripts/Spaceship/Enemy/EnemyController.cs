using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    public GameObject explosionParticle;
    public GameObject bullet;
    public float maxHealth = 100f;
    public float shootDelay = 2f;
    public float initialDelay = 0f;
    public float pointsBonus = 5000f;
    public int counterOffset = 0;
    public bool isBoss = false;
    public List<Transform> bulletOrigins;
    public List<Transform> lookAt;
    public Slider healthSlider;
    Transform currLookAt;
    int counter = 0;
    bool canShoot = false;
    bool exploded = false;
    float health;
    DifficultyHandler difficulty;
    ObjectPooler objectPooler;

    private void Awake()
    {
        counter += counterOffset;
        currLookAt = lookAt[1];
        StartCoroutine(Delay(shootDelay + initialDelay));
    }
    private void Start()
    {
        objectPooler = ObjectPooler.i;
        lookAt[0] = FindObjectOfType<SpaceshipController>().transform;
        difficulty = FindObjectOfType<DifficultyHandler>();
        maxHealth = Mathf.Floor(maxHealth * difficulty.diffFloat);
        pointsBonus *= difficulty.diffFloat;
        health = maxHealth;
        healthSlider.maxValue = maxHealth;
        healthSlider.value = health;
    }

    private void Update()
    {
        if (canShoot)
        {
            SoundManager.i.Play(Sounds.Shoot);
            if (lookAt[0] == null) lookAt[0] = FindObjectOfType<SpaceshipController>().transform;
            GameObject _bullet1 = objectPooler.SpawnFromPool(PooledObjects.EnemyBullet, bulletOrigins[0].position, bulletOrigins[0].rotation);
            GameObject _bullet2 = objectPooler.SpawnFromPool(PooledObjects.EnemyBullet, bulletOrigins[1].position, bulletOrigins[1].rotation);
            _bullet1.transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), Mathf.Abs(transform.localScale.y), Mathf.Abs(transform.localScale.z));
            _bullet2.transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), Mathf.Abs(transform.localScale.y), Mathf.Abs(transform.localScale.z));
            canShoot = false;
            counter++;
            if (counter % 3 == 0 && lookAt[0] != null) currLookAt = lookAt[0];
            else currLookAt = lookAt[1];
            StartCoroutine(Delay(shootDelay));
        }
    }

    IEnumerator Delay(float delay)
    {
        yield return new WaitForSeconds(delay);
        canShoot = true;
    }

    void LateUpdate()
    {
        transform.LookAt(currLookAt);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            health -= 10f;
            if (health <= 0f)
            {
                health = 0f;
                Explode(other);
            }
            else exploded = false;
            healthSlider.value = health;
            other.transform.parent.gameObject.SetActive(false);
        }
    }

    void Explode(Collider other)
    {
        if (!exploded && !isBoss)
        {
            exploded = true;
            FindObjectOfType<PointsSystem>().AddPoints(pointsBonus);
            SoundManager.i.Play(Sounds.Explosion);
            GameObject _particle = Instantiate(explosionParticle, transform.position, Quaternion.identity);
            Destroy(_particle, 5.5f);
            Destroy(gameObject);
        } else if (!exploded && isBoss)
        {
            exploded = true;
            FindObjectOfType<PointsSystem>().AddPoints(pointsBonus);
            SoundManager.i.Play(Sounds.Explosion);
            GameObject _particle = Instantiate(explosionParticle, transform.position, Quaternion.identity);
            Destroy(_particle, 5.5f);
            health = maxHealth;
            healthSlider.value = health;
            counter = counterOffset;
            currLookAt = lookAt[1];
            canShoot = false;
            StopAllCoroutines();
            StartCoroutine(Delay(shootDelay + initialDelay));
            GetComponent<Animator>().Play("Entry");
        }
        other.transform.parent.gameObject.SetActive(false);
    }

    public void Explode()
    {
        exploded = true;
        SoundManager.i.Play(Sounds.Explosion);
        FindObjectOfType<PointsSystem>().AddPoints(pointsBonus);
        GameObject _particle = Instantiate(explosionParticle, transform.position, Quaternion.identity);
        Destroy(_particle, 5.5f);
        Destroy(gameObject);
    }
}
