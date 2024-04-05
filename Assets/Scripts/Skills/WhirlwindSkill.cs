using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhirlwindSkill : MonoBehaviour
{
    private float damage = 10;
    private float speed = 10;
    private float swipe = 5;

    private string direction = "front";
    private bool isInstantiate = false;

    private Vector3 initialPosition;

    public GameObject player;
    public SpriteRenderer spriteRenderer;

    private void Start()
    {
        initialPosition = transform.position;
        player = GameObject.Find("Player");
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
            mob.swipe = swipe;

            // Vector2 backwardDirection = -transform.right;

            // Menggerakkan objek mundur secara horizontal menggunakan rb.MovePosition
            // mob.rb.MovePosition(mob.rb.position + backwardDirection * swipe * Time.fixedDeltaTime);

            // mob.isKnocked = true;
            // mob.knock = knock;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            MobController mob = other.GetComponent<MobController>();
            mob.isSwiped = false;

            // mob.isKnocked = false;
            // mob.knock = mob.ogKnock;
        }
    }

    public void Active()
    {
        gameObject.SetActive(true);
        isInstantiate = true;


        if (isInstantiate)
        {
            direction = player.GetComponent<PlayerController>().direction;
            isInstantiate = false;
        }
        // reset tempat awal muncul skill
        switch (direction)
        {
            case "right":
                spriteRenderer.sortingOrder = 20;
                transform.position = player.transform.position + new Vector3(0, initialPosition.y, initialPosition.z);
                break;
            case "left":
                spriteRenderer.sortingOrder = 20;
                transform.position = player.transform.position + new Vector3(0, initialPosition.y, initialPosition.z);
                break;
            case "front":
                spriteRenderer.sortingOrder = 20;
                transform.position = player.transform.position + new Vector3(0, -2, initialPosition.z);
                break;
            case "back":
                spriteRenderer.sortingOrder = -1;
                transform.position = player.transform.position + new Vector3(0, 2, initialPosition.z);
                break;
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
