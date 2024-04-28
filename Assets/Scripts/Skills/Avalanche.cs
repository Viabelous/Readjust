using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Avalanche : MonoBehaviour
{
    private Skill skill;

    private void Start()
    {
        skill = GetComponent<SkillController>().skill;
        skill.Damage += 0.3f * GameObject.Find("Player").GetComponent<PlayerController>().player.atk;
    }
}