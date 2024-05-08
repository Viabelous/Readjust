using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Calm : MonoBehaviour
{

    private Skill skill;
    private GameObject player;

    private void Start()
    {
        skill = GetComponent<SkillController>().skill;

        player = GameObject.Find("Player");
        BuffSystem buffSystem = player.GetComponent<BuffSystem>();

        buffSystem.ActivateBuff(
           new Buff(
                skill.Id,
                BuffType.FOC,
                skill.Value,
                skill.Timer
            )
        );

        StageManager.instance.PlayerActivatesSkill(skill);
    }
}
