using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighTideSkill : MonoBehaviour
{
    public float damage = 10;
    public float knockBackSpeed = 10, knockBackTimer = 0.3f;
    // public float knock = 20;
    private void Start()
    {

    }

    private void Update()
    {
        transform.position = GameObject.Find("Player").transform.position;
    }

    private void OnTriggerEnter2D(Collider2D other)
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




    private void OnAnimationEnd()
    {
        Destroy(gameObject);
    }
}
