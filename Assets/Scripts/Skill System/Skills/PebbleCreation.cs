using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PebbleCreation : MonoBehaviour
{
    private Skill skill;
    [SerializeField] private float dmgPersenOfDef;
    private void Start()
    {
        skill = GetComponent<SkillController>().skill;
        skill.Damage += dmgPersenOfDef * GameObject.Find("Player").GetComponent<PlayerController>().player.def;
        StageManager.instance.PlayerActivatesSkill(skill);
    }


}