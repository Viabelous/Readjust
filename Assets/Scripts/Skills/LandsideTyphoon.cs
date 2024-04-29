using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandsideTyphoon : MonoBehaviour
{
    private Skill skill;
    private GameObject player;
    [SerializeField] private string tagTarget;
    [SerializeField] private float dmgPersenOfAgi;
    [SerializeField] private float dmgPersenOfAtk;
    [SerializeField] private float timerPersenOfAgi;
    private bool isCollider;

    private List<string> pulledEnemies = new List<string>();


    private void Start()
    {
        // sesuaikan damage basic attack dengan atk player
        skill = GetComponent<SkillController>().skill;
        player = GameObject.Find("Player");
        PlayerController playerController = player.GetComponent<PlayerController>();

        skill.Damage = dmgPersenOfAgi * playerController.player.agi + dmgPersenOfAtk * playerController.player.atk;
        skill.Timer = timerPersenOfAgi * playerController.player.agi;
        isCollider = (gameObject.name == "collider_flying_enemies" || gameObject.name == "collider_flying_enemies") ? true : false;


        if (!isCollider)
        {
            return;
        }


    }

    private void Update()
    {
        Collider2D[] enemiesInRadius = Physics2D.OverlapCircleAll(transform.position, skill.PushRange, LayerMask.GetMask("Enemy"));

        foreach (Collider2D enemy in enemiesInRadius)
        {
            string id = enemy.GetComponent<MobController>().enemy.id;

            if (enemy.CompareTag(tagTarget) && !pulledEnemies.Contains(id))
            {
                pulledEnemies.Add(id);

                float distance = Vector3.Distance(gameObject.transform.position, enemy.transform.position);
                Vector3 direction = (gameObject.transform.position - enemy.transform.position).normalized;

                enemy.GetComponent<CrowdControlSystem>().ActivateCC(
                    new CCKnockBack(
                        skill.Id,
                        skill.PushSpeed,
                        distance - 1f,
                        enemy.transform.position,
                        direction
                    )
                );

            }
        }

    }



    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag(tagTarget))
        {
            MobController mob = other.GetComponent<MobController>();
            mob.speed = 0;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag(tagTarget))
        {
            MobController mob = other.GetComponent<MobController>();
            mob.speed = mob.enemy.movementSpeed;
        }
    }


}