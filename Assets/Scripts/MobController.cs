using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public enum CharacterState
{
    alive,
    dead
}



public class MobController : MonoBehaviour
{

    public Enemy enemy;
    public bool movementEnabled = true;
    public float speed;

    private DefenseSystem defenseSystem;
    private AttackSystem attackSystem;
    private CrowdControlSystem crowdControlSystem;
    private CharacterState state;

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
        crowdControlSystem = GetComponent<CrowdControlSystem>();

        player = GameObject.Find("Player");
        playerController = player.GetComponent<PlayerController>();

        enemy = enemy.CloneObject();
        speed = enemy.movementSpeed;
        state = CharacterState.alive;
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

        animate.SetFloat("Vertical", movement.y);
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

        if (
            movementEnabled
        )
        {
            Vector3 direction = (player.transform.position - transform.position).normalized;
            transform.Translate(direction * speed * Time.deltaTime);
        }

        if (movement.x != 0 && movement.y != 0)
        {
            gameObject.transform.localScale = new Vector3((movement.x > 0.5) ? 1 : -1, 1, 1);
        }

    }


    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Damage"))
        {
            print(enemy.hp);
            Damaged();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Damage") && state == CharacterState.alive)
        {
            Undamaged();
        }
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
        state = CharacterState.dead;
        playerController.CollectAerus(enemy.aerusValue);
        playerController.CollectExp(enemy.expValue);
        Damaged();
        Destroy(gameObject);
    }


}
