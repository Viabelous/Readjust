using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public enum CrowdControlState
{
    normal,
    slide,
    slow,
    knockback,
    stun
}

// dikasih ke skill
public class CrowdControlSystem : MonoBehaviour
{


    public CharacterType type;
    [HideInInspector]
    public CrowdControlState state;
    [HideInInspector]
    public bool isSlid, isKnocked, isSlowed, isStunned;
    [HideInInspector]
    public float slideSpeed, slideDistance, knockSpeed, knockDistance, slowedSpeed, stunTimer;
    [HideInInspector]
    public Vector2 slideDirection;
    [HideInInspector]
    public Vector3 knockDirection;
    [HideInInspector]
    private Vector3 initialPosSlide, initialPosKnock;

    [HideInInspector]
    public float initialSpeed;

    void Start()
    {
        state = CrowdControlState.normal;

        initialSpeed = type == CharacterType.player ?
                        GetComponent<PlayerController>().player.movementSpeed :
                        GetComponent<MobController>().enemy.movementSpeed;
    }

    void Update()
    {

        switch (type)
        {
            case CharacterType.player:


                break;


            case CharacterType.enemy:

                if (isSlid)
                {
                    GetComponent<MobController>().movementEnabled = false;
                    EnemySliding();
                }
                else
                {
                    initialPosSlide = transform.position;
                }

                if (isKnocked)
                {
                    GetComponent<MobController>().movementEnabled = true;
                    EnemyKnockedBack();
                }
                else
                {
                    initialPosKnock = transform.position;
                }

                if (isSlowed)
                {
                    GetComponent<MobController>().speed = slowedSpeed;
                }
                else
                {
                    GetComponent<MobController>().speed = initialSpeed;
                }

                if (isStunned)
                {
                    GetComponent<MobController>().movementEnabled = false;
                }
                else
                {
                    GetComponent<MobController>().movementEnabled = true;
                }
                break;
        }

    }

    private void EnemySliding()
    {
        if (Vector3.Distance(initialPosSlide, transform.position) >= slideDistance)
        {
            isSlid = false;
        }
        transform.Translate(slideDirection * slideSpeed * Time.deltaTime);
    }

    private void EnemyKnockedBack()
    {
        if (Vector3.Distance(initialPosKnock, transform.position) >= knockDistance)
        {
            isKnocked = false;
        }

        // Vector3 direction = -(player.transform.position - transform.position).normalized;
        transform.Translate(knockDirection * knockSpeed * Time.deltaTime);
    }



}