using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Skill/Heavy Tide")]
public class HeavyTide : Skill
{

    [Header("Boost Damage")]
    [SerializeField] private float dmgPersenOfATK;

    [Header("Crowd Control")]
    [SerializeField] private float pushSpeed;
    [SerializeField] private float pushRange;
    private GameObject gameObject;

    public float dmgPersenOfATKFinal
    {
        get { return dmgPersenOfATK + 0.2f * (level - 1); }
    }

    public override string GetDescription()
    {
        description = "Menyerang musuh di sekitar karakter dengan ombak yang akan mendorong musuh menjauh dan mengakibatkan water damage sebesar 25 + " + dmgPersenOfATKFinal * 100 + "% ATK.";
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