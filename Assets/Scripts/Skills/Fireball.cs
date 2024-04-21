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


    public override void Activate(GameObject gameObject)
    {

        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        initialRotation = gameObject.transform.localRotation;
        initialScale = gameObject.transform.localScale;
        player = GameObject.Find("Player");

        gameObject.transform.localRotation = initialRotation;
        float face = player.GetComponent<Animator>().GetFloat("Face");

        switch (face)
        {
            case 1:
                spriteRenderer.sortingLayerName = "Skill Front";
                gameObject.transform.position = player.transform.position + new Vector3(2, 0, 0);
                break;
            case 3:
                spriteRenderer.sortingLayerName = "Skill Front";
                gameObject.transform.position = player.transform.position + new Vector3(-2, 0, 0);
                gameObject.transform.rotation = gameObject.transform.rotation * Quaternion.Euler(0, 0, 180);
                break;

            case 0:
                spriteRenderer.sortingLayerName = "Skill Front";
                gameObject.transform.position = player.transform.position + new Vector3(0, -2, 0);
                gameObject.transform.rotation = gameObject.transform.rotation * Quaternion.Euler(0, 0, -90);
                break;

            case 2:

                gameObject.transform.position = player.transform.position + new Vector3(0, 2, 0);
                gameObject.transform.rotation = gameObject.transform.rotation * Quaternion.Euler(0, 0, 90);
                break;
        }

        // if (
        //     gameObject.transform.position.x > maxX ||
        //     gameObject.transform.position.x < minX ||
        //     gameObject.transform.position.y > maxY ||
        //     gameObject.transform.position.y < minY
        // )
        // {
        //     Destroy(gameObject);
        // }
    }


}
