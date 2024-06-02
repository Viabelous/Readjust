using UnityEngine;

[CreateAssetMenu(menuName = "Skill/Heavy Tide")]
public class HeavyTide : Skill
{

    [Header("Boost Damage")]
    [SerializeField] private float dmgPersenOfATK;
    [Header("Level Up Value")]
    [SerializeField] private float dmgPersenOfATKUp;

    [Header("Crowd Control")]
    [SerializeField] private float pushSpeed;
    [SerializeField] private float pushRange;
    private GameObject gameObject;

    public float dmgPersenOfATKFinal
    {
        get { return dmgPersenOfATK + dmgPersenOfATKUp * (level - 1); }
    }

    public override string GetDescription()
    {
        string additionATK = level > 1 ? " (+" + PersentaseToInt(dmgPersenOfATKFinal - dmgPersenOfATK) + "%) " : " ";

        description = "Menyerang musuh di sekitar karakter dengan ombak yang akan mendorong musuh menjauh dan mengakibatkan water damage sebesar " + damage + " + " + PersentaseToInt(dmgPersenOfATK) + "%" + additionATK + "ATK.";
        return description;
    }

    public override float GetDamage(Player player)
    {
        return this.damage + dmgPersenOfATKFinal * player.GetATK();
    }

    public override void Activate(GameObject gameObject)
    {
        this.gameObject = gameObject;
        Payment(GameObject.Find("Player").transform);
    }

    public override void HitEnemy(Collider2D other)
    {
        if (HasHitEnemy(other))
        {
            return;
        }

        if (other.CompareTag("Enemy"))
        {
            CrowdControlSystem mob = other.GetComponent<CrowdControlSystem>();
            Vector3 direction = -(gameObject.transform.position - mob.transform.position).normalized;
            mob.ActivateCC(
                new CCKnockBack(
                    this.id,
                    pushSpeed,
                    pushRange,
                    mob.transform.position,
                    direction
                )
            );

            base.HitEnemy(other);
        }
    }


}