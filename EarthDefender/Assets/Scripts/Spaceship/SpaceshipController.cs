using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceshipController : MonoBehaviour
{
    public float speed = 3f;
    public float rotationSpeed = 100f;
    public float maxRotation = 30f;
    bool lockedCursor = false;

    private void Start()
    {
        lockedCursor = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = lockedCursor;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (lockedCursor)
            {
                Cursor.lockState = CursorLockMode.None;
            } else
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
            lockedCursor = !lockedCursor;
            Cursor.visible = !lockedCursor;
        }
        float moveX = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        Vector3 moveVector = Vector3.right * moveX;
        transform.position += moveVector;
        transform.Rotate(new Vector3(-Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime, Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime, 0f));
        transform.eulerAngles = ClampedTargetAngles(transform.eulerAngles);
    }
    private Vector3 ClampedTargetAngles(Vector3 eulerAngles)
    {
        return new Vector3
        {
            x = Mathf.Clamp(
                (eulerAngles.x > 180) ? eulerAngles.x - 360 : eulerAngles.x,
                -maxRotation,
                maxRotation
            ),
            y = Mathf.Clamp(
                (eulerAngles.y > 180) ? eulerAngles.y - 360 : eulerAngles.y,
                -maxRotation,
                maxRotation
            ),
            z = 0f
        };
    }
}
