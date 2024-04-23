using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Whirlwind : Skill
{
    [SerializeField]
    private float slideSpeed, slideDistance;

    private GameObject player;
    private ChrDirection direction;
    // private SpriteRenderer spriteRenderer;
    private Transform transform;

    public override void Activate(GameObject gameObject)
    {
    }


    public override void HitEnemy(GameObject gameObject, Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {

            // gameObject.GetComponent<AreaEffector2D>().forceMagnitude = 300;
            CrowdControlSystem mob = other.GetComponent<CrowdControlSystem>();

            mob.ActivateSliding(slideSpeed, slideDistance);

            switch (direction)
            {
                case ChrDirection.Right:
                    mob.slideDirection = transform.right;
                    break;
                case ChrDirection.Left:
                    mob.slideDirection = -transform.right;
                    break;

                case ChrDirection.Front:
                    // cuma bisa untuk mob yang ada di bawah player
                    if (mob.transform.position.y < player.transform.position.y)
                    {
                        mob.slideDirection = -transform.up;
                    }
                    break;

                case ChrDirection.Back:
                    // cuma bisa untuk mob yang ada di atas player
                    if (mob.transform.position.y > player.transform.position.y)
                    {
                        mob.slideDirection = transform.up;
                    }
                    break;
            }

        }

    }
    public override void AfterHitEnemy(GameObject gameObject, Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            CrowdControlSystem mob = other.GetComponent<CrowdControlSystem>();
            mob.DeactivateSliding();
        }
    }

}
