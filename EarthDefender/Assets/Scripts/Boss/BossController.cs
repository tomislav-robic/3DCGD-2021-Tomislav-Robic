using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossController : MonoBehaviour
{
    public GameObject explosionParticle;
    public GameObject[] bulletOrigins = new GameObject[2];
    public float maxHealth = 2500f;
    public float pointsBonus = 50000f;
    public float rapidFireInterval = 0.15f;
    public float rapidFireDelay = 3f;
    public Slider healthSlider;
    public Canvas winCanvas;
    GameObject player;
    bool exploded = false;
    bool canShoot = false;
    int counter = 0;
    float health;
    float shootTime = 0f;
    DifficultyHandler difficulty;

    private void Start()
    {
        difficulty = FindObjectOfType<DifficultyHandler>();
        player = FindObjectOfType<SpaceshipController>().gameObject;
        maxHealth = Mathf.Floor(maxHealth * difficulty.diffFloat);
        pointsBonus *= difficulty.diffFloat;
        rapidFireInterval /= difficulty.diffFloat; 
        rapidFireDelay /= difficulty.diffFloat; 
        health = maxHealth;
        healthSlider.maxValue = maxHealth;
        healthSlider.value = health;
        InvokeRepeating(nameof(SetCanShoot), rapidFireDelay, rapidFireDelay);
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
    private void Update()
    {
        if (player == null) player = Resources.FindObjectsOfTypeAll<SpaceshipController>()[0].gameObject;
        if (player != null) {
            bulletOrigins[0].transform.LookAt(player.transform);
            bulletOrigins[1].transform.LookAt(player.transform);
        }
        shootTime -= Time.deltaTime;
        if (canShoot && shootTime < 0)
        {
            SoundManager.i.Play(Sounds.Shoot);
            ObjectPooler.i.SpawnFromPool(PooledObjects.EnemyBullet, bulletOrigins[0].transform.position + (Vector3.up * 2), bulletOrigins[0].transform.rotation);
            ObjectPooler.i.SpawnFromPool(PooledObjects.EnemyBullet, bulletOrigins[1].transform.position + (Vector3.up * 2), bulletOrigins[1].transform.rotation);
            shootTime += rapidFireInterval;
            counter++;
            if (counter >= 5)
            {
                canShoot = false;
                counter = 0;
            }
        }

    }
    void SetCanShoot()
    {
        canShoot = !canShoot;
        shootTime = 0f;
    }

    private void Explode(Collider other)
    {
        if (!exploded)
        {
            exploded = true;
            foreach(EnemyController enemy in FindObjectsOfType<EnemyController>())
            {
                enemy.Explode();
            }
            foreach(EnemyBullet bullet in FindObjectsOfType<EnemyBullet>())
            {
                Destroy(bullet.gameObject);
            }
            foreach(BigAsteroid bigAsteroid in FindObjectsOfType<BigAsteroid>())
            {
                bigAsteroid.Explode();
            }
            foreach (SmallAsteroid smallAsteroid in FindObjectsOfType<SmallAsteroid>())
            {
                smallAsteroid.Explode();
            }
            PointsSystem pointsSystem = FindObjectOfType<PointsSystem>();
            pointsSystem.AddPoints(pointsBonus);
            pointsSystem.SavePoints();
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            GameObject.Find("HUD").SetActive(false);
            FindObjectOfType<SpaceshipController>().enabled = false;
            foreach (WeaponController weapon in FindObjectsOfType<WeaponController>()) weapon.enabled = false;
            SpaceshipController.canPause = false;
            Instantiate(winCanvas);
            GameObject _particle = Instantiate(explosionParticle, transform.position, Quaternion.identity);
            _particle.transform.localScale = Vector3.one * 4f;
            Destroy(_particle, 5.5f);
            Destroy(gameObject);
        }
    }
}
