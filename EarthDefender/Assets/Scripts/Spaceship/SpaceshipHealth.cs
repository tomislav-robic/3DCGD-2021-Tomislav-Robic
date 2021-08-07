using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpaceshipHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    public float maxShield = 100f;

    public static float health;
    public static float shield;

    Slider healthSlider;
    Slider shieldSlider;

    private void Awake()
    {
        healthSlider = GameObject.Find("SpaceshipHealth").GetComponent<Slider>();
        shieldSlider = GameObject.Find("SpaceshipShield").GetComponent<Slider>();
        shield = maxShield;
        health = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        if (shield > 0f)
        {
            shield -= damage;
            if (shield < 0f)
            {
                health += shield;
                shield = 0f;
            }
        } else
        {
            health -= damage;
        }
        if (health <= 0f)
        {
            health = 0f;
            Die();
        }
        healthSlider.value = health;
        shieldSlider.value = shield;
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}
