using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public GameObject bullet;
    Transform bulletOrigin;

    private void Start()
    {
        bulletOrigin = transform.Find("BulletOrigin");
    }

    private void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos += Camera.main.transform.forward * 30f;
        Vector3 aim = Camera.main.ScreenToWorldPoint(mousePos);
        transform.LookAt(aim);
        if (Input.GetMouseButtonDown(0))
        {
            GameObject _bullet = Instantiate(bullet, bulletOrigin.position, bulletOrigin.rotation);
            _bullet.transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), Mathf.Abs(transform.localScale.y), Mathf.Abs(transform.localScale.z)); 
        }
    }
}
