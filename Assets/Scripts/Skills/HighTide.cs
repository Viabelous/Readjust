using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class HighTide : Skill
{
    public float knockBackSpeed = 10, knockBackTimer = 0.3f;
    public override void Activate(GameObject gameObject)
    {
        gameObject.transform.position = GameObject.Find("Player").transform.position;
    }

    public override void HitEnemy(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            MobController mob = other.GetComponent<MobController>();
            mob.hp -= damage;

            mob.onKnockBack = true;
            mob.knockBackSpeed = knockBackSpeed;
            mob.knockBackTimer = knockBackTimer;
        }
    }



    // private void OnTriggerExit2D(Collider2D other)
    // {
    //     if (other.CompareTag("Enemy"))
    //     {
    //         MobController mob = other.GetComponent<MobController>();
    //         mob.on = false;

    //     }
    // }



}
