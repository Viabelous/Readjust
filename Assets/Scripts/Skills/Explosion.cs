using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Explosion : Skill
{
    public float knockBackSpeed, knockBackDistance;

    public override void HitEnemy(GameObject gameObject, Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            CrowdControlSystem mob = other.GetComponent<CrowdControlSystem>();
            mob.ActivateKnockBack(knockBackSpeed, knockBackDistance, gameObject.transform.position);
        }
    }

    public override void AfterHitEnemy(GameObject gameObject, Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            CrowdControlSystem mob = other.GetComponent<CrowdControlSystem>();
            mob.DeactivateKnockBack();
        }
    }

}
