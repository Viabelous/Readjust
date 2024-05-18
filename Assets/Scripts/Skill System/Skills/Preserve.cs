using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Skill/Preserve")]
public class Preserve : Skill
{
    [Header("Buff Value")]
    [SerializeField] private float shieldPersenOfDEF;

    public override void Activate(GameObject gameObject)
    {
        Payment(GameObject.Find("Player").transform);

        BuffSystem buffSystem = GameObject.Find("Player").GetComponent<BuffSystem>();

        PlayerController playerController = buffSystem.GetComponent<PlayerController>();
        float value = shieldPersenOfDEF * playerController.player.GetDEF();

        buffSystem.ActivateBuff(
               new Buff(
                    this.id,
                    this.name,
                    BuffType.Shield,
                    value,
                    this.timer
                )
            );
    }
}

// public class Preserve : MonoBehaviour
// {
//     private Skill skill;
//     private GameObject player;

//     private void Start()
//     {
//         skill = GetComponent<SkillController>().skill;

//         player = GameObject.Find("Player");
//         BuffSystem buffSystem = player.GetComponent<BuffSystem>();

//         PlayerController playerController = player.GetComponent<PlayerController>();
//         float value = skill.Persentase * playerController.player.def;

//         buffSystem.ActivateBuff(
//            new Buff(
//                 skill.Id,
//                 skill.Name,
//                 BuffType.Shield,
//                 value,
//                 skill.Timer
//             )
//         );
//         StageManager.instance.PlayerActivatesSkill(skill);
//     }
// }
