using UnityEngine;

public class WindSlashBehaviour : MonoBehaviour
{
    // private Animator animator;
    private ChrDirection direction;

    [SerializeField] private GameObject leftRightCol, frontCol, backCol;
    [SerializeField] private Animator animator;

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
        // leftRightCol.SetActive(true);
        // frontCol.SetActive(false);
        // backCol.SetActive(false);
        animator.Play("wind_slash_left_right");

    }

    private void OnFrontAnimation()
    {
        // leftRightCol.SetActive(false);
        // frontCol.SetActive(true);
        // backCol.SetActive(false);
        animator.Play("wind_slash_front");
    }

    private void OnBackAnimation()
    {
        // leftRightCol.SetActive(false);
        // frontCol.SetActive(false);
        // backCol.SetActive(true);
        animator.Play("wind_slash_back");
    }
}