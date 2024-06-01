using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySkillController : MonoBehaviour
{
    private GameObject enemy;
    private float damage;
    private float speed;

    public GameObject GetEnemy()
    {
        return enemy;
    }

    public void SetEnemy(GameObject gameObject)
    {
        enemy = gameObject;
    }

    public void SetDamage(float damage)
    {
        this.damage = damage;
    }
    public float GetDamage()
    {
        return damage;
    }

    public void SetSpeed(float speed)
    {
        this.speed = speed;
    }
    public float GetSpeed()
    {
        return speed;
    }
}