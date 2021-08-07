using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public float speed = 40f;
    public float maxRotation = 30f;
    public float shootDelay = 3f;
    bool canShoot = true;
    public GameObject bullet;
    Transform bulletOrigin;

    private void Start()
    {
        bulletOrigin = transform.Find("BulletOrigin");
    }

    private void Update()
    {
        if ((Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space)) && canShoot)
        {
            GameObject _bullet = Instantiate(bullet, bulletOrigin.position, bulletOrigin.rotation);
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
