using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEditor;

public class SkillController : MonoBehaviour
{
    public Skill skill;

    private void Start()
    {
        // sesuaikan damage dengan stage
        if (
            skill.Element == Element.Fire &&
            (
                skill.Type == SkillType.BurstDamage ||
                skill.Type == SkillType.CrowdControl ||
                skill.Type == SkillType.Debuff
            )
        )
        {
            skill.Damage += skill.Damage * 0.1f;
        }

        skill.Activate(gameObject);
    }

    // private void OnTriggerEnter2D(Collider2D other)
    // {

    //     if (
    //         skill.Type == SkillType.BurstDamage ||
    //         skill.Type == SkillType.CrowdControl ||
    //         skill.Type == SkillType.Debuff
    //     )
    //     {
    //         skill.HitEnemy(gameObject, other);
    //     }

    // }

    // private void OnTriggerExit2D(Collider2D other)
    // {
    //     skill.AfterHitEnemy(gameObject, other);
    // }

    private void OnAnimationEnd()
    {
        Destroy(gameObject);
    }


}