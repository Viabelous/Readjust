using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Preserve : MonoBehaviour
{
    private Skill skill;
    private GameObject player;

    private void Start()
    {
        skill = GetComponent<SkillController>().skill;

        player = GameObject.Find("Player");
        BuffSystem buffSystem = player.GetComponent<BuffSystem>();

        PlayerController playerController = player.GetComponent<PlayerController>();
        float value = skill.Persentase * playerController.player.def;

        buffSystem.ActivateBuff(
           new Buff(
                skill.Id,
                BuffType.Shield,
                value,
                skill.Timer
            )
        );
    }
}
