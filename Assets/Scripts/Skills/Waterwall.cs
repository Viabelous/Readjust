using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Waterwall : Skill
{
    [SerializeField]
    private float slow = 0.5f, intervalTimer = 1;
    private float timerAttack;


    public override void Activate(GameObject gameObject)
    {
        timerAttack = intervalTimer;

        gameObject.transform.position = GameObject.Find("Player").transform.position;

        // Active();
    }

    public override void HitEnemy(Collider2D other)
    {

        if (other.CompareTag("Enemy"))
        {
            MobController mob = other.GetComponent<MobController>();
            mob.speed = mob.maxSpeed * slow;

            if (timerAttack >= intervalTimer)
            {
                mob.hp -= damage;
                timerAttack = 0f;
            }
            else
            {
                timerAttack += Time.deltaTime;
            }
        }

    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            MobController mob = other.GetComponent<MobController>();
            mob.speed = mob.maxSpeed;
            timerAttack = intervalTimer;
        }
    }


}
