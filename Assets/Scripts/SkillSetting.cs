using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEditor;

public class SkillSetting : MonoBehaviour
{
    public Skill skill;

    private void Start()
    {
        // skill.Activate(gameObject);
        skill.Activate(gameObject);
    }

    private void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (skill.type == SkillType.burstDamage ||
            skill.type == SkillType.crowdControl)
        {
            skill.HitEnemy(other);
        }

        if (other.CompareTag("Enemy"))
        {
            MobController mob = other.GetComponent<MobController>();
            mob.hp -= skill.damage;
        }

    }

    private void OnAnimationEnd()
    {
        Destroy(gameObject);
    }


}