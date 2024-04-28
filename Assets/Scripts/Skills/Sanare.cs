using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sanare : MonoBehaviour
{
    private Skill skill;
    private GameObject player;

    private void Start()
    {
        skill = GetComponent<SkillController>().skill;
        // playerController = GameObject.Find("Player").GetComponent<PlayerController>();

        player = GameObject.Find("Player");
        BuffSystem buffSystem = player.GetComponent<BuffSystem>();
        float value = skill.Persentase * player.GetComponent<PlayerController>().player.maxHp;

        buffSystem.ActivateBuff(
           new Buff(
                BuffType.HP,
                value,
                skill.Timer
            )
        );

    }
}