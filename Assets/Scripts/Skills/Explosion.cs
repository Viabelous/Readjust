using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Explosion : Skill
{
    public float knockSpeed, knockDistance;


    private ChrDirection direction;

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

        // reset tempat awal muncul & arah hadap skill
        switch (direction)
        {
            case ChrDirection.right:
                spriteRenderer.sortingLayerName = "Skill Front";
                gameObject.transform.position = player.transform.position + new Vector3(3, 0, 0);
                break;
            case ChrDirection.left:
                spriteRenderer.sortingLayerName = "Skill Front";
                gameObject.transform.position = player.transform.position + new Vector3(-3, 0, 0);
                gameObject.transform.rotation = gameObject.transform.rotation * Quaternion.Euler(0, 0, 180);
                break;

            case ChrDirection.front:
                spriteRenderer.sortingLayerName = "Skill Front";
                gameObject.transform.position = player.transform.position + new Vector3(0.25f, -3, 0);
                gameObject.transform.rotation = gameObject.transform.rotation * Quaternion.Euler(0, 0, -90);
                break;

            case ChrDirection.back:
                spriteRenderer.sortingLayerName = "Skill Back";
                gameObject.transform.position = player.transform.position + new Vector3(-0.25f, 3, 0);
                gameObject.transform.rotation = gameObject.transform.rotation * Quaternion.Euler(0, 0, 90);
                break;
        }

    }

    public override void HitEnemy(GameObject gameObject, Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            CrowdControlSystem mob = other.GetComponent<CrowdControlSystem>();
            mob.isKnocked = true;
            mob.knockSpeed = knockSpeed;
            mob.knockDistance = knockDistance;
            mob.knockDirection = -(player.transform.position - gameObject.transform.position).normalized;
        }
    }

    public override void AfterHitEnemy(GameObject gameObject, Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            CrowdControlSystem mob = other.GetComponent<CrowdControlSystem>();
            mob.isKnocked = false;
        }
    }

}
