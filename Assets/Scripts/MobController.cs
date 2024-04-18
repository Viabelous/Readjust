using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class MobController : MonoBehaviour
{

    public float maxSpeed = 1f, speed;
    public float maxHp = 500, hp;
    public float attack = 10f;

    public bool onKnockBack = false, onSlide = false;

    // [HideInInspector] public float knock;

    [HideInInspector] public float knockBackSpeed, knockBackTimer;

    [HideInInspector] public float slideSpeed, slideTimer;
    [HideInInspector] public Vector2 backward; // untuk arah slide

    [HideInInspector] public float intervalTimer = 1, timerAttack;


    private GameObject player;
    private Rigidbody2D rb;
    private Vector2 movement;
    private Animator animate;
    private SpriteRenderer spriteRenderer;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animate = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        speed = maxSpeed;
        hp = maxHp;
        timerAttack = intervalTimer;

        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        // kalau hp habis, hilangkan ---------------------------------------------------
        if (hp <= 0)
        {
            Destroy(gameObject, 0.5f);
        }

        // arah hadap mob (?) ------------------------------------------------------------
        if (player.transform.position.x != transform.position.x)
        {
            movement.x = player.transform.position.x < transform.position.x ? -1 : 1;
        }
        else
        {
            movement.x = 0;
        }

        if (player.transform.position.y != transform.position.y)
        {
            movement.y = player.transform.position.y < transform.position.y ? -1 : 1;
        }
        else
        {
            movement.y = 0;
        }

        animate.SetFloat("Horizontal", movement.y);
        animate.SetFloat("Speed", movement.sqrMagnitude);

        if (gameObject.transform.position.y > player.transform.position.y)
        {
            spriteRenderer.sortingLayerName = "Enemy Back";
        }
        else
        {
            spriteRenderer.sortingLayerName = "Enemy Front";
        }

    }

    // pergerakan mob ------------------------------------------------------------------------
    void FixedUpdate()
    {
        if (onKnockBack)
        {
            StartCoroutine(KnockingBack());
            // rb.MovePosition(rb.position + movement * -knock * speed * Time.fixedDeltaTime);
        }

        else if (onSlide)
        {
            StartCoroutine(Sliding());
        }

        else
        {
            Vector3 direction = (player.transform.position - transform.position).normalized;
            transform.Translate(direction * speed * Time.deltaTime);

            // rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);

        }

        if (movement.x != 0 && movement.y != 0)
        {
            gameObject.transform.localScale = new Vector3((movement.x > 0.5) ? 1 : -1, 1, 1);
        }

    }

    public IEnumerator Sliding()
    {
        onSlide = true;

        // // posisi mob di kanan player
        // if (transform.position.x > player.transform.position.x)
        // {
        //     backward = transform.right;
        // }
        // // posisi mob di kiri player
        // else if (transform.position.x < player.transform.position.x)
        // {
        //     backward = -transform.right;
        // }

        // // posisi mob di atas player
        // if (transform.position.y < player.transform.position.y)
        // {
        //     backward = transform.up;
        // }
        // // posisi mob di bawah player
        // else if (transform.position.y > player.transform.position.y)
        // {
        //     backward = -transform.up;
        // }


        // Mundur ke belakang
        transform.Translate(backward * slideSpeed * Time.deltaTime);


        // Tunggu beberapa detik
        yield return new WaitForSeconds(slideTimer);

        onSlide = false;

    }

    public IEnumerator KnockingBack()
    {
        Vector3 direction = -(player.transform.position - transform.position).normalized;
        transform.Translate(direction * knockBackSpeed * Time.deltaTime);

        // Tunggu beberapa detik
        yield return new WaitForSeconds(knockBackTimer);

        onKnockBack = false;

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





}
