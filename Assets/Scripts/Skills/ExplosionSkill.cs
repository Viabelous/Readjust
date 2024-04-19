using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionSkill : MonoBehaviour
{
    public float damage = 5;
    public float knockBackSpeed = 5, knockBackTimer = 0.3f;


    private string direction = "front";
    private bool isInstantiate = true;

    private Quaternion initialRotation;
    private Vector3 initialScale;

    private GameObject player;

    private SpriteRenderer spriteRenderer;


    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        initialRotation = transform.localRotation;
        initialScale = transform.localScale;
        player = GameObject.Find("Player");
    }

    private void Update()
    {
        if (isInstantiate)
        {
            direction = player.GetComponent<PlayerController>().direction;
            isInstantiate = false;

            transform.localRotation = initialRotation;

            if (direction == "back")
            {
                spriteRenderer.sortingOrder = 3;
            }
            else
            {
                spriteRenderer.sortingOrder = 20;
            }

            // reset tempat awal muncul & arah hadap skill

            switch (direction)
            {
                case "right":
                    spriteRenderer.sortingLayerName = "Skill Front";
                    transform.position = player.transform.position + new Vector3(3, 0, 0);
                    break;
                case "left":
                    spriteRenderer.sortingLayerName = "Skill Front";
                    transform.position = player.transform.position + new Vector3(-3, 0, 0);
                    transform.rotation = transform.rotation * Quaternion.Euler(0, 0, 180);
                    break;

                case "front":
                    spriteRenderer.sortingLayerName = "Skill Front";
                    transform.position = player.transform.position + new Vector3(0.5f, -3, 0);
                    transform.rotation = transform.rotation * Quaternion.Euler(0, 0, -90);
                    break;

                case "back":
                    spriteRenderer.sortingLayerName = "Skill Back";
                    transform.position = player.transform.position + new Vector3(0, 3, 0);
                    transform.rotation = transform.rotation * Quaternion.Euler(0, 0, 90);
                    break;
            }

        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            MobController mob = other.GetComponent<MobController>();
            mob.hp -= damage;
            mob.onKnockBack = true;
            mob.knockBackSpeed = knockBackSpeed;
            mob.knockBackTimer = knockBackTimer;

        }
    }


    private void OnAnimationEnd()
    {
        isInstantiate = true;
        Destroy(gameObject);
    }

}
