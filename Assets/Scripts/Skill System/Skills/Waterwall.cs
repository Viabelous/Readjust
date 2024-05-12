using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Skill/Waterwall")]
public class Waterwall : Skill
{
    [Header("Boost Damage")]
    [SerializeField] private float dmgPersenOfFOC;

    [Header("Crowd Control")]
    [SerializeField] private float slowPersenOfEnemySpeed;

    [Header("Custom Timer")]
    [SerializeField] private float timerPersenOfFOC;

    public override float GetDamage(Character character)
    {

        return dmgPersenOfFOC * character.foc;
    }

    public override void Activate(GameObject gameObject)
    {
        PlayerController playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        this.timer = timerPersenOfFOC * playerController.player.foc;
        Payment(playerController.transform);
    }

    public override void HitEnemy(Collider2D other)
    {

    }

    public override void WhileHitEnemy(Collider2D other)
    {
        if (HasHitEnemy(other))
        {
            return;
        }

        if (other.CompareTag("Enemy"))
        {
            MobController mob = other.GetComponent<MobController>();
            mob.speed -= slowPersenOfEnemySpeed * mob.speed;
            base.HitEnemy(other);
        }

    }

    public override void AfterHitEnemy(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            MobController mob = other.GetComponent<MobController>();
            mob.speed = mob.enemy.MovementSpeed;
            base.AfterHitEnemy(other);
        }
    }
}

// public class Waterwall : MonoBehaviour
// {
//     private Skill skill;
//     [SerializeField] private float dmgPersenOfFoc;
//     [SerializeField] private float timerPersenOfFoc;

//     private void Start()
//     {
//         // sesuaikan damage basic attack dengan atk player
//         skill = GetComponent<SkillController>().skill;
//         float foc = GameObject.Find("Player").GetComponent<PlayerController>().player.foc;
//         skill.Damage = dmgPersenOfFoc * foc;
//         skill.Timer = timerPersenOfFoc;

//         StageManager.instance.PlayerActivatesSkill(skill);
//     }

//     private void OnTriggerStay2D(Collider2D other)
//     {
//         if (skill.HasHitEnemy(other))
//         {
//             return;
//         }

//         if (other.CompareTag("Enemy"))
//         {
//             MobController mob = other.GetComponent<MobController>();
//             mob.speed -= skill.Persentase * mob.speed;
//             skill.HitEnemy(other);
//         }

//     }

//     private void OnTriggerExit2D(Collider2D other)
//     {
//         MobController mob = other.GetComponent<MobController>();
//         mob.speed = mob.enemy.movementSpeed;
//         skill.AfterHitEnemy(other);
//     }
// }
