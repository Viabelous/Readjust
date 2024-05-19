using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Skill/Lenire")]
public class Lenire : Skill
{
    [Header("Buff Value")]
    [SerializeField] private float manaValue;
    [SerializeField] private float manaPersenOfFOC;

    public float manaPersenOfFOCFinal
    {
        get { return manaPersenOfFOC + 0.2f * (level - 1); }
    }

    public override string GetDescription()
    {
        description = "Meningkatkan Mana sebanyak 200 + " + manaPersenOfFOC * 100 + "% FOC.";
        return description;
    }


    public override void Activate(GameObject gameObject)
    {
        PlayerController playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        Payment(playerController.transform);

        BuffSystem buffSystem = playerController.GetComponent<BuffSystem>();
        float value = manaValue + manaPersenOfFOCFinal * playerController.player.GetFOC();

        buffSystem.ActivateBuff(
           new Buff(
                this.id,
                this.name,
                BuffType.Mana,
                value,
                this.timer
            )
        );
    }
}

// public class Lenire : MonoBehaviour
// {

//     private Skill skill;
//     private PlayerController playerController;
//     [SerializeField] private float manaPersenOfFoc;


//     private void Start()
//     {
//         skill = GetComponent<SkillController>().skill;

//         playerController = GameObject.Find("Player").GetComponent<PlayerController>();
//         BuffSystem buffSystem = playerController.GetComponent<BuffSystem>();
//         float value = skill.Value + manaPersenOfFoc * playerController.player.foc;

//         buffSystem.ActivateBuff(
//            new Buff(
//                 skill.Id,
//                 skill.Name,
//                 BuffType.Mana,
//                 value,
//                 skill.Timer
//             )
//         );
//         StageManager.instance.PlayerActivatesSkill(skill);
//     }
// }
