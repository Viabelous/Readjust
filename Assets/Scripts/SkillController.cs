using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEditor;

public class SkillController : MonoBehaviour
{
    [SerializeField]
    private Skill skillTemplate;
    [HideInInspector]
    public Skill skill;


    private void Start()
    {
        skill = skillTemplate.Clone();

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

        print("Skill Damage: " + skill.Damage);

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
        // // reset keaktifan skill
        // if (!skill.stillActive)
        // {
        //     skill.stillActive = true;
        // }

        // skill.ResetEnemies();

        Destroy(gameObject);
    }


}