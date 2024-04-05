using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgniteSkill : MonoBehaviour
{
    private bool hit;
    private float damage;
    float moveHorizontal, moveVertical;
    public SpriteRenderer spriteRenderer;

    private void Start()
    {

        spriteRenderer = GetComponent<SpriteRenderer>();

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
    //     if (collision.CompareTag("Enemy"))
    //     {
    //         print("Kenaa wehh");
    //     }
    // }

    public void Active()
    {
        gameObject.SetActive(true);

        moveHorizontal = Input.GetAxis("Horizontal");
        if (moveHorizontal > 0f)
        {
            spriteRenderer.flipY = false;
            spriteRenderer.flipX = true;
        }

        else if (moveHorizontal < 0f)
        {
            spriteRenderer.flipY = false;
            spriteRenderer.flipX = false;
        }

        moveVertical = Input.GetAxis("Vertical");

        if (moveVertical < 0f)
        {
            spriteRenderer.flipY = true;
        }
        else if (moveVertical > 0f)
        {
            spriteRenderer.flipY = false;
        }
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
