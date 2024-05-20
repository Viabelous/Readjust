using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindSlashBehaviour : MonoBehaviour
{
    // private Animator animator;
    private ChrDirection direction;

    [SerializeField] private GameObject leftRightCol, frontCol, backCol;

    void Start()
    {
        // animator = GetComponent<Animator>();

        // skill.Damage += dmgPersenOfAtk * GameObject.Find("Player").GetComponent<PlayerController>().player.atk;

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
        // animator.Play("wind_slash_left_right");
        leftRightCol.SetActive(true);
        frontCol.SetActive(false);
        backCol.SetActive(false);

    }

    private void OnFrontAnimation()
    {
        // animator.Play("wind_slash_front");
        leftRightCol.SetActive(false);
        frontCol.SetActive(true);
        backCol.SetActive(false);
    }

    private void OnBackAnimation()
    {
        // animator.Play("wind_slash_back");
        leftRightCol.SetActive(false);
        frontCol.SetActive(false);
        backCol.SetActive(true);
    }
}