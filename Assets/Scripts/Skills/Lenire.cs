using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lenire : MonoBehaviour
{

    private Skill skill;
    private PlayerController playerController;
    [SerializeField] private float manaPersenOfFoc;


    private void Start()
    {
        skill = GetComponent<SkillController>().skill;

        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        BuffSystem buffSystem = playerController.GetComponent<BuffSystem>();
        float value = skill.Value + manaPersenOfFoc * playerController.player.foc;

        buffSystem.ActivateBuff(
           new Buff(
                skill.Id,
                BuffType.Mana,
                value,
                skill.Timer
            )
        );
    }
}
