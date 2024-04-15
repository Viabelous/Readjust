using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobController : MonoBehaviour
{

    public float maxSpeed = 1f, speed;
    public float maxHp = 500, hp;
    public float attack = 10f;

    public bool isKnocked = false, isSwiped = false;
    public float knock, swipe;

    public Vector2 backward; // untuk arah swipe

    public float intervalTimer = 1, timerAttack;



    public GameObject player;
    public GameObject mob;
    public Rigidbody2D rb;
    Vector2 movement;
    public Animator animate;
    SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        speed = maxSpeed;
        hp = maxHp;
        timerAttack = intervalTimer;
    }

    // Update is called once per frame
    void Update()
    {
        // kalau hp habis, hilangkan ---------------------------------------------------
        if (hp <= 0)
        {
            Destroy(mob, 0.5f);
        }

        // arah hadap mob (?) ------------------------------------------------------------
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

    public void OnTriggerEnter2D(Collider2D other)
    {
        // kena damage
        if (other.CompareTag("Damage"))
        {
            spriteRenderer.color = Color.red;
        }

    }

    public void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Damage"))
        {
            print("HP = " + hp.ToString());
        }

        if (other.CompareTag("Player"))
        {
            PlayerController player = other.gameObject.GetComponent<PlayerController>();
            player.attacked = true;

            if (timerAttack >= intervalTimer)
            {
                for (int i = 0; i < player.spriteRenderers.Length; i++)
                {
                    player.spriteRenderers[i].color = Color.red;
                }

                if (player.hp > 0)
                {
                    player.hp -= attack;
                    player.UpdateHealthBar();
                }
                timerAttack = 0f;
            }
            else
            {
                timerAttack += Time.deltaTime;
            }

        }

    }

    public void OnTriggerExit2D(Collider2D other)
    {
        // sudah gak kena damage
        if (other.CompareTag("Damage"))
        {
            spriteRenderer.color = Color.white;
        }

        if (other.CompareTag("Player"))
        {
            PlayerController player = other.gameObject.GetComponent<PlayerController>();
            for (int i = 0; i < player.spriteRenderers.Length; i++)
            {
                player.spriteRenderers[i].color = Color.white;
            }
            timerAttack = intervalTimer;

        }
    }



    // pergerakan mob ------------------------------------------------------------------------
    void FixedUpdate()
    {
        if (isKnocked)
        {
            rb.MovePosition(rb.position + movement * -knock * speed * Time.fixedDeltaTime);
        }
        else if (isSwiped)
        {
            rb.MovePosition(rb.position + backward * swipe * Time.fixedDeltaTime);
        }
        else
        {
            rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);

        }

        if (movement.x != 0 && movement.y != 0)
        {
            mob.transform.localScale = new Vector3((movement.x > 0.5) ? 1 : -1, 1, 1);
        }

    }


}
