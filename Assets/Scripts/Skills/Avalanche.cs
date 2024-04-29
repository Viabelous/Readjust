using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Avalanche : MonoBehaviour
{
    private Skill skill;
    private Animator animator;
    private ChrDirection direction;
    [SerializeField] private float dmgPersenOfAtk;
    // private GameObject objLeftRight, objFront, objBack;

    private BoxCollider2D boxCollider;

    private void Start()
    {
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        skill = GetComponent<SkillController>().skill;
        skill.Damage += dmgPersenOfAtk * GameObject.Find("Player").GetComponent<PlayerController>().player.atk;

        direction = GameObject.Find("Player").GetComponent<PlayerController>().direction;

        switch (direction)
        {
            case ChrDirection.Right:
            case ChrDirection.Left:
                OnLeftRightAnimation();
                break;
            case ChrDirection.Front:
                OnFrontAnimation();
                break;
            case ChrDirection.Back:
                OnBackAnimation();
                break;
        }
    }

    private void OnLeftRightAnimation()
    {
        animator.Play("avalanche_left_right");
        boxCollider.offset = new Vector2(0.3f, -0.36f);
        boxCollider.size = new Vector2(3.9f, 1.55f);

        // objFront.SetActive(false);
        // objBack.SetActive(false);
        // objLeftRight.SetActive(true);
    }

    private void OnFrontAnimation()
    {
        animator.Play("avalanche_front");
        boxCollider.offset = new Vector2(0.2f, 0.26f);
        boxCollider.size = new Vector2(0.1f, 3f);
        // objFront.SetActive(true);
        // objBack.SetActive(false);
        // objLeftRight.SetActive(false);

    }
    private void OnBackAnimation()
    {
        animator.Play("avalanche_back");
        boxCollider.offset = new Vector2(0.075f, 1.23f);
        boxCollider.size = new Vector2(0.85f, 3f);
        // objBack.SetActive(true);
        // objFront.SetActive(false);
        // objLeftRight.SetActive(false);
    }
}