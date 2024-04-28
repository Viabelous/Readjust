using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    private Skill skill;

    private void Start()
    {
        // sesuaikan damage basic attack dengan atk player
        skill = GetComponent<SkillController>().skill;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            CrowdControlSystem mob = other.GetComponent<CrowdControlSystem>();
            Vector3 direction = -(gameObject.transform.position - mob.transform.position).normalized;
            mob.ActivateCC(
                new CCKnockBack(
                    skill.PushSpeed,
                    skill.PushRange,
                    mob.transform.position,
                    direction
                )
            );
        }
    }
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
