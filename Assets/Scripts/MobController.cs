using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobController : MonoBehaviour
{

    public GameObject player;
    public GameObject mob;

    public float ogSpeed = 1f, speed = 1f;
    public float ogHp = 500, hp = 500;
    public bool isKnocked = false;
    public float ogKnock = 1, knock = 1;
    public bool isSwiped = false;
    public Vector2 ogBackward = new Vector2(0, 0), backward = new Vector2(0, 0);
    public float ogSwipe = 1, swipe = 1;

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

        rb.MovePosition(rb.position + (isSwiped ? backward * swipe : movement * (isKnocked ? -knock : knock) * speed) * Time.fixedDeltaTime);
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
