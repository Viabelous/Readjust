using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Explosion : Skill
{
    public float knockBackSpeed = 5, knockBackTimer = 0.3f;


    private string direction = "front";

    private Quaternion initialRotation;
    private Vector3 initialScale;

    private GameObject player;

    private SpriteRenderer spriteRenderer;


    public override void Activate(GameObject gameObject)
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        initialRotation = gameObject.transform.localRotation;
        initialScale = gameObject.transform.localScale;
        player = GameObject.Find("Player");


        direction = player.GetComponent<PlayerController>().direction;

        gameObject.transform.localRotation = initialRotation;

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
                gameObject.transform.position = player.transform.position + new Vector3(3, 0, 0);
                break;
            case "left":
                spriteRenderer.sortingLayerName = "Skill Front";
                gameObject.transform.position = player.transform.position + new Vector3(-3, 0, 0);
                gameObject.transform.rotation = gameObject.transform.rotation * Quaternion.Euler(0, 0, 180);
                break;

            case "front":
                spriteRenderer.sortingLayerName = "Skill Front";
                gameObject.transform.position = player.transform.position + new Vector3(0.25f, -3, 0);
                gameObject.transform.rotation = gameObject.transform.rotation * Quaternion.Euler(0, 0, -90);
                break;

            case "back":
                spriteRenderer.sortingLayerName = "Skill Back";
                gameObject.transform.position = player.transform.position + new Vector3(-0.25f, 3, 0);
                gameObject.transform.rotation = gameObject.transform.rotation * Quaternion.Euler(0, 0, 90);
                break;
        }

    }

    public override void HitEnemy(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            MobController mob = other.GetComponent<MobController>();
            mob.onKnockBack = true;
            mob.knockBackSpeed = knockBackSpeed;
            mob.knockBackTimer = knockBackTimer;

        }
    }

}
