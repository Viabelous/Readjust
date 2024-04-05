using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgniteSkill : MonoBehaviour
{
    private float damage = 50;
    float moveHorizontal, moveVertical;
    public SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
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
        }
    }

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
