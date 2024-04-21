using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Whirlwind : Skill
{
    [SerializeField]
    private float slideSpeed = 5, slideTimer = 0.3f;

    private GameObject player;
    private SpriteRenderer spriteRenderer;
    private Transform transform;

    public override void Activate(GameObject gameObject)
    {

        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        player = GameObject.Find("Player");


        // reset tempat awal muncul skill
        switch (player.GetComponent<Animator>().GetFloat("Face"))
        {
            case 1:
                gameObject.transform.position = player.transform.position + new Vector3(2, 1, 0);
                break;
            case 3:
                gameObject.transform.position = player.transform.position + new Vector3(-2, 1, 0);
                break;
            case 0:
                spriteRenderer.sortingLayerName = "Skill Front";
                gameObject.transform.position = player.transform.position;
                break;
            case 2:
                spriteRenderer.sortingLayerName = "Skill Back";
                gameObject.transform.position = player.transform.position + new Vector3(0, 2, 0);
                break;

        }
        transform = gameObject.transform;


    }


    public override void HitEnemy(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            // gameObject.GetComponent<AreaEffector2D>().forceMagnitude = 300;
            MobController mob = other.GetComponent<MobController>();
            mob.hp -= damage;

            mob.slideTimer = slideTimer;
            mob.slideSpeed = slideSpeed;
            mob.onSlide = true;

            switch (player.GetComponent<PlayerController>().direction)
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


}
