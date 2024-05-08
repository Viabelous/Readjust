using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Javeline : MonoBehaviour
{
    private Skill skill;
    private PlayerController playerController;
    private Animator animator;

    [SerializeField] private float dmgPersenOfAgi;
    // [SerializeField] private float radius;
    private void Start()
    {
        skill = GetComponent<SkillController>().skill;
        animator = GetComponent<Animator>();
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        skill.Damage += dmgPersenOfAgi * playerController.player.agi;

        GetNearestEnemy();

        if (skill.LockedEnemy == null)
        {
            Destroy(gameObject);
        }
        else
        {
            StageManager.instance.PlayerActivatesSkill(skill);
        }
    }

    private void GetNearestEnemy()
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

            if (mob.enemy.type != EnemyType.Flying)
            {
                continue;
            }

            // Menghitung jarak antara objek ini dengan musuh dalam loop
            float distanceToEnemy = Vector3.Distance(playerController.transform.position, enemy.transform.position);

            // Memeriksa apakah musuh saat ini memiliki jarak lebih dekat
            if (distanceToEnemy < closestDistance)
            {
                closestDistance = distanceToEnemy;
                skill.LockedEnemy = enemy.GetComponent<FlyingEnemyShadow>().children[0].transform;
            }
        }

        if (skill.LockedEnemy == null)
        {
            print("Ga dapet");
            // print("Namanya: " + skill.LockedEnemy.name);
        }

    }

}