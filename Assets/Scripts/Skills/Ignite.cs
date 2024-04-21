using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Ignite : Skill
{
    private float moveHorizontal, moveVertical;
    private Quaternion initialRotation;
    private Vector3 initialScale = new Vector3(1.2f, 1.2f, 1.2f);
    private float face;
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

        face = GameObject.Find("Player").GetComponent<Animator>().GetFloat("Face");
        switch (face)
        {
            // kanan
            case 1:
                gameObject.transform.localScale = new Vector3(
                -initialScale.x,
                initialScale.y,
                initialScale.z
            );
                break;
            // kiri
            case 3:
                gameObject.transform.localScale = initialScale; break;
            // depan
            case 0:
                gameObject.transform.localScale = new Vector3(
                initialScale.x,
                -initialScale.y,
                initialScale.z
            );
                gameObject.transform.rotation = gameObject.transform.rotation * Quaternion.Euler(0, 0, 90);
                break;
            // belakang
            case 2:
                gameObject.transform.localScale = initialScale;
                gameObject.transform.rotation = gameObject.transform.rotation * Quaternion.Euler(0, 0, -90);
                break;
        }

        // print(initialScale);
    }

    // private void Update()
    // {
    //     if (isInstantiate)
    //     {

    //     }

    //     transform.position = GameObject.Find("Player").transform.position;

    // }


    // private void OnTriggerEnter2D(Collider2D other)
    // {
    //     if (other.CompareTag("Enemy"))
    //     {
    //         MobController mob = other.GetComponent<MobController>();
    //         mob.hp -= damage;
    //     }
    // }



    // private void OnAnimationEnd()
    // {
    //     isInstantiate = true;
    //     Destroy(gameObject);
    // }
}
