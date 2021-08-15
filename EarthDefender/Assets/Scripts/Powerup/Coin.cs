using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public float bonus = 10000f;
    DifficultyHandler difficulty;

    private void Start()
    {
        difficulty = FindObjectOfType<DifficultyHandler>();
        bonus = Mathf.Floor(bonus * difficulty.diffFloat);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Spaceship"))
        {
            PointsSystem.i.AddPoints(bonus);
            Destroy(gameObject);
        }
        if (other.CompareTag("Bullet"))
        {
            other.transform.parent.gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }
}
