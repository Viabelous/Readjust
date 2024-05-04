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
        // cari musuh di dalam radius
        Collider2D[] enemiesInRadius = Physics2D.OverlapCircleAll(transform.position, skill.PushRange, LayerMask.GetMask("Enemy"));

        // untuk semua musuh di dalam radius
        foreach (Collider2D enemy in enemiesInRadius)
        {
            // ambil id dari musuh
            string id = enemy.GetComponent<MobController>().enemy.id;

            // if (enemy.CompareTag(tagTarget) && !pulledEnemies.Contains(id))

            // jika musuh belum pernah ditarik
            if (!pulledEnemies.Contains(id))
            {
                // tarik musuh ke dalam angin
                pulledEnemies.Add(id);

                float distance = Vector3.Distance(gameObject.transform.position, enemy.transform.position);
                Vector3 direction = (gameObject.transform.position - enemy.transform.position).normalized;
                float offset = UnityEngine.Random.Range(0.5f, 1.5f);

                enemy.GetComponent<CrowdControlSystem>().ActivateCC(
                    new CCKnockBack(
                        skill.Id,
                        skill.PushSpeed,
                        distance + offset,
                        enemy.transform.position,
                        direction
                    )
                );

            }
        }

    }



    private void OnTriggerEnter2D(Collider2D other)
    {
        // if (other.CompareTag(tagTarget))

        // musuh darat maupun terbang
        if (other.CompareTag("Enemy"))
        {
            MobController mob = other.GetComponent<MobController>();

            CrowdControlSystem ccSystem = mob.GetComponent<CrowdControlSystem>();
            // hapus efek tarikan pada musuh
            if (ccSystem.CheckCC(skill.Id))
            {
                mob.speed = 0;
                ccSystem.DactivateCC(skill.Id);
            }
        }


    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // musuh darat maupun terbang
        if (other.CompareTag("Enemy"))
        {
            MobController mob = other.GetComponent<MobController>();

            // kembalikan kecepatan musuh
            mob.speed = mob.enemy.movementSpeed;
        }
    }


}