using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HekaSwordBehaviour : MonoBehaviour
{
    Animator animator;
    [SerializeField] Vector3 offsetPivot;
    private bool canAttack = false;
    private Transform target;
    private float speed, lifeTime, lifeTimer, delay, delayTimer;

    private EnemySkillController enemySkillController;

    void Start()
    {
        canAttack = false;
        lifeTimer = 0;
        animator = GetComponent<Animator>();

        enemySkillController = GetComponent<EnemySkillController>();
    }

    void Update()
    {
        // print("can attack = " + canAttack);
        if (canAttack)
        {
            if (delayTimer <= 0)
            {
                if (lifeTimer <= 0)
                {
                    animator.SetTrigger("End");
                }
                else
                {
                    lifeTimer -= Time.deltaTime;
                    Locking();
                }
            }
            else
            {
                delayTimer -= Time.deltaTime;
            }
        }
    }

    public void AttackNow(GameObject heka, float damage, float speed, float lifeTime, float delay)
    {
        enemySkillController.SetEnemy(heka);
        enemySkillController.SetDamage(damage);

        animator.SetTrigger("Attack");

        this.speed = speed;
        this.lifeTime = lifeTime;
        this.delay = delay;
        delayTimer = this.delay;

        canAttack = true;

        lifeTimer = this.lifeTime;
        target = GameObject.FindWithTag("Player").transform;

    }



    public void Locking()
    {
        if (target == null)
        {
            return;
        }

        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

        // rotasikan arah hadap skill --------------------------------
        // transform.up = target.position - transform.position;
        transform.up = (target.position - transform.position).normalized;
        // Vector2 direction = (target.position - transform.position).normalized;

        // float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle));
        // transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 200 * Time.deltaTime);

    }

    public void Idle()
    {
        animator.SetTrigger("Idle");
    }

    public void EndAnimation()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // animator.SetTrigger("End");
            Destroy(gameObject);
        }
    }


}