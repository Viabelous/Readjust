using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Fireball : Skill
{

    // public float maxX, minX, maxY, minY;

    private Quaternion initialRotation;
    private Vector3 initialScale;

    private GameObject player;

    private SpriteRenderer spriteRenderer;
    private ChrDirection direction;


    public override void Activate(GameObject gameObject)
    {

        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        initialRotation = gameObject.transform.localRotation;
        initialScale = gameObject.transform.localScale;
        player = GameObject.Find("Player");

        gameObject.transform.localRotation = initialRotation;
        direction = player.GetComponent<PlayerController>().direction;

        switch (direction)
        {
            case ChrDirection.right:
                spriteRenderer.sortingLayerName = "Skill Front";
                gameObject.transform.position = player.transform.position + new Vector3(2, 0, 0);
                break;
            case ChrDirection.left:
                spriteRenderer.sortingLayerName = "Skill Front";
                gameObject.transform.position = player.transform.position + new Vector3(-2, 0, 0);
                gameObject.transform.rotation = gameObject.transform.rotation * Quaternion.Euler(0, 0, 180);
                break;

            case ChrDirection.front:
                spriteRenderer.sortingLayerName = "Skill Front";
                gameObject.transform.position = player.transform.position + new Vector3(0, -2, 0);
                gameObject.transform.rotation = gameObject.transform.rotation * Quaternion.Euler(0, 0, -90);
                break;

            case ChrDirection.back:

                gameObject.transform.position = player.transform.position + new Vector3(0, 2, 0);
                gameObject.transform.rotation = gameObject.transform.rotation * Quaternion.Euler(0, 0, 90);
                break;
        }
    }

}
