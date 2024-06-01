using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemyProjectile : MonoBehaviour
{
    [SerializeField] EnemyProjectile projectile;
    [SerializeField] EnemySkillController skillController;

    Transform target;
    Vector3 targetPos;
    float speed;

    float initDistance;
    Vector3 initPos;

    float timer;
    bool onLocking;

    void Start()
    {
        target = GameObject.FindWithTag("Player").transform;
        onLocking = true;
        timer = 0;

        speed = projectile.GetSpeed();
        skillController = GetComponent<EnemySkillController>();

        // transform.position = skillController.GetEnemy().
        // GetComponent<FlyingEnemyShadow>().flyingEnemy.transform.position;

        skillController.SetDamage(projectile.GetDamage());
        skillController.SetSpeed(projectile.GetSpeed());
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (onLocking && timer >= projectile.GetLockingTime())
        {
            onLocking = false;
            targetPos = target.position;
            initPos = transform.position;
            initDistance = Vector3.Distance(initPos, targetPos);
        }

        transform.position = Vector3.MoveTowards(
            transform.position,
            onLocking ? target.position : targetPos,
            speed * Time.deltaTime
        );

        if (!onLocking)
        {
            if (Vector3.Distance(initPos, transform.position) >= initDistance)
            {
                Destroy(gameObject);
            }
        }

    }

    public EnemyProjectile GetProjectile()
    {
        return projectile;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }

}