using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterwallSkill : MonoBehaviour
{
    private bool hit;
    public float damage;
    float moveHorizontal, moveVertical;
    public Animator animator;
    public PolygonCollider2D polygonCollider;

    private void Start()
    {
        animator = GetComponent<Animator>();
        polygonCollider = GetComponent<PolygonCollider2D>();

    }

    private void Update()
    {

        if (hit) return;

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
