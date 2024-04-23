using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public enum SkillMovementType
{
    Area,
    Linear, // gerak lurus sesuai arah hadap
    Locking, // geraknya mengikuti pergerakan musuh yg dilock
    OnPlayer,
    OnCamera
}

public class SkillMovement : MonoBehaviour
{
    private GameObject player;

    [SerializeField] public SkillMovementType type;

    // [Header("Skip this if On Player or Camera")]

    [Header("Skill Position When Instantiate")]
    [SerializeField] private Vector3 offsetRight;
    [SerializeField] private Vector3 offsetLeft;
    [SerializeField] private Vector3 offsetFront;
    [SerializeField] private Vector3 offsetBack;

    [SerializeField] private float rotationRight, rotationLeft, rotationFront, rotationBack;
    [SerializeField] private bool flipRight, flipLeft, flipFront, flipBack;

    [SerializeField] private SpriteRenderer spriteRenderer;

    // [Header("Movement (Linear)")]
    // [SerializeField] private float range;

    private ChrDirection direction;
    private SkillAnimation skillAnimation;
    private bool isInstantiate;
    private Vector3 initialPosition;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        direction = player.GetComponent<PlayerController>().direction;
        skillAnimation = GetComponent<SkillAnimation>();

        isInstantiate = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isInstantiate)
        {
            SetPosition();

            initialPosition = transform.position;
            isInstantiate = false;
        }

        if (skillAnimation != null && skillAnimation.skill.hitType != SkillHitType.Temporary && !skillAnimation.isAttacking)
        {
            return;
        }

        switch (type)
        {
            case SkillMovementType.Linear:
                Skill skill = GetComponent<SkillController>().skill;
                switch (direction)
                {
                    // kanan
                    case ChrDirection.Right:
                        transform.position += Vector3.right * skill.movementSpeed * Time.deltaTime;
                        break;
                    // kiri
                    case ChrDirection.Left:
                        transform.position += Vector3.left * skill.movementSpeed * Time.deltaTime;
                        break;
                    // depan
                    case ChrDirection.Front:
                        transform.position += Vector3.down * skill.movementSpeed * Time.deltaTime;
                        break;
                    // belakang
                    case ChrDirection.Back:
                        transform.position += Vector3.up * skill.movementSpeed * Time.deltaTime;
                        break;
                }

                if (Vector3.Distance(initialPosition, transform.position) > skill.movementRange)
                {
                    Destroy(gameObject);
                }

                break;

            case SkillMovementType.OnPlayer:
                transform.position = player.transform.position;
                break;

            case SkillMovementType.OnCamera:
                transform.position = Camera.main.transform.position + new Vector3(0, 0, 10);
                break;
        }

    }

    private void SetPosition()
    {
        switch (type)
        {
            case SkillMovementType.Linear:
            case SkillMovementType.Area:
            case SkillMovementType.OnPlayer:
                switch (direction)
                {
                    case ChrDirection.Right:
                        if (flipRight)
                        {
                            FlipHorizontal();
                        }
                        transform.position = player.transform.position + offsetRight;
                        transform.rotation = transform.rotation * Quaternion.Euler(0, 0, rotationRight);

                        break;

                    case ChrDirection.Left:
                        if (flipLeft)
                        {
                            FlipHorizontal();
                        }
                        transform.position = player.transform.position + offsetLeft;
                        transform.rotation = transform.rotation * Quaternion.Euler(0, 0, rotationLeft);

                        break;

                    case ChrDirection.Front:
                        if (flipFront)
                        {
                            FlipVertical();
                        }

                        if (
                            type == SkillMovementType.Linear ||
                            type == SkillMovementType.Area
                        )
                        {
                            spriteRenderer.sortingLayerName = "Skill Front";
                        }

                        transform.position = player.transform.position + offsetFront;
                        transform.rotation = transform.rotation * Quaternion.Euler(0, 0, rotationFront);

                        break;

                    case ChrDirection.Back:
                        if (flipBack)
                        {
                            FlipVertical();
                        }

                        if (
                            type == SkillMovementType.Linear ||
                            type == SkillMovementType.Area
                        )
                        {
                            spriteRenderer.sortingLayerName = "Skill Back";

                        }

                        transform.position = player.transform.position + offsetBack;
                        transform.rotation = transform.rotation * Quaternion.Euler(0, 0, rotationBack);

                        break;
                }

                break;

            case SkillMovementType.OnCamera:
                transform.position = Camera.main.transform.position + new Vector3(0, 0, 10);
                break;
        }
    }

    private void FlipHorizontal()
    {
        Vector3 newScale = transform.localScale;
        newScale.x *= -1;
        transform.localScale = newScale;
    }

    private void FlipVertical()
    {
        Vector3 newScale = transform.localScale;
        newScale.y *= -1; // Membalik skala vertical
        transform.localScale = newScale;
    }


}
