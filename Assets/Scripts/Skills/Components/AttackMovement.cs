using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AttackMovementType
{
    Linear, // gerak lurus sesuai arah hadap
    Locking, // geraknya mengikuti pergerakan musuh yg dilock
    OnPlayer // gerak mengikuti posisi player
}

public class AttackMovement : MonoBehaviour
{
    private GameObject player;

    [SerializeField]
    private float speed;

    [SerializeField]
    private AttackMovementType type;
    private float face;
    private ProjectileAttack projectileAttack;


    void Start()
    {
        player = GameObject.FindWithTag("Player");
        face = player.GetComponent<Animator>().GetFloat("Face");
        projectileAttack = GetComponent<ProjectileAttack>();
        // try
        // {
        // }
        // catch (Exception ex)
        // {
        //     Debug.Log(ex);
        // }
    }

    // Update is called once per frame
    void Update()
    {
        if (projectileAttack != null && !projectileAttack.isMoving)
        {
            return;
        }

        switch (type)
        {
            case AttackMovementType.Linear:
                switch (face)
                {
                    // kanan
                    case 1:
                        transform.position += Vector3.right * speed * Time.deltaTime;
                        break;
                    // kiri
                    case 3:
                        transform.position += Vector3.left * speed * Time.deltaTime;
                        break;
                    // depan
                    case 0:
                        transform.position += Vector3.down * speed * Time.deltaTime;
                        break;
                    // belakang
                    case 2:
                        transform.position += Vector3.up * speed * Time.deltaTime;
                        break;
                }
                break;

            case AttackMovementType.OnPlayer:
                transform.position = player.transform.position;
                break;
        }

    }
}
