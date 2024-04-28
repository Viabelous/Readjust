using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEditor;

public class SkillController : MonoBehaviour
{
    [SerializeField]
    public Skill skillTemplate;

    [HideInInspector]
    public Skill skill;


    private void Start()
    {

        skill = skillTemplate.Clone();
        // sesuaikan damage skill dengan stage
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
        print("Skill Damage + Fire: " + skill.Damage);
        skill.Activate(gameObject);
    }

    private void OnAnimationEnd()
    {

        Destroy(gameObject);
    }


}