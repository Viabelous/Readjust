using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class IgniteSkill : MonoBehaviour
{
    private float damage = 50;
    private float moveHorizontal, moveVertical;
    private Quaternion initialRotation;
    private Vector3 initialScale = new Vector3(1.2f, 1.2f, 1.2f);
    // public SpriteRenderer spriteRenderer;
    public PolygonCollider2D polyCollider;

    private void Start()
    {
        // spriteRenderer = GetComponent<SpriteRenderer>();
        polyCollider = GetComponent<PolygonCollider2D>();
        initialRotation = transform.localRotation;
        initialScale = transform.localScale;
        // print(initialScale);
    }

    private void Update()
    {
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            MobController mob = other.GetComponent<MobController>();
            mob.hp -= damage;
        }
    }

    public void Active()
    {
        gameObject.SetActive(true);

        moveHorizontal = Input.GetAxis("Horizontal");
        moveVertical = Input.GetAxis("Vertical");

        transform.localRotation = initialRotation;
        // transform.localScale = initialScale;

        // kanan
        if (moveHorizontal > 0f)
        {
            // spriteRenderer.flipX = true;
            // spriteRenderer.flipY = false;
            // transform.localScale = new Vector3(-1.2f, 1.2f, 1.2f);
            transform.localScale = new Vector3(
                -initialScale.x,
                initialScale.y,
                initialScale.z
            );
        }

        // kiri
        else if (moveHorizontal < 0f)
        {
            transform.localScale = initialScale;
        }

        // depan
        else if (moveVertical < 0f)
        {
            // spriteRenderer.flipY = true;
            // transform.localScale = new Vector3(1.2f, -1.2f, 1.2f);
            transform.localScale = new Vector3(
                initialScale.x,
                -initialScale.y,
                initialScale.z
            );
            transform.rotation = transform.rotation * Quaternion.Euler(0, 0, 90);
        }
        // belakang
        else if (moveVertical > 0f)
        {
            transform.localScale = initialScale;
            transform.rotation = transform.rotation * Quaternion.Euler(0, 0, -90);
        }

        // polyCollider.transform.


    }

    public void ResetRotation()
    {
        transform.rotation = initialRotation;
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
