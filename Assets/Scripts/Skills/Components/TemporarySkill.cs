using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TemporarySkill : MonoBehaviour // skill pake waktu
{

    [SerializeField]
    private string attackAnimationName, endAnimationName;

    [SerializeField]
    private float activeTime, offsetTime;
    private float timer;
    private bool isAttacking;

    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        timer = activeTime - offsetTime;
        isAttacking = true;
    }

    // Update is called once per frame
    void Update()
    {

        if (isAttacking)
        {
            timer -= Time.deltaTime;
        }

        if (timer <= 0 && isAttacking)
        {
            isAttacking = false;
            animator.Play(endAnimationName);
        }
    }

    private void OnAnimationAttack()
    {
        animator.Play(attackAnimationName);
    }

    private void OnAnimationEnd()
    {

        Destroy(gameObject);

    }
}
