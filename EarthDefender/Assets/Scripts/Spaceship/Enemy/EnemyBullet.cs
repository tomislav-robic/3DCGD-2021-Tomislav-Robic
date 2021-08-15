using System.Collections;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float damage = 10f;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Spaceship"))
        {
            other.GetComponentInParent<SpaceshipHealth>().TakeDamage(damage);
            transform.parent.gameObject.SetActive(false);
        }
    }

}
