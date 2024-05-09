using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThornCover : MonoBehaviour
{
    private Skill skill;
    private PlayerController playerController;
    private BuffSystem buffSystem;
    private Buff buff;
    [SerializeField] private float dmgPersenOfDef;
    [SerializeField] private float dmgPersenOfAtk;


    private void Start()
    {
        skill = GetComponent<SkillController>().skill;

        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        buffSystem = playerController.GetComponent<BuffSystem>();
        float value = dmgPersenOfDef * playerController.player.def + dmgPersenOfAtk * playerController.player.atk;
        buff = new Buff(
                skill.Id,
                skill.Name,
                BuffType.Thorn,
                value,
                skill.Timer
            );
        buffSystem.ActivateBuff(buff);
        StageManager.instance.PlayerActivatesSkill(skill);
    }

    private void Update()
    {
        if (!buffSystem.CheckBuff(buff))
        {
            Destroy(gameObject);
        }
    }
}