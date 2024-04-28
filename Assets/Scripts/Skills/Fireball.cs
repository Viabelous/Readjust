using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    private Skill skill;

    private void Start()
    {
        skill = GetComponent<SkillController>().skill;
        skill.Damage += 0.5f * GameObject.Find("Player").GetComponent<PlayerController>().player.atk;
    }

}