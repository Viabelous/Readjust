using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Whirlwind : MonoBehaviour
{
    private Skill skill;
    private GameObject player;

    // [SerializeField] private EnemyType enemyTarget;
    [SerializeField] private float dmgPersenOfAgi;
    [SerializeField] private float dmgPersenOfAtk;

    private void Start()
    {
        // sesuaikan damage basic attack dengan atk player
        skill = GetComponent<SkillController>().skill;
        player = GameObject.Find("Player");
        PlayerController playerController = player.GetComponent<PlayerController>();

        skill.Damage = dmgPersenOfAgi * playerController.player.agi + dmgPersenOfAtk * playerController.player.atk;
        StageManager.instance.PlayerActivatesSkill(skill);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (skill.HasHitEnemy(other))
        {
            return;
        }

        if (other.CompareTag("Enemy"))
        {
            // // kalau damage yg diberikan sesuai dengan tipe musuh yg terkena damage
            // // misal musuh terbang terkena damage angin bisa, tapi damage biasa tidak bisa
            // if (!gameObject.GetComponent<SkillController>().validAttack)
            // {
            //     return;
            // }

            CrowdControlSystem mob = other.GetComponent<CrowdControlSystem>();

            // mob.ActivateSliding(slideSpeed, slideDistance);
            Vector2 backward = new Vector2();
            switch (player.GetComponent<PlayerController>().direction)
            {
                case ChrDirection.Right:
                    backward = transform.right;
                    break;
                case ChrDirection.Left:
                    backward = -transform.right;
                    break;

                case ChrDirection.Front:
                    // cuma bisa untuk mob yang ada di bawah player
                    if (mob.transform.position.y < player.transform.position.y)
                    {
                        backward = -transform.up;
                    }
                    break;

                case ChrDirection.Back:
                    // cuma bisa untuk mob yang ada di atas player
                    if (mob.transform.position.y > player.transform.position.y)
                    {
                        backward = transform.up;
                    }
                    break;
            }

            mob.ActivateCC(
                new CCSlide(
                    skill.Id,
                    skill.PushSpeed,
                    skill.PushRange,
                    mob.transform.position,
                    backward
                )
            );

            skill.HitEnemy(other);

            // jika mob yang kena bukan target dari skill
            // misal: targetnya adalah ground enemy dan yg kena flying enemy
            //        maka flying enemy tidak akan terkena efek cc
            // if (enemyTarget != mob.GetComponent<MobController>().enemy.type)
            // {
            //     return;
            // }


        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            skill.AfterHitEnemy(other);
        }
    }


}