using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "Skill/Javelin")]
public class Javeline : Skill
{
    [Header("Boost Damage")]
    [SerializeField] private float dmgPersenOfAGI;
    [Header("Level Up Value")]

    [SerializeField] private float dmgPersenOfAGIUp;

    [Header("Range Attack")]
    [SerializeField] private float radius;
    private Transform player;

    public float dmgPersenOfAGIFinal
    {
        get { return dmgPersenOfAGI + dmgPersenOfAGIUp * (level - 1); }
    }

    // [SerializeField] private float radius;
    public override string GetDescription()
    {
        string additionAGI = level > 1 ? " (+" + PersentaseToInt(dmgPersenOfAGIFinal - dmgPersenOfAGI) + "%) " : " ";

        description = "Menyerang satu musuh terbang terdekat di sekitar karakter dengan air damage sebesar " + damage + " + " + PersentaseToInt(dmgPersenOfAGI) + "%" + additionAGI + "AGI.";
        return description;
    }


    public override float GetDamage(Player player)
    {
        return this.damage + dmgPersenOfAGIFinal * player.GetAGI();
    }

    public override void Activate(GameObject gameObject)
    {

        player = GameObject.Find("Player").transform;
        GetNearestEnemy();

        if (this.lockedEnemy == null)
        {
            Debug.Log("masuk sini di destroy");
            Destroy(gameObject);
        }
        else
        {
            Payment(player);
        }
    }
    public override void OnActivated(GameObject gameObject)
    {
        if (this.lockedEnemy == null)
        {
            Destroy(gameObject);
        }
    }

    private void GetNearestEnemy()
    {
        // skill.LockedEnemy = GameObject.Find("FlyingEnemy").GetComponent<FlyingEnemy>().children[0].transform;
        List<Collider2D> enemiesInRadius = Physics2D.OverlapCircleAll(
            player.position,
            radius,
            LayerMask.GetMask("Enemy")
        ).ToList();

        float closestDistance = Mathf.Infinity;
        Transform closestEnemy = null;

        // untuk semua musuh di dalam radius
        foreach (Collider2D enemy in enemiesInRadius)
        {
            MobController mob = enemy.GetComponent<MobController>();

            if (mob.enemy.type != EnemyType.Flying)
            {
                continue;
            }

            float distanceToEnemy = Vector3.Distance(player.position, enemy.transform.position);
            if (distanceToEnemy < closestDistance)
            {
                closestDistance = distanceToEnemy;
                closestEnemy = enemy.transform;
            }
        }

        if (closestEnemy != null)
        {
            this.lockedEnemy = closestEnemy.GetComponent<FlyingEnemyShadow>().children[0].transform;
        }


    }


}
