using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThornCover : MonoBehaviour
{
    private Skill skill;
    private PlayerController playerController;
    [SerializeField] private float dmgPersenOfDef;
    [SerializeField] private float dmgPersenOfAtk;


    private void Start()
    {
        skill = GetComponent<SkillController>().skill;

        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        BuffSystem buffSystem = playerController.GetComponent<BuffSystem>();
        float value = dmgPersenOfDef * playerController.player.def + dmgPersenOfAtk * playerController.player.atk;

        buffSystem.ActivateBuff(
           new Buff(
                skill.Id,
                BuffType.Thorn,
                value,
                skill.Timer
            )
        );
    }
}