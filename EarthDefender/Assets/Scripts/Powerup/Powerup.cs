using System.Collections;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    public enum PowerupType
    {
        Health,
        Shield,
        Earth,
        Points
    }
    public PowerupType powerUpType;
    public float bonus;
    DifficultyHandler difficulty;
    PowerupTextManager powerUpText;
    bool pickedUp = false;

    private void Start()
    {
        difficulty = FindObjectOfType<DifficultyHandler>();
        Spawner.spawnedAsteroids++;
        if (GameObject.Find("PowerUpText") != null) powerUpText = GameObject.Find("PowerUpText").GetComponent<PowerupTextManager>();
        if (powerUpType != PowerupType.Points) bonus = Mathf.Floor(bonus / difficulty.diffFloat);
        else bonus = Mathf.Floor(bonus * difficulty.diffFloat);
        //Debug.Log($"Powerup {powerUpType} spawned. Spawned Asteroids: {Spawner.spawnedAsteroids}. Object: {gameObject}");
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Spaceship") && !pickedUp)
        {
            if (powerUpType == PowerupType.Health)
            {
                other.GetComponentInParent<SpaceshipHealth>().Heal(bonus);
            }
            if (powerUpType == PowerupType.Shield)
            {
                other.GetComponentInParent<SpaceshipHealth>().ReplenishShield(bonus);
            }
            if (powerUpType == PowerupType.Earth)
            {
                GameObject.Find("Earth").GetComponent<EarthHealth>().Heal(bonus);
            }
            if (powerUpType == PowerupType.Points)
            {
                PointsSystem.i.AddPoints(bonus);
            }
            if (powerUpText != null) powerUpText.PopUpText($"{powerUpType} +{bonus}");
            pickedUp = true;
            Destroy(gameObject);
        }
        if (other.CompareTag("Bullet"))
        {
            if (other.transform.parent.gameObject != null) other.transform.parent.gameObject.SetActive(false);
            Destroy(gameObject);
        }
        if (other.CompareTag("Enemy"))
        {
            if (other.GetComponent<BigAsteroid>() != null) other.GetComponent<BigAsteroid>().Explode();
            if (other.GetComponent<SmallAsteroid>() != null) other.GetComponent<SmallAsteroid>().Explode();
            Destroy(gameObject);
        }
        if (other.CompareTag("Earth")) Destroy(gameObject);
    }

    private void OnDestroy()
    {
        //Debug.Log($"Powerup {powerUpType} Destroyed.");
        Spawner.spawnedAsteroids--;
        //Debug.Log($"{powerUpType}: Spawned Asteroids: {Spawner.spawnedAsteroids}.");
    }
}
