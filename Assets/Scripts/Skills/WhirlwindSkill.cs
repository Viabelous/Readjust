using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhirlwindSkill : MonoBehaviour
{
    public float damage = 10;
    public float speed = 10;
    public float swipe = 5;
    public float manaUsage = 5;

    private string direction = "front";
    private bool isInstantiate = false;


    public GameObject player;
    public SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {

        if (gameObject.activeInHierarchy)
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
    }



    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            MobController mob = other.GetComponent<MobController>();
            mob.hp -= damage;

            mob.isSwiped = true;
            mob.swipe = swipe;

            switch (direction)
            {
                case "right":
                    mob.backward = mob.transform.right;
                    break;
                case "left":
                    mob.backward = -mob.transform.right;
                    break;
                case "front":
                    mob.backward = -mob.transform.up;
                    break;
                case "back":
                    mob.backward = mob.transform.up;
                    break;
            }
        }
    }


    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            MobController mob = other.GetComponent<MobController>();
            mob.isSwiped = false;
        }
    }

    public void Active()
    {
        gameObject.SetActive(true);
        isInstantiate = true;

        if (isInstantiate)
        {
            player = GameObject.Find("Player");
            direction = player.GetComponent<PlayerController>().direction;
            isInstantiate = false;

            // reset tempat awal muncul skill
            switch (direction)
            {
                case "right":
                    spriteRenderer.sortingOrder = 20;
                    transform.position = player.transform.position + new Vector3(2, 1, 0);
                    break;
                case "left":
                    spriteRenderer.sortingOrder = 20;
                    transform.position = player.transform.position + new Vector3(-2, 1, 0);
                    break;
                case "front":
                    spriteRenderer.sortingOrder = 20;
                    transform.position = player.transform.position;
                    break;
                case "back":
                    spriteRenderer.sortingOrder = -1;
                    transform.position = player.transform.position + new Vector3(0, 3, 0);
                    break;
            }
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
