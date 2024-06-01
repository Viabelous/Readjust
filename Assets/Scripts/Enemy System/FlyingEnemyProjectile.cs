using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemyProjectile : MonoBehaviour
{
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
        // targetPos = target.position;
        timer = 0;

        transform.position = GetComponent<EnemySkillController>()
        .GetEnemy().GetComponent<FlyingEnemyShadow>().flyingEnemy.transform.position;


    }

    void Update()
    {
        timer += Time.deltaTime;

        if (onLocking && timer >= 1)
        {
            onLocking = false;
            targetPos = target.position;
            initPos = transform.position;
            initDistance = Vector3.Distance(initPos, targetPos);
        }

        speed = GetComponent<EnemySkillController>().GetSpeed();

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



    // void UpdateTargetPos()
    // {

    // }
}