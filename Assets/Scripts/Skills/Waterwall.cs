using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waterwall : MonoBehaviour
{
    private Skill skill;

    private void Start()
    {
        // sesuaikan damage basic attack dengan atk player
        skill = GetComponent<SkillController>().skill;
        float foc = GameObject.Find("Player").GetComponent<PlayerController>().player.foc;
        skill.Damage = 0.75f * foc;
        skill.Timer = foc;
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
