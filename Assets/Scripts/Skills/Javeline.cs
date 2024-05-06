using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Javeline : MonoBehaviour
{
    private Skill skill;
    private PlayerController playerController;

    [SerializeField] private float dmgPersenOfAgi;
    // [SerializeField] private float radius;
    private void Start()
    {
        skill = GetComponent<SkillController>().skill;
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        skill.Damage += dmgPersenOfAgi * playerController.player.agi;
        GetNearestEnemy();
    }

    private void GetNearestEnemy()
    {
        Collider2D[] enemiesInRadius = Physics2D.OverlapCircleAll(transform.position, skill.MovementRange, LayerMask.GetMask("Enemy"));

        float closestDistance = Mathf.Infinity;
        // untuk semua musuh di dalam radius
        foreach (Collider2D enemy in enemiesInRadius)
        {
            // Menghitung jarak antara objek ini dengan musuh dalam loop
            float distanceToEnemy = Vector3.Distance(playerController.transform.position, enemy.transform.position);

            // Memeriksa apakah musuh saat ini memiliki jarak lebih dekat
            if (distanceToEnemy < closestDistance)
            {
                closestDistance = distanceToEnemy;
                skill.LockedEnemy = enemy.GetComponent<FlyingEnemy>().children[0].transform;
            }

        }

        if (skill.LockedEnemy != null)
        {
            print("Namanya: " + skill.LockedEnemy.name);

        }
        else
        {
            print("Ga dapet");
        }

        // if (closestObject != null)
        // {
        //     skill.LockedEnemy = closestObject.GetComponent<FlyingEnemy>().children[0].transform;
        //     Debug.Log("Objek terdekat: " + closestObject.name + ", jarak: " + closestDistance);
        // }
        // else
        // {
        //     Debug.Log("Tidak ada objek terdekat ditemukan.");
        // }


        // GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        // // Inisialisasi variabel untuk menyimpan jarak terdekat dan musuh terdekat
        // float closestDistance = Mathf.Infinity;

        // // Looping untuk mencari musuh terdekat
        // foreach (GameObject enemy in enemies)
        // {
        //     MobController mob = enemy.GetComponent<MobController>();

        //     if (mob.enemy.type != EnemyType.Flying)
        //     {
        //         continue;
        //     }

        //     // Menghitung jarak antara objek ini dengan musuh dalam loop
        //     float distanceToEnemy = Vector3.Distance(playerController.transform.position, enemy.transform.position);

        //     // Memeriksa apakah musuh saat ini memiliki jarak lebih dekat
        //     if (distanceToEnemy < closestDistance)
        //     {
        //         closestDistance = distanceToEnemy;
        //         skill.LockedEnemy = enemy.GetComponent<MobController>();
        //     }
        // }
    }

}