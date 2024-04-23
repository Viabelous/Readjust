using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Whirlwind : Skill
{
    [SerializeField]
    private float slideSpeed, slideDistance;

    private GameObject player;
    private ChrDirection direction;
    private SpriteRenderer spriteRenderer;
    private Transform transform;

    public override void Activate(GameObject gameObject)
    {

        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        player = GameObject.Find("Player");
        direction = player.GetComponent<PlayerController>().direction;

        // reset tempat awal muncul skill
        switch (direction)
        {
            case ChrDirection.right:
                gameObject.transform.position = player.transform.position + new Vector3(2, 1, 0);
                break;
            case ChrDirection.left:
                gameObject.transform.position = player.transform.position + new Vector3(-2, 1, 0);
                break;
            case ChrDirection.front:
                spriteRenderer.sortingLayerName = "Skill Front";
                gameObject.transform.position = player.transform.position;
                break;
            case ChrDirection.back:
                spriteRenderer.sortingLayerName = "Skill Back";
                gameObject.transform.position = player.transform.position + new Vector3(0, 2, 0);
                break;

        }
        transform = gameObject.transform;


    }


    public override void HitEnemy(GameObject gameObject, Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {

            // gameObject.GetComponent<AreaEffector2D>().forceMagnitude = 300;
            CrowdControlSystem mob = other.GetComponent<CrowdControlSystem>();

            mob.isSlid = true;
            mob.slideSpeed = slideSpeed;
            mob.slideDistance = slideDistance;

            switch (direction)
            {
                case ChrDirection.right:
                    mob.slideDirection = transform.right;
                    break;
                case ChrDirection.left:
                    mob.slideDirection = -transform.right;
                    break;

                case ChrDirection.front:
                    // cuma bisa untuk mob yang ada di bawah player
                    if (mob.transform.position.y < player.transform.position.y)
                    {
                        mob.slideDirection = -transform.up;
                    }
                    break;

                case ChrDirection.back:
                    // cuma bisa untuk mob yang ada di atas player
                    if (mob.transform.position.y > player.transform.position.y)
                    {
                        mob.slideDirection = transform.up;
                    }
                    break;
            }

        }

    }
    public override void AfterHitEnemy(GameObject gameObject, Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            CrowdControlSystem mob = other.GetComponent<CrowdControlSystem>();
            mob.isSlid = false;
        }
    }

}
