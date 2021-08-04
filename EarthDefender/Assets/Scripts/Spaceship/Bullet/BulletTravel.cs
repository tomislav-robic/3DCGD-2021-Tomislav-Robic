using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletTravel : MonoBehaviour
{
    public float speed = 20f;
    public float destroyAfter = 10f;
    private void Start()
    {
        Destroy(gameObject, destroyAfter);
    }
    private void Update()
    {
        transform.position += transform.forward * Time.deltaTime * speed;
    }
}
