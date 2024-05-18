using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Skill/Thorn Cover")]
public class ThornCover : Skill
{

    [Header("Buff Value")]
    [SerializeField] private float dmgPersenOfDEF;
    [SerializeField] private float dmgPersenOfATK;
    private BuffSystem buffSystem;
    private Buff buff;


    public override void Activate(GameObject gameObject)
    {

        PlayerController playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        buffSystem = playerController.GetComponent<BuffSystem>();
        Payment(buffSystem.transform);

        float value = dmgPersenOfDEF * playerController.player.GetDEF() + dmgPersenOfATK * playerController.player.GetATK();
        buff = new Buff(
                this.id,
                this.name,
                BuffType.Thorn,
                value,
                this.timer
            );
        buffSystem.ActivateBuff(buff);
    }

    public override void OnActivated(GameObject gameObject)
    {
        if (!buffSystem.CheckBuff(buff))
        {
            Destroy(gameObject);
        }
    }
}
// public class ThornCover : MonoBehaviour
// {
//     private Skill skill;
//     private PlayerController playerController;
//     private BuffSystem buffSystem;
//     private Buff buff;
//     [SerializeField] private float dmgPersenOfDef;
//     [SerializeField] private float dmgPersenOfAtk;


//     private void Start()
//     {
//         skill = GetComponent<SkillController>().skill;

//         playerController = GameObject.Find("Player").GetComponent<PlayerController>();
//         buffSystem = playerController.GetComponent<BuffSystem>();
//         float value = dmgPersenOfDef * playerController.player.def + dmgPersenOfAtk * playerController.player.atk;
//         buff = new Buff(
//                 skill.Id,
//                 skill.Name,
//                 BuffType.Thorn,
//                 value,
//                 skill.Timer
//             );
//         buffSystem.ActivateBuff(buff);
//         StageManager.instance.PlayerActivatesSkill(skill);
//     }

//     private void Update()
//     {
//         if (!buffSystem.CheckBuff(buff))
//         {
//             Destroy(gameObject);
//         }
//     }
// }