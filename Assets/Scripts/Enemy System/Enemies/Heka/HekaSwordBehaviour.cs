using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HekaSwordBehaviour : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] Vector3 offsetPivot;
    private bool canAttack = false;
    private Transform target;
    private float speed, maxTime, timer;



    void Update()
    {
        if (canAttack)
        {

            if (timer <= 0)
            {
                animator.Play("heka_sword_end");
            }
            else
            {
                timer -= Time.deltaTime;
                Locking();
            }
        }
    }

    public void AttackNow(float speed, float maxTime)
    {
        this.speed = speed;
        this.maxTime = maxTime;

        canAttack = true;

        timer = this.maxTime;
        target = GameObject.FindWithTag("Player").transform;

        animator.Play("heka_sword_attack");
    }

    public void Locking()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed);

        // rotasikan arah hadap skill --------------------------------
        Vector2 directionToTarget = target.position - (transform.position + offsetPivot);

        float angle = Mathf.Atan2(directionToTarget.y, directionToTarget.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);

        // Terapkan rotasi pada titik pivot
        transform.rotation = targetRotation;
    }

    public void EndAnimation()
    {
        Destroy(gameObject);
    }


}