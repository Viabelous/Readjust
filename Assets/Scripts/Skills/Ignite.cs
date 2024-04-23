using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Ignite : Skill
{
    private float moveHorizontal, moveVertical;
    private Quaternion initialRotation;
    private Vector3 initialScale = new Vector3(1.2f, 1.2f, 1.2f);
    private ChrDirection face;
    // public SpriteRenderer spriteRenderer;
    private PolygonCollider2D polyCollider;

    public override void Activate(GameObject gameObject)
    {
        // spriteRenderer = GetComponent<SpriteRenderer>();
        polyCollider = gameObject.GetComponent<PolygonCollider2D>();
        initialRotation = gameObject.transform.localRotation;
        initialScale = gameObject.transform.localScale;

        moveHorizontal = Input.GetAxis("Horizontal");
        moveVertical = Input.GetAxis("Vertical");


        gameObject.transform.localRotation = initialRotation;

        face = GameObject.Find("Player").GetComponent<PlayerController>().direction;
        switch (face)
        {
            // kanan
            case ChrDirection.right:
                gameObject.transform.localScale = new Vector3(
                -initialScale.x,
                initialScale.y,
                initialScale.z
            );
                break;
            // kiri
            case ChrDirection.left:
                gameObject.transform.localScale = initialScale; break;
            // depan
            case ChrDirection.front:
                gameObject.transform.localScale = new Vector3(
                initialScale.x,
                -initialScale.y,
                initialScale.z
            );
                gameObject.transform.rotation = gameObject.transform.rotation * Quaternion.Euler(0, 0, 90);
                break;
            // belakang
            case ChrDirection.back:
                gameObject.transform.localScale = initialScale;
                gameObject.transform.rotation = gameObject.transform.rotation * Quaternion.Euler(0, 0, -90);
                break;
        }

        // print(initialScale);
    }

}
