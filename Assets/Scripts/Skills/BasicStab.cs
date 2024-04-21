using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class BasicStab : Skill
{

    [SerializeField]
    private float speed;

    // private string direction;
    private GameObject player;
    private Quaternion initialRotation;
    private Vector3 initialScale;
    private SpriteRenderer spriteRenderer;



    public override void Activate(GameObject gameObject)
    {
        // Instantiate(gameObject, player.transform);

        player = GameObject.FindWithTag("Player");


        initialRotation = gameObject.transform.localRotation;
        initialScale = gameObject.transform.localScale;
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();

        // direction = player.GetComponent<PlayerController>().direction;

        gameObject.transform.localRotation = initialRotation;
        float face = player.GetComponent<Animator>().GetFloat("Face");


        // reset tempat awal muncul & arah hadap skill

        switch (face)
        {
            // kanan
            case 1:
                spriteRenderer.sortingLayerName = "Skill Front";
                gameObject.transform.rotation = gameObject.transform.rotation * Quaternion.Euler(0, 0, 180);

                break;
            // kiri
            case 3:
                spriteRenderer.sortingLayerName = "Skill Front";
                break;
            // depan
            case 0:
                spriteRenderer.sortingLayerName = "Skill Front";
                gameObject.transform.rotation = gameObject.transform.rotation * Quaternion.Euler(0, 0, 90);
                break;
            // belakang
            case 2:
                gameObject.transform.rotation = gameObject.transform.rotation * Quaternion.Euler(0, 0, -90);
                spriteRenderer.sortingLayerName = "Skill Back";
                break;
        }

        // setelah diatur rotasi gambar dari skill, letakkan skill di titik player
        gameObject.transform.position = player.transform.position;

    }





    // private string direction = "front";
    // private bool isInstantiate = true;

    // private Quaternion initialRotation;
    // private Vector3 initialScale;

    // private GameObject player;


    // private SpriteRenderer spriteRenderer;


    // private void Awake()
    // {
    //     spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    // }

    // private void Start()
    // {
    //     initialRotation = gameObject.transform.localRotation;
    //     initialScale = gameObject.transform.localScale;
    //     player = GameObject.Find("Player");
    // }

    // private void Update()
    // {
    //     if (isInstantiate)
    //     {
    //         direction = player.GetComponent<PlayerController>().direction;
    //         isInstantiate = false;

    //         gameObject.transform.localRotation = initialRotation;

    //         if (direction == "back")
    //         {
    //             spriteRenderer.sortingOrder = 3;
    //         }
    //         else
    //         {
    //             spriteRenderer.sortingOrder = 20;
    //         }

    //         // reset tempat awal muncul & arah hadap skill

    //         switch (direction)
    //         {
    //             case "right":
    //                 spriteRenderer.sortingLayerName = "Skill Front";
    //                 gameObject.transform.rotation = gameObject.transform.rotation * Quaternion.Euler(0, 0, 90);

    //                 break;
    //             case "left":
    //                 spriteRenderer.sortingLayerName = "Skill Front";
    //                 gameObject.transform.rotation = gameObject.transform.rotation * Quaternion.Euler(0, 0, -90);

    //                 break;
    //             case "front":
    //                 spriteRenderer.sortingLayerName = "Skill Front";
    //                 break;

    //             case "back":
    //                 spriteRenderer.sortingLayerName = "Skill Back";
    //                 gameObject.transform.rotation = gameObject.transform.rotation * Quaternion.Euler(0, 0, 180);
    //                 break;
    //         }

    //         gameObject.transform.position = player.gameObject.transform.position;
    //     }


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