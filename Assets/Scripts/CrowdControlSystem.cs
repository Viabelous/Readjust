using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


// dikasih ke skill
public class CrowdControlSystem : MonoBehaviour
{

    public CharacterType type;
    private List<CrowdControl> ccsActive = new List<CrowdControl>();

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

        initialSpeed = type == CharacterType.Player ?
                        GetComponent<PlayerController>().player.movementSpeed :
                        GetComponent<MobController>().enemy.movementSpeed;
    }

    void Update()
    {

        if (CheckCC(CrowdControlType.Slide))
        {
            int index = ccsActive.FindIndex(cc => cc.type == CrowdControlType.Slide);
            CCSlide slide = (CCSlide)ccsActive[index];

            transform.Translate(slide.backward * slide.speed * Time.deltaTime);

            if (Vector3.Distance(slide.initialPos, transform.position) >= slide.range)
            {
                ccsActive.RemoveAt(index);
            }
        }

        if (CheckCC(CrowdControlType.KnockBack))
        {
            int index = ccsActive.FindIndex(cc => cc.type == CrowdControlType.KnockBack);
            CCKnockBack knockBack = (CCKnockBack)ccsActive[index];

            transform.Translate(knockBack.direction * knockBack.speed * Time.deltaTime);

            if (Vector3.Distance(knockBack.initialPos, transform.position) >= knockBack.range)
            {
                ccsActive.RemoveAt(index);
            }
        }

        // if (CheckCC(CrowdControlType.Slow))
        // {
        //     int index = ccsActive.FindIndex(cc => cc.type == CrowdControlType.KnockBack);
        //     CCSlow slow = (CCSlow)ccsActive[index];
        //     if ()
        //     StartCoroutine(SlowCoroutine(slow));
        // }

        // switch (type)
        // {
        //     case CharacterType.Player:


        //         break;


        //     case CharacterType.Enemy:

        //         if (isSlid)
        //         {
        //             GetComponent<MobController>().movementEnabled = false;
        //             EnemySliding();
        //         }
        //         else
        //         {
        //             initialPosSlide = transform.position;
        //         }

        //         if (isKnocked)
        //         {
        //             GetComponent<MobController>().movementEnabled = true;
        //             EnemyKnockedBack();
        //         }
        //         else
        //         {
        //             initialPosKnock = transform.position;
        //         }

        //         if (isSlowed)
        //         {
        //             GetComponent<MobController>().speed = slowedSpeed;
        //         }
        //         else
        //         {
        //             GetComponent<MobController>().speed = initialSpeed;
        //         }

        //         if (isStunned)
        //         {
        //             GetComponent<MobController>().movementEnabled = false;
        //         }
        //         else
        //         {
        //             GetComponent<MobController>().movementEnabled = true;
        //         }
        //         break;
        // }

    }

    public bool CheckCC(CrowdControlType type)
    {
        if (ccsActive.FindIndex(cc => cc.type == type) != -1)
        {
            return true;
        }
        return false;
    }

    public void ActivateCC(CrowdControl cc)
    {
        switch (cc.type)
        {
            case CrowdControlType.Slide:
            case CrowdControlType.KnockBack:
                ccsActive.Add(cc);
                break;
            case CrowdControlType.Slow:
                ccsActive.Add(cc);
                StartCoroutine(SlowCoroutine((CCSlow)cc));
                break;
        }
        ccsActive.Add(cc);
    }

    public void DactivateCC(string ccId)
    {
        int index = ccsActive.FindIndex(cc => cc.id == ccId);
        ccsActive.RemoveAt(index);
    }

    public IEnumerator SlowCoroutine(CCSlow cc)
    {
        MobController mob = GetComponent<MobController>();
        mob.speed -= mob.speed * cc.slow;
        yield return new WaitForSeconds(cc.timer);
        mob.speed = cc.initialSpeed;
        ccsActive.Remove(cc);
    }

    //     public void ActivateSliding(float speed, float distance)
    //     {
    //         isSlid = true;
    //         slideSpeed = speed;
    //         slideDistance = distance;
    //     }

    //     private void EnemySliding()
    //     {
    //         if (Vector3.Distance(initialPosSlide, transform.position) >= slideDistance)
    //         {
    //             isSlid = false;
    //         }
    //         transform.Translate(slideDirection * slideSpeed * Time.deltaTime);
    //     }

    //     public void DeactivateSliding()
    //     {
    //         isSlid = false;
    //     }

    //     public void ActivateKnockBack(float speed, float distance, Vector3 gameObjectPos)
    //     {
    //         isKnocked = true;
    //         knockSpeed = speed;
    //         knockDistance = distance;
    //         knockDirection = -(gameObjectPos - transform.position).normalized;
    //     }

    //     private void EnemyKnockedBack()
    //     {
    //         if (Vector3.Distance(initialPosKnock, transform.position) >= knockDistance)
    //         {
    //             isKnocked = false;
    //         }

    //         // Vector3 direction = -(player.transform.position - transform.position).normalized;
    //         transform.Translate(knockDirection * knockSpeed * Time.deltaTime);
    //     }

    //     public void DeactivateKnockBack()
    //     {
    //         isKnocked = false;
    //     }

    //     public void ActivateSlowing(float slow)
    //     {
    //         isSlowed = true;
    //         slowedSpeed = initialSpeed - initialSpeed * slow;
    //     }

    //     public void DeactivateSlowing()
    //     {
    //         isSlowed = false;
    //     }
    // }
}