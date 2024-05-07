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

        if (
                   skill.LockedEnemy == null ||
                   skill.LockedEnemy != null && !ReferenceEquals(skill.LockedEnemy, null)
               )
        {
            switch (playerController.direction)
            {
                case ChrDirection.Right:
                    transform.position = playerController.transform.position + new Vector3(1, 0, 0);
                    transform.rotation = transform.rotation * Quaternion.Euler(0, 0, 0);
                    break;
                case ChrDirection.Left:
                    transform.position = playerController.transform.position + new Vector3(-1, 0, 0);
                    transform.rotation = transform.rotation * Quaternion.Euler(0, 0, -180);
                    break;
                case ChrDirection.Front:
                    transform.position = playerController.transform.position + new Vector3(0, -1, 0);
                    transform.rotation = transform.rotation * Quaternion.Euler(0, 0, -90);
                    break;
                case ChrDirection.Back:
                    transform.position = playerController.transform.position + new Vector3(0, 1, 0);
                    transform.rotation = transform.rotation * Quaternion.Euler(0, 0, 90);
                    break;
            }

            animator.Play("javeline_failed");
        }
    }

    private void Update()
    {


        // // Menghitung posisi berdasarkan pergerakan melingkar
        // angle += Time.deltaTime * skill.MovementSpeed * 2; // Sudut berdasarkan waktu
        // Vector2 offset = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * radius; // Offset berdasarkan sudut dan radius
        // Vector2 targetPosition = center + offset; // Posisi target berdasarkan offset

        // // Menggerakkan objek ke posisi target
        // transform.position = targetPosition;

        // // Jika sudut pergerakan mencapai 720 derajat (2 kali lingkaran), maka menghilangkan objek
        // if (angle >= Mathf.PI * 4)
        // {
        //     Destroy(gameObject);
        // }
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