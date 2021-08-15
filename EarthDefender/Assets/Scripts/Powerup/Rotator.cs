using UnityEngine;

public class Rotator : MonoBehaviour
{
    public float rotationSpeed = 40f;
    void Update()
    {
        transform.Rotate(-Vector3.up * rotationSpeed * Time.deltaTime);
    }
}
