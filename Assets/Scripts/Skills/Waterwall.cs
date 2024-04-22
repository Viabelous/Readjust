using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Waterwall : Skill
{
    [SerializeField]
    private float slow = 50;

    public override void Activate(GameObject gameObject)
    {
        gameObject.transform.position = GameObject.Find("Player").transform.position;

        // Active();
    }

    public override void HitEnemy(Collider2D other)
    {

        if (other.CompareTag("Enemy"))
        {
            MobController mob = other.GetComponent<MobController>();
            mob.enemy.agi -= slow;
        }

    }

    public override void AfterHitEnemey(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            MobController mob = other.GetComponent<MobController>();
            mob.enemy.agi += slow;
        }
    }


}
