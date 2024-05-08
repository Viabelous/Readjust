using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Nexus : MonoBehaviour
{
    public Skill skill;
    private PlayerController playerController;

    private void Start()
    {
        skill = GetComponent<SkillController>().skill;

        playerController = GameObject.Find("Player").GetComponent<PlayerController>();

        GetNearestEnemyInFrontOfPlayer();

        if (skill.LockedEnemy != null)
        {
            BuffSystem buffSystem = playerController.GetComponent<BuffSystem>();

            buffSystem.ActivateBuff(
               new Buff(
                    skill.Id,
                    BuffType.Nexus,
                    0,
                    skill.Timer
                )
            );

            StageManager.instance.PlayerActivatesSkill(skill);
        }

        else
        {
            Destroy(gameObject);
        }
    }


    private void GetNearestEnemyInFrontOfPlayer()
    {
        // skill.LockedEnemy = GameObject.Find("FlyingEnemy").GetComponent<FlyingEnemy>().children[0].transform;

        Collider2D[] enemiesInRadius = Physics2D.OverlapCircleAll(
            playerController.transform.position,
            skill.MovementRange,
            LayerMask.GetMask("Enemy")
        );

        float closestDistance = Mathf.Infinity;

        // untuk semua musuh di dalam radius
        foreach (Collider2D enemy in enemiesInRadius)
        {
            MobController mob = enemy.GetComponent<MobController>();

            if (mob.enemy.type == EnemyType.Flying)
            {
                continue;
            }

            // Menghitung jarak antara objek ini dengan musuh dalam loop
            float distanceToEnemy = Vector3.Distance(playerController.transform.position, enemy.transform.position);

            // Memeriksa apakah musuh saat ini memiliki jarak lebih dekat
            if (distanceToEnemy < closestDistance)
            {
                closestDistance = distanceToEnemy;
                skill.LockedEnemy = enemy.transform;
            }
        }

        if (skill.LockedEnemy == null)
        {
            print("Ga dapet");
            // print("Namanya: " + skill.LockedEnemy.name);
        }

    }

}