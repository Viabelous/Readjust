using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sacrivert : MonoBehaviour
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
                skill.Id,
                BuffType.Mana,
                skill.Value,
                skill.Timer
            )
        );
    }
}