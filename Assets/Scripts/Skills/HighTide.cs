using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class HighTide : Skill
{
    public float knockBackSpeed, knockBackDistance;

    public override void Activate(GameObject gameObject)
    {
        gameObject.transform.position = GameObject.Find("Player").transform.position;
    }

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
