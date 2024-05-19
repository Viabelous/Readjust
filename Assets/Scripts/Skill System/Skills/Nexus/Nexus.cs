using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "Skill/Nexus")]
public class Nexus : Skill
{
    [Header("Skill Effect")]
    [SerializeField] private float radius;
    [SerializeField] private float dmgPersenOfTotalDmg;
    private PlayerController playerController;
    private BuffSystem buffSystem;
    private Buff buff;

    public float dmgPersenOfTotalDmgFinal
    {
        get { return dmgPersenOfTotalDmg + 0.2f * (level - 1); }
    }

    public override string GetDescription()
    {
        description = "Memberikan status {Bloodlink} pada musuh di hadapan terdekat dengan HP tertinggi. Ketika ada musuh dengan status {Bloodlink} pada stage, memukul musuh biasa akan memberikan fire damage sebesar " + dmgPersenOfTotalDmg * 100 + "% pada musuh dengan status {Bloodlink}.";
        return description;
    }

    public override void Activate(GameObject gameObject)
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();

        GetNearestAndHighestHPEnemy();

        if (this.lockedEnemy != null)
        {
            Payment(playerController.transform);

            buffSystem = playerController.GetComponent<BuffSystem>();
            buff = new Buff(
                    this.id,
                    this.name,
                    BuffType.Custom,
                    0,
                    this.timer
                );
            buffSystem.ActivateBuff(buff);
        }

        else
        {
            Destroy(gameObject);
        }
    }


    public override void OnActivated(GameObject gameObject)
    {
        if (!buffSystem.CheckBuff(buff))
        {
            Destroy(gameObject);
        }
    }

    private void GetNearestAndHighestHPEnemy()
    {
        // skill.LockedEnemy = GameObject.Find("FlyingEnemy").GetComponent<FlyingEnemy>().children[0].transform;

        Collider2D[] enemiesInRadius = Physics2D.OverlapCircleAll(
            playerController.transform.position,
            radius,
            LayerMask.GetMask("Enemy")
        );

        Dictionary<Transform, float[]> enemies = new Dictionary<Transform, float[]>();

        foreach (Collider2D enemy in enemiesInRadius)
        {
            MobController mob = enemy.GetComponent<MobController>();

            if (mob.enemy.type != EnemyType.Ground)
            {
                // flyingEnemyIndexes.Add(i);
                continue;
            }

            float distanceToEnemy = Vector3.Distance(playerController.transform.position, enemy.transform.position);
            float[] enemyProperty = { distanceToEnemy, mob.enemy.hp };
            enemies.Add(enemy.transform, enemyProperty);
        }

        if (enemies.Count == 0)
        {
            return;
        }

        enemies.OrderBy(dict => dict.Value[0]).ToDictionary(pair => pair.Key, pair => pair.Value);
        enemies.OrderByDescending(dict => dict.Value[1]).ToDictionary(pair => pair.Key, pair => pair.Value);
        this.lockedEnemy = enemies.Keys.ElementAt(0);
    }


}