using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEditor;

public class SkillController : MonoBehaviour
{
    [SerializeField]
    public Skill skillTemplate;

    [HideInInspector]
    public Skill skill;




    private void Start()
    {
        skill = skillTemplate.Clone();
        // sesuaikan damage skill dengan stage
        if (
            skill.Element == Element.Fire &&
            (
                skill.Type == SkillType.BurstDamage ||
                skill.Type == SkillType.CrowdControl ||
                skill.Type == SkillType.Debuff
            )
        )
        {
            skill.Damage += skill.Damage * 0.1f;
        }
        // print("Skill Damage + Fire: " + skill.Damage);
        skill.Activate(gameObject);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {

            if (transform.position.y > other.transform.position.y)
            {
                foreach (SpriteRenderer spriteRenderer in other.GetComponent<PlayerController>().spriteRenderers)
                {
                    spriteRenderer.sortingLayerName = "Player Front";
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
            other.GetComponent<MobController>().onSkillTrigger = false;
        }
    }
    private void OnAnimationEnd()
    {

        Destroy(gameObject);
    }


}