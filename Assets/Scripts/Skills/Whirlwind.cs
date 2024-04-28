using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Whirlwind : MonoBehaviour
{
    private Skill skill;
    private GameObject player;

    private void Start()
    {
        // sesuaikan damage basic attack dengan atk player
        skill = GetComponent<SkillController>().skill;
        player = GameObject.Find("Player");
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (skill.HasHitEnemy(other))
        {
            return;
        }

        if (other.CompareTag("Enemy"))
        {
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
                    skill.PushSpeed,
                    skill.PushRange,
                    mob.transform.position,
                    backward
                )
            );

            skill.HitEnemy(other);
        }
    }


}
// [CreateAssetMenu]
// public class Whirlwind : Skill
// {
//     [Header("Crowd Control")]
//     [SerializeField] private float slideSpeed;
//     [SerializeField] private float slideDistance;

//     private GameObject player;
//     private ChrDirection direction;
//     // private SpriteRenderer spriteRenderer;
//     private Transform transform;

//     public override void Activate(GameObject gameObject)
//     {
//         transform = gameObject.transform;
//         player = GameObject.Find("Player");
//         direction = player.GetComponent<PlayerController>().direction;
//     }


//     public override void HitEnemy(GameObject gameObject, Collider2D other)
//     {
//         base.HitEnemy(gameObject, other);
//         if (other.CompareTag("Enemy"))
//         {

//             CrowdControlSystem mob = other.GetComponent<CrowdControlSystem>();
//             // mob.ActivateSliding(slideSpeed, slideDistance);
//             Vector2 backward = new Vector2();
//             switch (direction)
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
//                     slideSpeed,
//                     slideDistance,
//                     mob.transform.position,
//                     backward
//                 )
//             );

//         }

//     }


//     public override void AfterHitEnemy(GameObject gameObject, Collider2D other)
//     {
//         base.AfterHitEnemy(gameObject, other);
//     }

// }
