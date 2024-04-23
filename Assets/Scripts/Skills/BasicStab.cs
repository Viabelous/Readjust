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
    private ChrDirection direction;


    public override void Activate(GameObject gameObject)
    {

        player = GameObject.FindWithTag("Player");
        this.damage = player.GetComponent<PlayerController>().player.atk;

        initialRotation = gameObject.transform.localRotation;
        initialScale = gameObject.transform.localScale;
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();

        direction = player.GetComponent<PlayerController>().direction;

        gameObject.transform.localRotation = initialRotation;
        float face = player.GetComponent<Animator>().GetFloat("Face");


        // reset tempat awal muncul & arah hadap skill

        switch (direction)
        {
            // kanan
            case ChrDirection.right:
                spriteRenderer.sortingLayerName = "Skill Front";
                gameObject.transform.rotation = gameObject.transform.rotation * Quaternion.Euler(0, 0, 180);
                break;
            // kiri
            case ChrDirection.left:
                spriteRenderer.sortingLayerName = "Skill Front";
                break;
            // depan
            case ChrDirection.front:
                spriteRenderer.sortingLayerName = "Skill Front";
                gameObject.transform.rotation = gameObject.transform.rotation * Quaternion.Euler(0, 0, 90);
                break;
            // belakang
            case ChrDirection.back:
                gameObject.transform.rotation = gameObject.transform.rotation * Quaternion.Euler(0, 0, -90);
                spriteRenderer.sortingLayerName = "Skill Back";
                break;
        }

        // setelah diatur rotasi gambar dari skill, letakkan skill di titik player
        gameObject.transform.position = player.transform.position;

    }


}