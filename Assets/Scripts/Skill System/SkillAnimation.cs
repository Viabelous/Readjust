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
        skill = GetComponent<SkillController>().playerSkill;
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
                if (skill.Timer > 0)
                {
                    if (isAttacking)
                    {
                        timer -= Time.deltaTime;
                    }

                    if (timer <= 0 && isAttacking)
                    {
                        isAttacking = false;
                        animator.Play(endAnimationName);
                    }
                }
                break;
        }


    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            // kalau skill bukan angin kena bayangan musuh terbang
            if (
                skill.Element != Element.Air &&
                other.GetComponent<MobController>().enemy.type == EnemyType.Flying
            )
            {
                return;
            }

            // kalau skill locking kena bayangan musuh terbang atau
            // kalau skill kena org yg bukan di-locknya
            if (
                skill.MovementType == SkillMovementType.Locking &&
                other.GetComponent<MobController>().enemy.type == EnemyType.Flying ||

                skill.MovementType == SkillMovementType.Locking &&
                skill.LockedEnemy != other.transform
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
            skill.MovementType == SkillMovementType.Locking &&
            skill.LockedEnemy == other.transform
        )
        {
            print("masuk sini gak");
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
