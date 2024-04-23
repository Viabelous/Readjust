using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEditor;

public class SkillController : MonoBehaviour
{
    public Skill skill;

    private float intervalTimer = 1;

    [HideInInspector]
    private float timerAttack;

    private void Start()
    {
        timerAttack = intervalTimer;
        skill.Activate(gameObject);
    }

    private void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (
            skill.type == SkillType.burstDamage ||
            skill.type == SkillType.crowdControl
        )
        {
            skill.HitEnemy(gameObject, other);
        }

    }

    // private void OnTriggerStay2D(Collider2D other)
    // {
    //     if (
    //         skill.hitType == SkillHitType.temporary &&
    //         (skill.type == SkillType.burstDamage || skill.type == SkillType.crowdControl)
    //     )
    //     {
    //         skill.HitEnemy(gameObject, other);
    //     }
    // }

    private void OnTriggerExit2D(Collider2D other)
    {
        skill.AfterHitEnemy(gameObject, other);
    }

    private void OnAnimationEnd()
    {
        Destroy(gameObject);
    }


}