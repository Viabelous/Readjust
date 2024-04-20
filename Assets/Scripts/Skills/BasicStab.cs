using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicStab : MonoBehaviour
{
    public float damage = 5, speed = 10;


    private string direction = "front";
    private bool isInstantiate = true;

    private Quaternion initialRotation;
    private Vector3 initialScale;

    private GameObject player;


    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
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
                    transform.rotation = transform.rotation * Quaternion.Euler(0, 0, 90);

                    break;
                case "left":
                    spriteRenderer.sortingLayerName = "Skill Front";
                    transform.rotation = transform.rotation * Quaternion.Euler(0, 0, -90);

                    break;
                case "front":
                    spriteRenderer.sortingLayerName = "Skill Front";
                    break;

                case "back":
                    spriteRenderer.sortingLayerName = "Skill Back";
                    transform.rotation = transform.rotation * Quaternion.Euler(0, 0, 180);
                    break;
            }

            transform.position = player.transform.position;
        }


    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            MobController mob = other.GetComponent<MobController>();
            mob.hp -= damage;


        }
    }


    private void OnAnimationEnd()
    {
        isInstantiate = true;
        Destroy(gameObject);
    }



}