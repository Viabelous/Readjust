using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StalactiteShoot : MonoBehaviour
{
    private Skill skill;
    [SerializeField] private float dmgPersenOfDef;
    [SerializeField] private float dmgPersenOfAtk;

    private void Start()
    {
        PlayerController playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        skill = GetComponent<SkillController>().skill;
        skill.Damage += dmgPersenOfDef * playerController.player.def + dmgPersenOfAtk * playerController.player.atk;
        StageManager.instance.PlayerActivatesSkill(skill);
    }

}