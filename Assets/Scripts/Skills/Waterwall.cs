using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Waterwall : Skill
{
    [SerializeField]
    private float slow;

    public override void Activate(GameObject gameObject)
    {
        gameObject.transform.position = GameObject.Find("Player").transform.position;

        // Active();
    }

    public override void HitEnemyFirstTime(GameObject gameObject, Collider2D other)
    {
    }

    public override void HitEnemy(GameObject gameObject, Collider2D other)
    {

        if (other.CompareTag("Enemy"))
        {
            CrowdControlSystem mob = other.GetComponent<CrowdControlSystem>();
            mob.isSlowed = true;
            mob.slowedSpeed = mob.initialSpeed - mob.initialSpeed * slow;
        }

    }

    public override void AfterHitEnemy(GameObject gameObject, Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            CrowdControlSystem mob = other.GetComponent<CrowdControlSystem>();
            mob.isSlowed = false;
        }
    }


}
