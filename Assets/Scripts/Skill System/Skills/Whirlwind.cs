using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Skill/Whirlwind")]
public class Whirlwind : Skill
{
    [Header("Boost Damage")]
    [SerializeField] private float dmgPersenOfAGI;
    [SerializeField] private float dmgPersenOfATK;

    [Header("Crowd Control")]
    [SerializeField] private float pushSpeed;
    [SerializeField] private float pushRange;
    private PlayerController playerController;
    private GameObject gameObject;
    private ChrDirection direction;

    public override float GetDamage(Character character)
    {
        return this.damage += dmgPersenOfAGI * character.agi + dmgPersenOfATK * character.atk;
    }

    public override void Activate(GameObject gameObject)
    {
        this.gameObject = gameObject;
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        direction = playerController.direction;
        StageManager.instance.PlayerActivatesSkill(this);
    }


    public override void HitEnemy(Collider2D other)
    {

    }

    public override void WhileHitEnemy(Collider2D other)
    {
        if (HasHitEnemy(other))
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
            switch (direction)
            {
                case ChrDirection.Right:
                    backward = gameObject.transform.right;
                    break;
                case ChrDirection.Left:
                    backward = -gameObject.transform.right;
                    break;

                case ChrDirection.Front:
                    // cuma bisa untuk mob yang ada di bawah player
                    if (mob.transform.position.y < playerController.transform.position.y)
                    {
                        backward = -gameObject.transform.up;
                    }
                    break;

                case ChrDirection.Back:
                    // cuma bisa untuk mob yang ada di atas player
                    if (mob.transform.position.y > playerController.transform.position.y)
                    {
                        backward = gameObject.transform.up;
                    }
                    break;
            }

            mob.ActivateCC(
                new CCSlide(
                    this.id,
                    pushSpeed,
                    pushRange,
                    mob.transform.position,
                    backward
                )
            );

            base.HitEnemy(other);

            // jika mob yang kena bukan target dari skill
            // misal: targetnya adalah ground enemy dan yg kena flying enemy
            //        maka flying enemy tidak akan terkena efek cc
            // if (enemyTarget != mob.GetComponent<MobController>().enemy.type)
            // {
            //     return;
            // }


        }
    }

    public override void AfterHitEnemy(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            base.AfterHitEnemy(other);
        }
    }

}
// public class Whirlwind : MonoBehaviour
// {
//     private Skill skill;
//     private GameObject player;

//     // [SerializeField] private EnemyType enemyTarget;
//     [SerializeField] private float dmgPersenOfAgi;
//     [SerializeField] private float dmgPersenOfAtk;

//     private void Start()
//     {
//         // sesuaikan damage basic attack dengan atk player
//         skill = GetComponent<SkillController>().skill;
//         player = GameObject.Find("Player");
//         PlayerController playerController = player.GetComponent<PlayerController>();

//         skill.Damage = dmgPersenOfAgi * playerController.player.agi + dmgPersenOfAtk * playerController.player.atk;
//         StageManager.instance.PlayerActivatesSkill(skill);
//     }

//     private void OnTriggerStay2D(Collider2D other)
//     {
//         if (skill.HasHitEnemy(other))
//         {
//             return;
//         }

//         if (other.CompareTag("Enemy"))
//         {
//             // // kalau damage yg diberikan sesuai dengan tipe musuh yg terkena damage
//             // // misal musuh terbang terkena damage angin bisa, tapi damage biasa tidak bisa
//             // if (!gameObject.GetComponent<SkillController>().validAttack)
//             // {
//             //     return;
//             // }

//             CrowdControlSystem mob = other.GetComponent<CrowdControlSystem>();

//             // mob.ActivateSliding(slideSpeed, slideDistance);
//             Vector2 backward = new Vector2();
//             switch (player.GetComponent<PlayerController>().direction)
//             {
//                 case ChrDirection.Right:
//                     backward = transform.right;
//                     break;
//                 case ChrDirection.Left:
//                     backward = -transform.right;
//                     break;

//                 case ChrDirection.Front:
//                     // cuma bisa untuk mob yang ada di bawah player
//                     if (mob.transform.position.y < player.transform.position.y)
//                     {
//                         backward = -transform.up;
//                     }
//                     break;

//                 case ChrDirection.Back:
//                     // cuma bisa untuk mob yang ada di atas player
//                     if (mob.transform.position.y > player.transform.position.y)
//                     {
//                         backward = transform.up;
//                     }
//                     break;
//             }

//             mob.ActivateCC(
//                 new CCSlide(
//                     skill.Id,
//                     skill.PushSpeed,
//                     skill.PushRange,
//                     mob.transform.position,
//                     backward
//                 )
//             );

//             skill.HitEnemy(other);

//             // jika mob yang kena bukan target dari skill
//             // misal: targetnya adalah ground enemy dan yg kena flying enemy
//             //        maka flying enemy tidak akan terkena efek cc
//             // if (enemyTarget != mob.GetComponent<MobController>().enemy.type)
//             // {
//             //     return;
//             // }


//         }
//     }

//     private void OnTriggerExit2D(Collider2D other)
//     {
//         if (other.CompareTag("Enemy"))
//         {
//             skill.AfterHitEnemy(other);
//         }
//     }
// }