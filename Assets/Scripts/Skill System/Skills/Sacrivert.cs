using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Skill/Sacrivert")]
public class Sacrivert : Skill
{
    [Header("Custom Cost")]
    [SerializeField] private float costPersenOfMaxHP;

    [Header("Buff Value")]
    [SerializeField] private float manaValue;

    public override void Activate(GameObject gameObject)
    {
        BuffSystem buffSystem = GameObject.Find("Player").GetComponent<BuffSystem>();

        float hp = buffSystem.GetComponent<PlayerController>().player.hp;
        this.cost = costPersenOfMaxHP * hp;

        if (hp >= this.cost)
        {
            Payment(buffSystem.transform);

            buffSystem.ActivateBuff(
                   new Buff(
                        this.id,
                        this.name,
                        BuffType.Mana,
                        manaValue,
                        this.timer
                    )
                );

        }

    }
}
// public class Sacrivert : MonoBehaviour
// {
//     private Skill skill;
//     private GameObject player;

//     private void Start()
//     {
//         skill = GetComponent<SkillController>().skill;
//         // playerController = GameObject.Find("Player").GetComponent<PlayerController>();

//         player = GameObject.Find("Player");
//         BuffSystem buffSystem = player.GetComponent<BuffSystem>();

//         buffSystem.ActivateBuff(
//            new Buff(
//                 skill.Id,
//                 skill.Name,
//                 BuffType.Mana,
//                 skill.Value,
//                 skill.Timer
//             )
//         );
//         StageManager.instance.PlayerActivatesSkill(skill);
//     }
// }