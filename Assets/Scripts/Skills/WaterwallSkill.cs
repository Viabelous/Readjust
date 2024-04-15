using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterwallSkill : MonoBehaviour
{
    public float damage = 10f;
    public float slow = 0.5f;
    public float manaUsage = 10;

    public float intervalTimer = 1, timerAttack;


    private void Start()
    {
        timerAttack = intervalTimer;

    }

    private void Update()
    {


    }

    private void OnTriggerStay2D(Collider2D other)
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

    public void Active()
    {
        gameObject.SetActive(true);
    }

    public void Deactive()
    {
        gameObject.SetActive(false);
    }

    private void OnAnimationEnd()
    {
        Deactive();
    }
}
