// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class FlyingEnemy : MonoBehaviour
// {
//     [HideInInspector]
//     public bool isTriggeredByDamage = false;
//     public Collider2D triggeredDamage;


//     private SpriteRenderer spriteRenderer;
//     private MobController mobController;
//     private Transform player;
//     void Start()
//     {
//         player = GameObject.FindWithTag("Player").transform;
//         spriteRenderer = GetComponent<SpriteRenderer>();
//         mobController = GetComponent<MobController>();
//     }

//     // Update is called once per frame
//     void Update()
//     {
//         // if (!mobController.onSkillTrigger && spriteRenderer.sortingLayerName != "Chr Front")
//         // {
//         //     spriteRenderer.sortingLayerName = "Chr Front";
//         // }

//         // if (Vector3.Distance(transform.position, player.position) <= mobController.enemy.GetDistanceToPlayer())
//         // {
//         //     mobController.movementEnabled = false;
//         // }
//         // else
//         // {
//         //     mobController.movementEnabled = true;
//         // }
//     }

//     private void OnTriggerEnter2D(Collider2D other)
//     {
//         if (other.CompareTag("Damage"))
//         {
//             if (isTriggeredByDamage) return;


//             Skill skill = other.GetComponent<SkillController>().skill;
//             if (skill.Element != Element.Air) return;

//             if (skill.MovementType == SkillMovementType.Locking)
//             {
//                 // print("TRIGGERED");
//                 isTriggeredByDamage = true;
//                 triggeredDamage = other;
//             }
//         }
//     }

//     private void OnTriggerExit2D(Collider2D other)
//     {
//         if (other.CompareTag("Damage"))
//         {
//             Skill skill = other.GetComponent<SkillController>().skill;
//             if (skill.Element != Element.Air) return;

//             if (skill.MovementType == SkillMovementType.Locking)
//             {
//                 isTriggeredByDamage = false;
//                 triggeredDamage = null;
//             }
//         }
//     }
// }
