using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    private Skill skill;
    [SerializeField] private float dmgPersenOfAtk;

    private void Start()
    {
        skill = GetComponent<SkillController>().skill;
        skill.Damage += dmgPersenOfAtk * GameObject.Find("Player").GetComponent<PlayerController>().player.atk;
        StageManager.instance.PlayerActivatesSkill(skill);
    }

}