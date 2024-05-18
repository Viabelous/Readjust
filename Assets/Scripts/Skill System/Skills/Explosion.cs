using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "Skill/Explosion")]
public class Explosion : Skill
{
    [Header("Boost Damage")]
    [SerializeField] private float dmgPersenOfATK;

    [Header("Crowd Control")]
    [SerializeField] private float pushSpeed;
    [SerializeField] private float pushRange;

    private GameObject gameObject;

    public override float GetDamage(Player player)
    {
        return this.damage += dmgPersenOfATK * player.GetATK();
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
        }

        base.HitEnemy(other);

    }


}