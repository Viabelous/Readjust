using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobController : MonoBehaviour
{

    public GameObject player;
    public GameObject mob;

    public float ogSpeed = 1f, speed = 1f;
    public float ogHp = 500, hp = 500;
    // public float knock = 0;

    public Rigidbody2D rb;
    Vector2 movement;
    public Animator animate;
    SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        // kalau hp habis, hilangkan
        if (hp <= 0)
        {
            Destroy(mob, 0.5f);
        }

        Move();

    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        // kena damage
        if (other.CompareTag("Damage"))
        {
            spriteRenderer.color = Color.red;
            print("HP = " + hp.ToString());
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        // sudah gak kena damage
        if (other.CompareTag("Damage"))
        {
            spriteRenderer.color = Color.white;


        }
    }

    // void OnCollisionEnter2D(Collision2D collision)
    // {
    //     // Memeriksa apakah objek bertabrakan dengan sesuatu
    //     if (collision.gameObject.CompareTag("Enemy"))
    //     {
    //         // Mendapatkan arah normal dari permukaan yang ditabrak
    //         Vector3 bounceDirection = collision.contacts[0].normal;

    //         // Memberikan gaya pantulan mundur ke objek
    //         collision.rigidbody.AddForce(-bounceDirection * 20, ForceMode2D.Impulse);
    //     }
    // }



    void Move()
    {
        if (player.transform.position.x != mob.transform.position.x)
        {
            movement.x = player.transform.position.x < mob.transform.position.x ? -1 : 1;
        }
        else
        {
            movement.x = 0;
        }

        if (player.transform.position.y != mob.transform.position.y)
        {
            movement.y = player.transform.position.y < mob.transform.position.y ? -1 : 1;
        }
        else
        {
            movement.y = 0;
        }

        animate.SetFloat("Horizontal", movement.y);
        animate.SetFloat("Speed", movement.sqrMagnitude);
    }


    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
        if (movement.x != 0 && movement.y != 0)
        {
            mob.transform.localScale = new Vector3((movement.x > 0.5) ? 1 : -1, 1, 1);
        }
    }

    public bool OnTheLeft()
    {
        return mob.transform.position.x < player.transform.position.x;
    }

    public bool OnTheRight()
    {
        return mob.transform.position.x > player.transform.position.x;
    }
    public bool OnTheTop()
    {
        return mob.transform.position.y > player.transform.position.x;
    }
    public bool OnTheBottom()
    {
        return mob.transform.position.x < player.transform.position.x;
    }

}
