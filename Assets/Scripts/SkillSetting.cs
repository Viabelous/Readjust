using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEditor;

public class SkillSetting : MonoBehaviour
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

        if (skill.type == SkillType.burstDamage ||
            skill.type == SkillType.crowdControl)
        {
            skill.HitEnemy(other);
        }


    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            MobController mob = other.GetComponent<MobController>();

            if (timerAttack >= intervalTimer)
            {
                mob.enemy.hp -= skill.damage;
                timerAttack = 0f;
            }
            else
            {
                timerAttack += Time.deltaTime;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        skill.AfterHitEnemey(other);
        timerAttack = intervalTimer;
    }

    private void OnAnimationEnd()
    {
        Destroy(gameObject);
    }


}