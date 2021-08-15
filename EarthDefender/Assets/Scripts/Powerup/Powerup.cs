using UnityEngine;

public class Powerup : MonoBehaviour
{
    public enum PowerupType
    {
        Health,
        Shield,
        Earth
    }
    public PowerupType powerUpType;
    public float bonus;
    DifficultyHandler difficulty;

    private void Start()
    {
        difficulty = FindObjectOfType<DifficultyHandler>();
        bonus /= difficulty.diffFloat;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Spaceship"))
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
            Destroy(gameObject);
        }
        if (other.CompareTag("Bullet"))
        {
            other.transform.parent.gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }

}
