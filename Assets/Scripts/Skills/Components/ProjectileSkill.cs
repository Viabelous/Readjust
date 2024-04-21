using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ProjectileSkill : MonoBehaviour // peluru
{

    [SerializeField]
    private string attackAnimation, endAnimation;

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
            animator.Play(endAnimation);
        }
    }

    private void OnAnimationAttack()
    {
        if (isMoving)
        {
            animator.Play(attackAnimation);
        }
    }

    private void OnAnimationEnd()
    {
        Destroy(gameObject);
    }
}
