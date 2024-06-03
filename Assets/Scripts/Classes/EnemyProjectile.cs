using UnityEngine;

[CreateAssetMenu(menuName = "Enemy Projectile")]

public class EnemyProjectile : ScriptableObject
{
    [SerializeField] private float damage;
    [SerializeField] private float speed;
    [SerializeField] private float lockingTime;
    [SerializeField] private float timeInterval;


    public float GetDamage()
    {
        return damage;
    }

    public float GetSpeed()
    {
        return speed;
    }

    public float GetLockingTime()
    {
        return lockingTime;
    }
    public float GetTimeInterval()
    {
        return timeInterval;
    }
}
