using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NexusTargetPos : MonoBehaviour
{
    enum NexusComponent
    {
        Effect,
        Voodoo,
        Link
    }

    private Skill skill;
    [SerializeField] private NexusComponent component;
    private PlayerController playerController;

    private void Start()
    {
        // SetPosition();

        skill = transform.parent.GetComponent<SkillController>().skill;
        playerController = StageManager.instance.player.GetComponent<PlayerController>();
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
                transform.position = playerController.transform.position;
                break;
            case NexusComponent.Voodoo:
                // transform.position = (Vector2)target.transform.position + Vector2.up * targetSpriteRenderer.sprite.bounds.size.y * 0.5f;
                transform.position = (Vector2)target.transform.position + Vector2.up * (-target.GetComponent<GroundEnemy>().GetPivotOffset() + 0.2f);
                break;
            case NexusComponent.Link:
                // transform.position = (Vector2)target.transform.position + Vector2.up * targetSpriteRenderer.sprite.bounds.size.y * 0.1f;
                transform.position = (Vector2)target.transform.position + Vector2.up * (-target.GetComponent<GroundEnemy>().GetPivotOffset() + 0.2f);
                break;
        }

    }

}