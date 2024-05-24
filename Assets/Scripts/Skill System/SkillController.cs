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
        skill.Activate(gameObject);
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
            MobController mobController = other.GetComponent<MobController>();
            if (skill.Element != Element.Air && mobController.enemy.type == EnemyType.Flying)
            {
                return;
            }

            if (mobController.enemy.type == EnemyType.Ground)
            {
                if (transform.position.y > other.transform.position.y)
                {
                    mobController.spriteRenderer.sortingLayerName = "Chr Back";
                }
                else
                {
                    mobController.spriteRenderer.sortingLayerName = "Chr Front";
                }

                mobController.onSkillTrigger = true;
            }

        }

        skill.WhileHitEnemy(other);

        // if (other.CompareTag("Object"))
        // {
        //     // kalau skill ada di atas object
        //     if (transform.position.y > other.transform.position.y)
        //     {
        //         if (GetComponent<SpriteRenderer>() != null)
        //         {
        //             GetComponent<SpriteRenderer>().sortingOrder = -1;
        //         }
        //     }
        //     // kalau skill ada di bawah object
        //     else
        //     {
        //         if (GetComponent<SpriteRenderer>() != null)
        //         {
        //             GetComponent<SpriteRenderer>().sortingOrder = 20;
        //         }
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