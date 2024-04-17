using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighTideSkill : MonoBehaviour
{
    public float damage = 10;
    public float knock = 20;
    public float manaUsage = 15;

    private void Start()
    {

    }

    private void Update()
    {
        transform.position = GameObject.Find("Player").transform.position;

        Active();
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
        Destroy(gameObject);
        Deactive();
    }
}
