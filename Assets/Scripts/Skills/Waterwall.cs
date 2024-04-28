using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waterwall : MonoBehaviour
{
    private Skill skill;

    private void Start()
    {
        // sesuaikan damage basic attack dengan atk player
        skill = GetComponent<SkillController>().skill;
        float foc = GameObject.Find("Player").GetComponent<PlayerController>().player.foc;
        skill.Damage = 0.75f * foc;
        skill.Timer = foc;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (skill.HasHitEnemy(other))
        {
            return;
        }

        if (other.CompareTag("Enemy"))
        {
            MobController mob = other.GetComponent<MobController>();
            mob.speed -= skill.Persentase * mob.speed;
            skill.HitEnemy(other);
        }

    }

    private void OnTriggerExit2D(Collider2D other)
    {
        MobController mob = other.GetComponent<MobController>();
        mob.speed = mob.enemy.movementSpeed;
        skill.AfterHitEnemy(other);
    }
}

// [CreateAssetMenu]
// public class Waterwall : Skill
// {
//     [Header("Crowd Control")]
//     [SerializeField]
//     private float slow;

//     public override void Activate(GameObject gameObject)
//     {
//         // gameObject.transform.position = GameObject.Find("Player").transform.position;

//         // // Active();
//     }

//     // public override void HitEnemyFirstTime(GameObject gameObject, Collider2D other)
//     // {
//     // }

//     public override void HitEnemy(GameObject gameObject, Collider2D other)
//     {

//         if (HasHitEnemy(other))
//         {
//             return;
//         }

//         if (other.CompareTag("Enemy"))
//         {
//             MobController mob = other.GetComponent<MobController>();
//             mob.speed -= mob.speed * slow;
//         }

//         base.HitEnemy(gameObject, other);
//     }

//     public override void AfterHitEnemy(GameObject gameObject, Collider2D other)
//     {

//         if (other.CompareTag("Enemy"))
//         {
//             MobController mob = other.GetComponent<MobController>();
//             mob.speed = mob.enemy.movementSpeed;
//         }

//         base.AfterHitEnemy(gameObject, other);

//     }


// }
