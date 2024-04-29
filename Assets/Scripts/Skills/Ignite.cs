using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ignite : MonoBehaviour
{
    private Skill skill;
    [SerializeField] private float dmgPersenOfAtk;

    private void Start()
    {
        skill = GetComponent<SkillController>().skill;
        skill.Damage += dmgPersenOfAtk * GameObject.Find("Player").GetComponent<PlayerController>().player.atk;

        print("Skill Damage + Atk: " + skill.Damage);
    }
}