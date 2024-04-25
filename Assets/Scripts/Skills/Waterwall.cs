using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Waterwall : Skill
{
    [Header("Crowd Control")]
    [SerializeField]
    private float slow;
    private string ccId;




    public override void Activate(GameObject gameObject)
    {
        // gameObject.transform.position = GameObject.Find("Player").transform.position;

        // // Active();
    }

    // public override void HitEnemyFirstTime(GameObject gameObject, Collider2D other)
    // {
    // }

    public override void HitEnemy(GameObject gameObject, Collider2D other)
    {

        base.HitEnemy(gameObject, other);

        if (other.CompareTag("Enemy"))
        {
            if (HasHitEnemy(other))
            {
                return;
            }

            CrowdControlSystem mob = other.GetComponent<CrowdControlSystem>();
            CCSlow cc = new CCSlow(
                    slow,
                    timer,
                    other.GetComponent<MobController>().speed
                );
            ccId = cc.id;
            Debug.Log(ccId + " - " + cc.id);
            mob.ActivateCC(
                cc
            );
        }
    }

    public override void AfterHitEnemy(GameObject gameObject, Collider2D other)
    {
        base.AfterHitEnemy(gameObject, other);

        if (other.CompareTag("Enemy"))
        {
            CrowdControlSystem mob = other.GetComponent<CrowdControlSystem>();
            mob.DactivateCC(ccId);
        }

    }


}
