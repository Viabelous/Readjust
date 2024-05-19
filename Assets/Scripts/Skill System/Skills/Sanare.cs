using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Skill/Sanare")]
public class Sanare : Skill
{
    [Header("Buff Value")]
    [SerializeField] private float HPPersenOfMaxHP;

    public float HPPersenOfMaxHPFinal
    {
        get { return HPPersenOfMaxHP + 0.2f * (level - 1); }
    }

    public override string GetDescription()
    {
        description = "Mengonsumsi Mana untuk mengisi HP. Pemulihan yang didapat senilai dengan " + HPPersenOfMaxHPFinal * 100 + "% Max HP.";
        return description;
    }

    public override void Activate(GameObject gameObject)
    {

        BuffSystem buffSystem = GameObject.Find("Player").GetComponent<BuffSystem>();

        Payment(buffSystem.transform);

        float value = HPPersenOfMaxHPFinal * buffSystem.GetComponent<PlayerController>().player.GetMaxHP();

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