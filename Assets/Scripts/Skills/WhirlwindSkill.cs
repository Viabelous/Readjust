using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhirlwindSkill : MonoBehaviour
{
    private bool hit;
    private float damage = 30;


    private void Start()
    {

    }

    private void Update()
    {

        if (hit) return;

    }



    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            MobController mob = other.GetComponent<MobController>();
            mob.hp -= damage;
            mob.rb.AddForce(Vector2.up * 20, ForceMode2D.Impulse);
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
