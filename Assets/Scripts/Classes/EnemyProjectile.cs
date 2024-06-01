using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
        return speed;
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
