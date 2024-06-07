using UnityEngine;

[CreateAssetMenu(menuName = "Skill/Waterwall")]
public class Waterwall : Skill
{
    [Header("Boost Damage")]
    [SerializeField] private float dmgPersenOfFOC;
    [Header("Level Up Value")]
    [SerializeField] private float dmgPersenOfFOCUp;

    [Header("Crowd Control")]
    [SerializeField] private float slowPersenOfEnemySpeed;

    [Header("Custom Timer")]
    [SerializeField] private float timerPersenOfFOC;

    public float dmgPersenOfFOCFinal
    {
        get { return dmgPersenOfFOC + dmgPersenOfFOCUp * (level - 1); }
    }
    public float dmgPersenOfFOCFinalPersen
    {
        get { return dmgPersenOfFOCFinal + 2.5f; }
    }

    public override string GetDescription()
    {
        string additionFOC = level > 1 ? " (+" + PersentaseToInt(dmgPersenOfFOCFinal - dmgPersenOfFOC) + "%)" : " ";

        description = "Menciptakan {Waterwall} yang akan memberikan efek slow pada musuh dan seiring waktu memberikan water damage sebesar " + PersentaseToInt(dmgPersenOfFOC) + "%" + additionFOC + "FOC. {Waterwall} akan bertahan selama 100% FOC detik ketika skill ini digunakan. Menggunakan skill ini tidak akan menghilangkan shield.";
        return description;
    }

    public override float GetDamage(Player player)
    {

        return dmgPersenOfFOCFinalPersen * player.GetFOC();
    }

    public override void Activate(GameObject gameObject)
    {
        PlayerController playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        this.timer = timerPersenOfFOC * playerController.player.GetFOC();
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
