using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicStab : MonoBehaviour
{
    private Skill skill;

    private void Start()
    {
        // sesuaikan damage basic attack dengan atk player
        skill = GetComponent<SkillController>().skill;
        skill.Damage = GameObject.Find("Player").GetComponent<PlayerController>().player.atk;
    }
}