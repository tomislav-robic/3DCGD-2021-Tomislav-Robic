using UnityEngine;

[System.Serializable]
public class Pool
{
    public string tag;
    public GameObject prefab;
    public int size;
}

public struct PooledObjects
{
    public static string Bullet = "Bullet";
    public static string BigAsteroid = "BigAsteroid";
    public static string SmallAsteroid = "SmallAsteroid";
    public static string EnemyBullet = "EnemyBullet";
    public static string BigExplosion = "BigExplosion";
    public static string SmallExplosion = "SmallExplosion";
}