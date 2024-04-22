using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class MobController : MonoBehaviour
{

    public Enemy enemy;
    public bool movementEnabled = true;


    [HideInInspector] public bool onKnockBack = false, onSlide = false;

    [HideInInspector] public float knockBackSpeed, knockBackTimer;

    [HideInInspector] public float slideSpeed, slideTimer;
    [HideInInspector] public Vector2 backward; // untuk arah slide

    private DefenseSystem defenseSystem;
    private AttackSystem attackSystem;

    private GameObject player;
    private Vector2 movement;
    private Animator animate;
    private SpriteRenderer spriteRenderer;
    private PlayerController playerController;


    void Start()
    {
        animate = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        defenseSystem = GetComponent<DefenseSystem>();
        attackSystem = GetComponent<AttackSystem>();

        player = GameObject.Find("Player");
        playerController = player.GetComponent<PlayerController>();

        // NANTI UBAH KALO UDAH BANYAK MUSUHNYA
        enemy = new Enemy(
            EnemyName.mob,
            GameData.enemies[0].maxHp,
            GameData.enemies[0].atk,
            GameData.enemies[0].def,
            GameData.enemies[0].agi,
            GameData.enemies[0].foc,
            GameData.enemies[0].aerusValue,
            GameData.enemies[0].expValue
        );


    }

    // Update is called once per frame
    void Update()
    {
        // kalau hp habis, hilangkan ---------------------------------------------------
        if (enemy.hp <= 0)
        {
            Die();
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

        else if (movementEnabled)
        {
            Vector3 direction = (player.transform.position - transform.position).normalized;
            transform.Translate(direction * enemy.movementSpeed * Time.deltaTime);

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

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Damage"))
        {
            Damaged();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Damage"))
        {
            Undamaged();
        }
    }
    // private void OnTriggerStay2D(Collider2D other)
    // {
    //     if (other.CompareTag("Damage"))
    //     {
    //         if (timer <= 0)
    //         {
    //             timer = maxTimer;
    //             defenseSystem.TakeDamage(other.GetComponent<AttackSystem>().DealDamage());
    //         }
    //         else
    //         {
    //             timer -= Time.deltaTime;
    //         }
    //     }

    //     // if ()
    // }

    // public void OnTriggerStay2D(Collider2D other)
    // {
    //     if (other.CompareTag("Damage"))
    //     {
    //         Damaged();
    //         defenseSystem.TakeDamage(other.GetComponent<AttackSystem>().DealDamage());
    //     }

    //     if (other.CompareTag("Player"))
    //     {
    //         if (timerAttack >= intervalTimer)
    //         {
    //             playerController.state = PlayerState.attacked;
    //             timerAttack = 0f;
    //         }
    //         else
    //         {
    //             playerController.state = PlayerState.idle;
    //             timerAttack += Time.deltaTime;
    //         }
    //     }

    // }

    // public void OnTriggerExit2D(Collider2D other)
    // {
    //     // sudah gak kena damage
    //     if (other.CompareTag("Damage"))
    //     {
    //         spriteRenderer.color = Color.white;
    //     }

    //     if (other.CompareTag("Player"))
    //     {
    //         playerController.state = PlayerState.idle;
    //         timerAttack = intervalTimer;
    //     }
    // }


    private void Attack()
    {
        // atur kalo attacknya mau diapa2in
        // totalDamage = attack * 1;
    }

    private void Attacked(float totalDamage)
    {
        // defenseSystem.TakeDamage();
    }

    private void Damaged()
    {
        spriteRenderer.color = Color.red;

    }

    private void Undamaged()
    {
        spriteRenderer.color = Color.white;
    }



    private void Die()
    {
        playerController.CollectAerus(enemy.aerusValue);
        playerController.CollectExp(enemy.expValue);
        Destroy(gameObject);
    }


}
