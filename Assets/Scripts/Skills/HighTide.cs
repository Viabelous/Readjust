using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class HighTide : Skill
{
    public float knockBackSpeed, distance;

    public override void Activate(GameObject gameObject)
    {
        gameObject.transform.position = GameObject.Find("Player").transform.position;
    }

    public override void HitEnemy(GameObject gameObject, Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            CrowdControlSystem mob = other.GetComponent<CrowdControlSystem>();
            // mob.enemy.hp -= damage;

            mob.isKnocked = true;
            mob.knockSpeed = knockBackSpeed;
            mob.knockDistance = distance;
            mob.knockDirection = -(gameObject.transform.position - mob.transform.position).normalized;
            // mob.knockBackTimer = knockBackTimer;
        }
    }

    public override void AfterHitEnemy(GameObject gameObject, Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            CrowdControlSystem mob = other.GetComponent<CrowdControlSystem>();
            mob.isKnocked = false;
        }
    }


}
