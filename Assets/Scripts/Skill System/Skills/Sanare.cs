using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Skill/Sanare")]
public class Sanare : Skill
{
    [Header("Buff Value")]
    [SerializeField] private float HPPersenOfMaxHP;

    public override void Activate(GameObject gameObject)
    {

        BuffSystem buffSystem = GameObject.Find("Player").GetComponent<BuffSystem>();

        Payment(buffSystem.transform);

        float value = HPPersenOfMaxHP * buffSystem.GetComponent<PlayerController>().player.maxHp;

        buffSystem.ActivateBuff(
           new Buff(
                this.id,
                this.name,
                BuffType.HP,
                value,
                this.timer
            )
        );


    }
}
// public class Sanare : MonoBehaviour
// {
//     private Skill skill;
//     private GameObject player;

//     private void Start()
//     {
//         skill = GetComponent<SkillController>().skill;
//         // playerController = GameObject.Find("Player").GetComponent<PlayerController>();

//         player = GameObject.Find("Player");
//         BuffSystem buffSystem = player.GetComponent<BuffSystem>();
//         float value = skill.Persentase * player.GetComponent<PlayerController>().player.maxHp;

//         buffSystem.ActivateBuff(
//            new Buff(
//                 skill.Id,
//                 skill.Name,
//                 BuffType.HP,
//                 value,
//                 skill.Timer
//             )
//         );
//         StageManager.instance.PlayerActivatesSkill(skill);

//     }
// }