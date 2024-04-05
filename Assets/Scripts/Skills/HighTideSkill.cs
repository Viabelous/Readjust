using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighTideSkill : MonoBehaviour
{
    private float damage = 10;
    private float knock = 20;

    private void Start()
    {

    }

    private void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            MobController mob = other.GetComponent<MobController>();
            mob.hp -= damage;

            mob.isKnocked = true;
            mob.knock = knock;
        }
    }



    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            MobController mob = other.GetComponent<MobController>();

            mob.isKnocked = false;
            mob.knock = mob.ogKnock;


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
