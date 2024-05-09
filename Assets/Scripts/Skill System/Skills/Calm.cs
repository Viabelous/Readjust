using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Calm : MonoBehaviour
{

    private Skill skill;
    private GameObject player;
    private BuffSystem buffSystem;
    private Buff buff;

    private void Start()
    {
        skill = GetComponent<SkillController>().skill;

        player = GameObject.Find("Player");
        buffSystem = player.GetComponent<BuffSystem>();
        buff = new Buff(
                skill.Id,
                skill.Name,
                BuffType.FOC,
                skill.Value,
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
