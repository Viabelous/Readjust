using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Skill/Landside Typhoon")]
public class LandsideTyphoon : Skill
{
    [Header("Boost Damage")]
    [SerializeField] private float dmgPersenOfAGI;
    [SerializeField] private float dmgPersenOfATK;

    [Header("Crowd Control")]
    [SerializeField] private float pullSpeed;
    [SerializeField] private float radius;

    [Header("Custom Timer")]
    [SerializeField] private float timerPersenOfAGI;

    private PlayerController playerController;

    private List<string> pulledEnemies = new List<string>();

    public float dmgPersenOfAGIFinal
    {
        get { return dmgPersenOfAGI + 0.2f * (level - 1); }
    }

    public float dmgPersenOfATKFinal
    {
        get { return dmgPersenOfATK + 0.2f * (level - 1); }
    }

    public override string GetDescription()
    {
        description = "Menarik musuh yang menapak maupun terbang dan terus memberikan wind damage sebesar " + dmgPersenOfAGIFinal * 100 + "% AGI + " + dmgPersenOfATKFinal * 100 + "% ATK setiap detik. Durasi skill ini adalah " + timerPersenOfAGI * 100 + "% AGI detik.";
        return description;
    }

    public override float GetDamage(Player player)
    {
        return damage + dmgPersenOfAGIFinal * player.GetAGI() + dmgPersenOfATKFinal * player.GetATK();
    }

    public override void Activate(GameObject gameObject)
    {

        // sesuaikan damage basic attack dengan atk player
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();

        this.timer = timerPersenOfAGI * playerController.player.GetAGI();

        Payment(playerController.transform);

    }

    public override void OnActivated(GameObject gameObject)
    {

        // cari musuh di dalam radius
        Collider2D[] enemiesInRadius = Physics2D.OverlapCircleAll(
            gameObject.transform.position,
            radius,
            LayerMask.GetMask("Enemy")
        );

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
                                    this.id,
                                    pullSpeed,
                                    distance + offset,
                                    enemy.transform.position,
                                    direction
                                )
                            );

            }
        }

    }



    public override void HitEnemy(Collider2D other)
    {
        // musuh darat maupun terbang
        if (other.CompareTag("Enemy"))
        {
            MobController mob = other.GetComponent<MobController>();

            // kalau musuh nya sudah ditarik,
            // maka berikan efek cc
            if (pulledEnemies.Contains(mob.enemy.id))
            {
                CrowdControlSystem ccSystem = mob.GetComponent<CrowdControlSystem>();
                // hapus efek tarikan pada musuh
                if (ccSystem.CheckCC(this.id))
                {
                    mob.speed = 0;
                    ccSystem.DactivateCC(this.id);
                }
            }

        }


    }

    public override void AfterHitEnemy(Collider2D other)
    {
        // musuh darat maupun terbang
        if (other.CompareTag("Enemy"))
        {
            MobController mob = other.GetComponent<MobController>();
            if (pulledEnemies.Contains(mob.enemy.id))
            {
                // kembalikan kecepatan musuh
                mob.speed = mob.enemy.MovementSpeed;
                pulledEnemies.Remove(mob.enemy.id);
            }
        }
    }


}
