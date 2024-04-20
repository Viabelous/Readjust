using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TemporaryAttack : MonoBehaviour
{

    [SerializeField]
    private string attackAnimationName, endAnimationName;

    [SerializeField]
    private float attackTime, offsetTime;
    private float timer;
    private bool isMoving;

    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        timer = attackTime - offsetTime;
        isMoving = true;
    }

    // Update is called once per frame
    void Update()
    {

        if (isMoving)
        {
            timer -= Time.deltaTime;
        }

        if (timer <= 0 && isMoving)
        {
            isMoving = false;
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
