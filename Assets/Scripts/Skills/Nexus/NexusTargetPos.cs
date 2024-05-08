using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NexusTargetPos : MonoBehaviour
{
    enum NexusComponent
    {
        Effect,
        Voodoo
    }

    private Skill skill;
    [SerializeField] private NexusComponent component;

    private void Start()
    {
        skill = transform.parent.GetComponent<SkillController>().skill;
        SetPosition();

    }

    private void Update()
    {
        SetPosition();
    }

    private void SetPosition()
    {
        if (skill.LockedEnemy == null)
        {
            Destroy(gameObject);
            return;
        }

        Transform target = skill.LockedEnemy;
        SpriteRenderer targetSpriteRenderer = target.GetComponent<SpriteRenderer>();

        switch (component)
        {
            case NexusComponent.Effect:
                transform.position = (Vector2)target.transform.position - Vector2.up * targetSpriteRenderer.sprite.bounds.size.y * 0.5f;

                break;
            case NexusComponent.Voodoo:
                transform.position = (Vector2)target.transform.position + Vector2.up * targetSpriteRenderer.sprite.bounds.size.y * 0.5f;
                break;
        }

    }

}