using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ignite : MonoBehaviour
{
    private Skill skill;

    private void Start()
    {
        skill = GetComponent<SkillController>().skill;
        skill.Damage += 2.5f * GameObject.Find("Player").GetComponent<PlayerController>().player.atk;

        print("Skill Damage + Atk: " + skill.Damage);
    }
}