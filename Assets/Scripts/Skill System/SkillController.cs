using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEditor;

public class SkillController : MonoBehaviour
{
    [SerializeField]
    public Skill skill;

    // [HideInInspector] public bool validAttack = true;

    // [SerializeField] private Collider2D groundCollider, flyingCollider;

    private void Awake()
    {
        skill = skill.Clone();
    }

    private void Start()
    {

        // sesuaikan damage skill dengan stage
        // if (
        //     skill.Element == Element.Fire &&
        //     (
        //         skill.Type == SkillType.BurstDamage ||
        //         skill.Type == SkillType.CrowdControl ||
        //         skill.Type == SkillType.Debuff
        //     )
        // )
        // {
        //     skill.Damage += skill.Damage * 0.1f;
        // }

        skill.Activate(gameObject);
        // print(skill.GetDamage(GameObject.Find("Player").GetComponent<PlayerController>().player));
    }

    private void Update()
    {
        skill.OnActivated(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (skill.Element != Element.Air && other.GetComponent<MobController>().enemy.type == EnemyType.Flying)
            {
                return;
            }
        }

        skill.HitEnemy(other);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (skill.Element != Element.Air && other.GetComponent<MobController>().enemy.type == EnemyType.Flying)
            {
                return;
            }
        }

        skill.WhileHitEnemy(other);

        if (other.CompareTag("Enemy"))
        {
            MobController mob = other.GetComponent<MobController>();

            // // jika musuh darat terkena collider darat ATAU
            // // jika musuh terbang terkena collider terbang
            // // maka berikan damage/cc dapat diberikan
            // if (
            //     groundCollider != null &&
            //     groundCollider.OverlapPoint(mob.transform.position) &&
            //     mob.enemy.type == EnemyType.Ground ||
            //     flyingCollider != null &&
            //     flyingCollider.OverlapPoint(mob.transform.position) &&
            //     mob.enemy.type == EnemyType.Flying
            // )
            // {
            //     validAttack = true;
            // }
            // else
            // {
            //     validAttack = false;
            // }
        }

        if (other.CompareTag("Player"))
        {
            if (skill.MovementType == SkillMovementType.Area)
            {
                // kalau skill di atas player
                if (transform.position.y > other.transform.position.y)
                {
                    foreach (SpriteRenderer spriteRenderer in other.GetComponent<PlayerController>().spriteRenderers)
                    {
                        spriteRenderer.sortingLayerName = "Player Front";
                    }
                }
                // kalau skill di bawah player
                else
                {
                    foreach (SpriteRenderer spriteRenderer in other.GetComponent<PlayerController>().spriteRenderers)
                    {
                        spriteRenderer.sortingLayerName = "Player";
                    }
                }
            }
            else
            {
                foreach (SpriteRenderer spriteRenderer in other.GetComponent<PlayerController>().spriteRenderers)
                {
                    spriteRenderer.sortingLayerName = "Player";
                }
            }

        }

        // if (other.CompareTag("Enemy"))
        // {
        //     other.GetComponent<MobController>().onSkillTrigger = true;
        //     if (transform.position.y + 1 > other.transform.position.y)
        //     {
        //         other.GetComponent<SpriteRenderer>().sortingLayerName = "Enemy Front";
        //     }
        //     else
        //     {
        //         other.GetComponent<SpriteRenderer>().sortingLayerName = "Enemy";
        //     }
        // }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            skill.AfterHitEnemy(other);
            other.GetComponent<MobController>().onSkillTrigger = false;
        }
    }
    private void OnAnimationEnd()
    {

        Destroy(gameObject);
    }


}