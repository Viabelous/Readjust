using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WillOfFire : MonoBehaviour
{
    private Skill skill;
    private GameObject player;

    private void Start()
    {
        skill = GetComponent<SkillController>().skill;
        // playerController = GameObject.Find("Player").GetComponent<PlayerController>();

        player = GameObject.Find("Player");
        BuffSystem buffSystem = player.GetComponent<BuffSystem>();

        buffSystem.ActivateBuff(
           new Buff(
                BuffType.ATK,
                skill.Value,
                skill.Timer
            )
        );
    }
}