using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ProjectileAttack : MonoBehaviour
{

    [SerializeField]
    private string attackAnimationName, endAnimationName;

    [HideInInspector]
    public bool isMoving; // untuk projectile 

    // [SerializeField]
    // private float attackTimer; // untuk temporary
    // private float timer;

    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        isMoving = true;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy") && isMoving)
        {
            isMoving = false;
            animator.Play(endAnimationName);
        }
    }

    private void OnAnimationAttack()
    {
        if (isMoving)
        {
            animator.Play(attackAnimationName);
        }
    }

    private void OnAnimationEnd()
    {
        Destroy(gameObject);
    }
}
