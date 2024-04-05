using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighTideSkill : MonoBehaviour
{
    private bool hit;
    private float damage;

    private void Start()
    {

    }

    private void Update()
    {

        if (hit) return;

    }

    public float GetDamage()
    {
        return this.damage;
    }
    public void SetDamage(float damage)
    {
        this.damage = damage;
    }


    // private void OnCollisionEnter2d(Collision2D collision)
    // {
    //     hit = true;
    //     if (collision.gameObject.CompareTag("Enemy"))
    //     {
    //         print("Kenaa");
    //     }

    // }

    // private void OnTriggerEnter2D(Collider2D collision)
    // {
    //     if (collision.gameObject.CompareTag("Enemy"))
    //     {
    //         print("Kenaa wehh");
    //     }
    // }

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
