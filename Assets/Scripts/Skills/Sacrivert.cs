using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sacrivert : MonoBehaviour
{
    private Skill skill;
    private GameObject player;

    private void Start()
    {
        skill = GetComponent<SkillController>().skill;
        // playerController = GameObject.Find("Player").GetComponent<PlayerController>();

        player = GameObject.Find("Player");
        BuffSystem buffSystem = player.GetComponent<BuffSystem>();

        buffSystem.ActivateBuff(
           new Buff(
                BuffType.Mana,
                skill.Value,
                skill.Timer
            )
        );

        // skill = GetComponent<SkillController>().skill;
        // playerController = GameObject.Find("Player").GetComponent<PlayerController>();

        // // float result = skill.Persentase * playerController.player.hp;
        // float result = skill.Value + playerController.player.mana;

        // if (result > playerController.player.maxMana)
        // {
        //     playerController.player.mana = playerController.player.maxMana;
        // }
        // else
        // {
        //     playerController.player.mana += skill.Value;
        // }
    }
}
// [CreateAssetMenu]
// public class Sacrivert : Skill
// {
//     private PlayerController playerController;
//     public float persentase;

//     public override void Activate(GameObject gameObject)
//     {
//         playerController = GameObject.Find("Player").GetComponent<PlayerController>();

//         float result = persentase * playerController.player.hp;

//         if (result > playerController.player.maxMana)
//         {
//             playerController.player.mana = playerController.player.maxMana;
//         }
//         else
//         {
//             playerController.player.mana += result;
//         }

//     }


// }
