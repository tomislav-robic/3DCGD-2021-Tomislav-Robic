using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceshipController : MonoBehaviour
{
    public float speed = 1f;
    public float maxDeviation = 10f;

    private void Update()
    {
        if (Mathf.Abs(transform.position.x) <= maxDeviation)
        {
            float moveX = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
            Vector3 moveVector = transform.right * moveX;
            transform.position += moveVector;
        }
        if (transform.position.x >= maxDeviation) transform.position = new Vector3(maxDeviation, transform.position.y, transform.position.z);
        if (transform.position.x <= -maxDeviation) transform.position = new Vector3(-maxDeviation, transform.position.y, transform.position.z);
    }
}
