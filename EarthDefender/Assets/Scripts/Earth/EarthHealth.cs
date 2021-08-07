using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EarthHealth : MonoBehaviour
{
    public float maxHealth = 200f;

    public static float health;

    Slider healthSlider;

    private void Awake()
    {
        healthSlider = GameObject.Find("EarthHealth").GetComponent<Slider>();
        health = maxHealth;
    }
    
    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0f)
        {
            health = 0f;
            Die();
        }
        healthSlider.value = health;
    }

    public void Die()
    {
        Debug.Log("Earth destroyed");
    }
}
