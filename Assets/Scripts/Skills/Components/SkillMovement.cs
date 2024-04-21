using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public enum SkillMovementType
{
    Linear, // gerak lurus sesuai arah hadap
    Locking, // geraknya mengikuti pergerakan musuh yg dilock
    OnPlayer,
    OnCamera
}

public class SkillMovement : MonoBehaviour
{
    private GameObject player;

    [SerializeField]
    private float speed, range;
    [SerializeField]
    private Vector3 offset;


    [SerializeField]
    public SkillMovementType type;
    private float face;
    private ProjectileSkill projectileSkill;
    private bool onInstantiate;
    private Vector3 initialPosition;
    private Animator animator;
    private bool onCamera;


    void Start()
    {
        player = GameObject.FindWithTag("Player");
        face = player.GetComponent<Animator>().GetFloat("Face");
        projectileSkill = GetComponent<ProjectileSkill>();
        animator = GetComponent<Animator>();
        onCamera = true;
        onInstantiate = true;

    }

    // Update is called once per frame
    void Update()
    {
        if (onInstantiate)
        {
            initialPosition = transform.position;
            onInstantiate = false;
        }

        if (projectileSkill != null && !projectileSkill.isMoving)
        {
            return;
        }

        switch (type)
        {
            case SkillMovementType.Linear:
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

                if (Vector3.Distance(initialPosition, transform.position) > range)
                {
                    Destroy(gameObject);
                }

                break;

            case SkillMovementType.OnPlayer:
                transform.position = player.transform.position;
                break;

            case SkillMovementType.OnCamera:
                if (onCamera)
                {
                    transform.position = Camera.main.transform.position + new Vector3(0, 0, 10);
                }
                else
                {
                    Vector3 direction = (player.transform.position + offset - transform.position).normalized;
                    transform.Translate(direction * (speed > 0 ? speed : 1) * Time.deltaTime);

                }
                // if (animator.GetBool("onCamera"))
                // {
                //     transform.position = Camera.main.transform.position + new Vector3(0, 0, 10);
                // }
                // else
                // {
                //     Vector3 direction = (player.transform.position + offset - transform.position).normalized;
                //     transform.Translate(direction * (speed > 0 ? speed : 1) * Time.deltaTime);
                // }
                break;
        }

    }

    public void onPlayerAnimation()
    {
        // animator.SetBool("onCamera", false);
        onCamera = false;
    }

    // public void onAttackAnimation()
    // {
    //     // animator.SetTrigger("onAttack");
    //     animator.SetBool("onAttack", true);
    // }


}
