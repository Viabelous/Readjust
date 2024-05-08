using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waterwall : MonoBehaviour
{
    private Skill skill;
    [SerializeField] private float dmgPersenOfFoc;
    [SerializeField] private float timerPersenOfFoc;

    private void Start()
    {
        // sesuaikan damage basic attack dengan atk player
        skill = GetComponent<SkillController>().skill;
        float foc = GameObject.Find("Player").GetComponent<PlayerController>().player.foc;
        skill.Damage = dmgPersenOfFoc * foc;
        skill.Timer = timerPersenOfFoc;

        StageManager.instance.PlayerActivatesSkill(skill);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (skill.HasHitEnemy(other))
        {
            return;
        }

        if (other.CompareTag("Enemy"))
        {
            MobController mob = other.GetComponent<MobController>();
            mob.speed -= skill.Persentase * mob.speed;
            skill.HitEnemy(other);
        }

    }

    private void OnTriggerExit2D(Collider2D other)
    {
        MobController mob = other.GetComponent<MobController>();
        mob.speed = mob.enemy.movementSpeed;
        skill.AfterHitEnemy(other);
    }
}
