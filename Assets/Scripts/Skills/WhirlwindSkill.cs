using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhirlwindSkill : MonoBehaviour
{
    [SerializeField]
    private float damage = 10,
                speed = 10, slideSpeed = 5, slideTimer = 0.3f;

    private string direction = "front";
    private bool isInstantiate = true;


    private GameObject player;
    private SpriteRenderer spriteRenderer;


    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        player = GameObject.Find("Player");

    }

    private void Update()
    {

        if (isInstantiate)
        {
            isInstantiate = false;
            direction = player.GetComponent<PlayerController>().direction;

            // reset tempat awal muncul skill
            switch (direction)
            {
                case "right":
                    transform.position = player.transform.position + new Vector3(2, 1, 0);
                    break;
                case "left":
                    transform.position = player.transform.position + new Vector3(-2, 1, 0);
                    break;
                case "front":
                    spriteRenderer.sortingLayerName = "Skill Front";
                    transform.position = player.transform.position;
                    break;
                case "back":
                    spriteRenderer.sortingLayerName = "Skill Back";
                    transform.position = player.transform.position + new Vector3(0, 2, 0);
                    break;
            }
        }

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



    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            // gameObject.GetComponent<AreaEffector2D>().forceMagnitude = 300;
            MobController mob = other.GetComponent<MobController>();
            mob.hp -= damage;

            mob.slideTimer = slideTimer;
            mob.slideSpeed = slideSpeed;
            mob.onSlide = true;

            switch (direction)
            {
                case "right":
                    mob.backward = transform.right;
                    break;
                case "left":
                    mob.backward = -transform.right;
                    break;
                case "front":
                    // cuma bisa untuk mob yang ada di bawah player
                    if (mob.transform.position.y < player.transform.position.y)
                    {
                        mob.backward = -transform.up;
                    }
                    break;
                case "back":
                    // cuma bisa untuk mob yang ada di atas player
                    if (mob.transform.position.y > player.transform.position.y)
                    {
                        mob.backward = transform.up;
                    }
                    break;
            }

        }

    }



    // private void OnTriggerExit2D(Collider2D other)
    // {
    //     if (other.CompareTag("Enemy"))
    //     {
    //         MobController mob = other.GetComponent<MobController>();
    //         mob.isSwiped = false;
    //     }
    // }

    // public void Active()
    // {
    //     // gameObject.SetActive(true);
    //     // isInstantiate = true;





    // }


    // public void Deactive()
    // {
    //     gameObject.SetActive(false);
    // }

    private void OnAnimationEnd()
    {
        isInstantiate = true;
        Destroy(gameObject);
        // Deactive();
    }
}
