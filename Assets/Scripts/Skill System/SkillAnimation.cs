using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SkillAnimation : MonoBehaviour // skill pake waktu
{

    [SerializeField]
    private string attackAnimationName, endAnimationName;

    private float timer;

    [HideInInspector]
    public bool isAttacking;

    [HideInInspector]
    public Skill skill;
    private Animator animator;

    void Start()
    {
        skill = GetComponent<SkillController>().skill;
        animator = GetComponent<Animator>();

        timer = skill.Timer;
        isAttacking = true;
    }

    // Update is called once per frame
    void Update()
    {

        switch (skill.HitType)
        {
            case SkillHitType.Once:
                break;

            case SkillHitType.Temporary:
                if (isAttacking)
                {
                    timer -= Time.deltaTime;
                }

                if (timer <= 0 && isAttacking)
                {
                    isAttacking = false;
                    animator.Play(endAnimationName);
                }
                break;
        }


    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            // kalau skill locking kena bayangan musuh terbang
            if (
                skill.MovementType == SkillMovementType.Locking &&
                other.GetComponent<MobController>().enemy.type == EnemyType.Flying
            )
            {
                return;
            }

            // biasanya tipe projectile
            if (isAttacking && skill.HitType == SkillHitType.Once)
            {
                isAttacking = false;
                animator.Play(endAnimationName);

            }
        }

        if (
            other.CompareTag("FlyingEnemy") &&
            skill.Element == Element.Air &&
            skill.MovementType == SkillMovementType.Locking
        )
        {
            // biasanya tipe projectile
            if (isAttacking && skill.HitType == SkillHitType.Once)
            {
                isAttacking = false;
                animator.Play(endAnimationName);

            }
        }
    }

    public void OnAnimationAttack()
    {
        switch (skill.HitType)
        {
            case SkillHitType.Once:
                if (isAttacking)
                {
                    animator.Play(attackAnimationName);
                }
                break;
            case SkillHitType.Temporary:
                animator.Play(attackAnimationName);
                break;
        }
    }

    public void OnAnimationEnd()
    {
        Destroy(gameObject);

    }
}
