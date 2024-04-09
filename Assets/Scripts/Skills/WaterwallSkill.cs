using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterwallSkill : MonoBehaviour
{
    public float damage = 0.1f;
    public float slow = 0.7f;
    public float manaUsage = 10;


    private void Start()
    {

    }

    private void Update()
    {


    }
    public float GetDamage()
    {
        return this.damage;
    }
    public void SetDamage(float damage)
    {
        this.damage = damage;
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            MobController mob = other.GetComponent<MobController>();
            mob.hp -= damage;
            mob.speed -= mob.ogSpeed * slow;
        }
    }


    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            MobController mob = other.GetComponent<MobController>();
            mob.speed = mob.ogSpeed;
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
