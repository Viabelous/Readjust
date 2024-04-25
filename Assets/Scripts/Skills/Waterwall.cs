using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Waterwall : Skill
{
    [Header("Crowd Control")]
    [SerializeField]
    private float slow;

    public override void Activate(GameObject gameObject)
    {
        // gameObject.transform.position = GameObject.Find("Player").transform.position;

        // // Active();
    }

    // public override void HitEnemyFirstTime(GameObject gameObject, Collider2D other)
    // {
    // }

    public override void HitEnemy(GameObject gameObject, Collider2D other)
    {

        if (HasHitEnemy(other))
        {
            return;
        }

        if (other.CompareTag("Enemy"))
        {
            MobController mob = other.GetComponent<MobController>();
            mob.speed -= mob.speed * slow;
        }

        base.HitEnemy(gameObject, other);
    }

    public override void AfterHitEnemy(GameObject gameObject, Collider2D other)
    {

        if (other.CompareTag("Enemy"))
        {
            MobController mob = other.GetComponent<MobController>();
            mob.speed = mob.enemy.movementSpeed;
        }

        base.AfterHitEnemy(gameObject, other);

    }


}
