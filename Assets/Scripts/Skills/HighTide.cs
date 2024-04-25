using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class HighTide : Skill
{
    [Header("Crowd Control")]
    [SerializeField] private float knockBackSpeed;
    [SerializeField] private float knockBackDistance;

    public override void Activate(GameObject gameObject)
    {
        // gameObject.transform.position = GameObject.Find("Player").transform.position;
    }

    public override void HitEnemy(GameObject gameObject, Collider2D other)
    {
        base.HitEnemy(gameObject, other);
        if (other.CompareTag("Enemy"))
        {
            CrowdControlSystem mob = other.GetComponent<CrowdControlSystem>();
            Vector3 direction = -(gameObject.transform.position - mob.transform.position).normalized;
            mob.ActivateCC(
                new CCKnockBack(
                    knockBackSpeed,
                    knockBackDistance,
                    mob.transform.position,
                    direction
                )
            );
            // mob.ActivateKnockBack(knockBackSpeed, knockBackDistance, gameObject.transform.position);
        }
    }

    public override void AfterHitEnemy(GameObject gameObject, Collider2D other)
    {
        base.AfterHitEnemy(gameObject, other);
    }


}
