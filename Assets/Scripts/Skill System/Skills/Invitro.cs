using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Skill/Invitro")]
public class Invitro : Skill
{
    [Header("Buff Value")]
    [SerializeField] private float shieldPersenOfMaxHP;
    [SerializeField] private float shieldPersenOfDEF;
    [SerializeField] public float hpPersenOfDmg;
    private PlayerController playerController;
    private float shield;
    private GameObject gameObject;


    private BuffSystem buffSystem;

    public override void Activate(GameObject gameObject)
    {
        this.gameObject = gameObject;
        GameObject player = GameObject.Find("Player");
        buffSystem = player.GetComponent<BuffSystem>();

        playerController = player.GetComponent<PlayerController>();
        shield = shieldPersenOfMaxHP * playerController.player.maxHp + shieldPersenOfDEF * playerController.player.def;
        buffSystem.ActivateBuff(
           new Buff(
                this.id,
                this.name,
                BuffType.Shield,
                shield,
                this.timer
            )
        );
        StageManager.instance.PlayerActivatesSkill(this);
    }

}

// public class Invitro : MonoBehaviour
// {
//     private Skill skill;
//     private GameObject player;
//     private BuffSystem buffSystem;
//     [SerializeField] private float shieldPersenOfMaxHP;
//     [SerializeField] private float shieldPersenOfDef;


//     private void Start()
//     {
//         skill = GetComponent<SkillController>().skill;

//         player = GameObject.Find("Player");
//         buffSystem = player.GetComponent<BuffSystem>();

//         PlayerController playerController = player.GetComponent<PlayerController>();
//         float value = shieldPersenOfMaxHP * playerController.player.maxHp + shieldPersenOfDef * playerController.player.def;

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

//     // private void Update()
//     // {
//     //     if (buffSystem.buffsActive.FindIndex(buff => buff.id == skill.Id) == -1)
//     //     {
//     //         Destroy(gameObject);
//     //     }
//     // }
// }