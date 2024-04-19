using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballSkill : MonoBehaviour
{

    public float damage = 5, speed = 10;
    public float maxX, minX, maxY, minY;

    private string direction = "front";
    private bool isInstantiate = true, onAttack = true;

    private Quaternion initialRotation;
    private Vector3 initialScale;

    private GameObject player;

    private SpriteRenderer spriteRenderer;
    private Animator animator;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        initialRotation = transform.localRotation;
        initialScale = transform.localScale;
        player = GameObject.Find("Player");
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (isInstantiate)
        {
            direction = player.GetComponent<PlayerController>().direction;
            isInstantiate = false;

            transform.localRotation = initialRotation;

            // reset tempat awal muncul & arah hadap skill

            switch (direction)
            {
                case "right":
                    spriteRenderer.sortingLayerName = "Skill Front";
                    transform.position = player.transform.position + new Vector3(1, 0, 0);
                    break;
                case "left":
                    spriteRenderer.sortingLayerName = "Skill Front";
                    transform.position = player.transform.position + new Vector3(-1, 0, 0);
                    transform.rotation = transform.rotation * Quaternion.Euler(0, 0, 180);
                    break;

                case "front":
                    spriteRenderer.sortingLayerName = "Skill Front";
                    transform.position = player.transform.position + new Vector3(0, -3, 0);
                    transform.rotation = transform.rotation * Quaternion.Euler(0, 0, -90);
                    break;

                case "back":

                    transform.position = player.transform.position + new Vector3(0, 3, 0);
                    transform.rotation = transform.rotation * Quaternion.Euler(0, 0, 90);
                    break;
            }

        }

        if (onAttack)
        {
            switch (direction)
            {
                case "right":
                    transform.position += Vector3.right * speed * Time.deltaTime;
                    break;
                case "left":
                    transform.position += Vector3.left * speed * Time.deltaTime;
                    break;
                case "front":
                    transform.position += Vector3.down * speed * Time.deltaTime;
                    break;
                case "back":
                    transform.position += Vector3.up * speed * Time.deltaTime;
                    break;
            }
        }

        if (
            transform.position.x > maxX ||
            transform.position.x < minX ||
            transform.position.y > maxY ||
            transform.position.y < minY
        )
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy") && onAttack)
        {
            MobController mob = other.GetComponent<MobController>();
            mob.hp -= damage;

            onAttack = false;
            animator.Play("fireball_end");
        }
    }

    private void OnAnimationAttack()
    {
        if (onAttack)
        {
            animator.Play("fireball_attack");
        }
    }


    private void OnAnimationEnd()
    {
        // isInstantiate = true;
        // isInstantiate = true;
        Destroy(gameObject);
    }
}
