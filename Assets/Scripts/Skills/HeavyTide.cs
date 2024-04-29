using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class HeavyTide : MonoBehaviour
{
    private Skill skill;
    [SerializeField] private float dmgPersenOfAtk;

    private void Start()
    {
        // sesuaikan damage basic attack dengan atk player
        skill = GetComponent<SkillController>().skill;
        skill.Damage += dmgPersenOfAtk * GameObject.Find("Player").GetComponent<PlayerController>().player.atk;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (skill.HasHitEnemy(other))
        {
            return;
        }

        if (other.CompareTag("Enemy"))
        {
            CrowdControlSystem mob = other.GetComponent<CrowdControlSystem>();
            Vector3 direction = -(gameObject.transform.position - mob.transform.position).normalized;
            mob.ActivateCC(
                new CCKnockBack(
                    skill.Id,
                    skill.PushSpeed,
                    skill.PushRange,
                    mob.transform.position,
                    direction
                )
            );

            skill.HitEnemy(other);
        }
    }

    // private void OnTriggerExit2D(Collider2D other)
    // {
    //     if (other.CompareTag("Enemy"))
    //     {
    //         skill.AfterHitEnemy(other);
    //     }
    // }
}

// [CreateAssetMenu]
// public class Explosion : Skill
// {
//     [Header("Crowd Control")]
//     [SerializeField] private float knockBackSpeed;
//     [SerializeField] private float knockBackDistance;

//     public override void Activate(GameObject gameObject)
//     {
//     }

//     public override void HitEnemy(GameObject gameObject, Collider2D other)
//     {
//         base.HitEnemy(gameObject, other);
//         if (other.CompareTag("Enemy"))
//         {
//             CrowdControlSystem mob = other.GetComponent<CrowdControlSystem>();
//             Vector3 direction = -(gameObject.transform.position - mob.transform.position).normalized;
//             mob.ActivateCC(
//                 new CCKnockBack(
//                     knockBackSpeed,
//                     knockBackDistance,
//                     mob.transform.position,
//                     direction
//                 )
//             );
//         }
//     }

//     public override void AfterHitEnemy(GameObject gameObject, Collider2D other)
//     {
//         base.AfterHitEnemy(gameObject, other);
//     }

// }


// [CreateAssetMenu]
// public class HighTide : Skill
// {
//     [Header("Crowd Control")]
//     [SerializeField] private float knockBackSpeed;
//     [SerializeField] private float knockBackDistance;

//     public override void Activate(GameObject gameObject)
//     {
//         // gameObject.transform.position = GameObject.Find("Player").transform.position;
//     }

//     public override void HitEnemy(GameObject gameObject, Collider2D other)
//     {
//         base.HitEnemy(gameObject, other);
//         if (other.CompareTag("Enemy"))
//         {
//             CrowdControlSystem mob = other.GetComponent<CrowdControlSystem>();
//             Vector3 direction = -(gameObject.transform.position - mob.transform.position).normalized;
//             mob.ActivateCC(
//                 new CCKnockBack(
//                     knockBackSpeed,
//                     knockBackDistance,
//                     mob.transform.position,
//                     direction
//                 )
//             );
//             // mob.ActivateKnockBack(knockBackSpeed, knockBackDistance, gameObject.transform.position);
//         }
//     }

//     public override void AfterHitEnemy(GameObject gameObject, Collider2D other)
//     {
//         base.AfterHitEnemy(gameObject, other);
//     }


// }
