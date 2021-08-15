using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WeaponController : MonoBehaviour
{
    public float speed = 40f;
    public float shootDelay = 3f;
    public GameObject bullet;
    bool canShoot = true;
    Transform bulletOrigin;
    DifficultyHandler difficulty;
    ObjectPooler objectPooler;

    private void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoad;
    }

    private void OnSceneLoad(Scene scene, LoadSceneMode sceneMode)
    {
        objectPooler = ObjectPooler.i;
    }

    private void Start()
    {
        objectPooler = ObjectPooler.i;
        bulletOrigin = transform.Find("BulletOrigin");
        difficulty = FindObjectOfType<DifficultyHandler>();
        shootDelay *= difficulty.diffFloat;
    }

    private void Update()
    {
        if ((Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space)) && canShoot && Time.timeScale > 0f)
        {
            SoundManager.i.Play(Sounds.Shoot);
            GameObject _bullet = objectPooler.SpawnFromPool(PooledObjects.Bullet, bulletOrigin.position, bulletOrigin.rotation);
            _bullet.transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), Mathf.Abs(transform.localScale.y), Mathf.Abs(transform.localScale.z));
            canShoot = false;
            StartCoroutine(Delay(shootDelay));
        }
    }

    IEnumerator Delay(float delay)
    {
        yield return new WaitForSeconds(delay);
        canShoot = true;
    }
}
