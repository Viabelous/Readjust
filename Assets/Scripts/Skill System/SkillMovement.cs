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
    private Skill skill;

    [SerializeField] public SkillMovementType type;

    // [Header("Skip this if On Player or Camera")]

    [Header("Skill Position When Instantiate")]
    // [SerializeField]
    // private bool rotateWithPlayer;
    [SerializeField] private Vector3 offsetRight;
    [SerializeField] private Vector3 offsetLeft;
    [SerializeField] private Vector3 offsetFront;
    [SerializeField] private Vector3 offsetBack;

    [SerializeField] private float rotationRight, rotationLeft, rotationFront, rotationBack;
    [SerializeField] private bool flipRight, flipLeft, flipFront, flipBack;

    [Header("Area & Linear Only")]
    [SerializeField] private SpriteRenderer spriteRenderer;

    [Header("Linear Only")]
    [SerializeField] private bool oppositeDirection = false;
    [Header("Locking Only")]
    [SerializeField] private Vector3 offsetPivot;

    // [Header("Movement (Linear)")]
    // [SerializeField] private float range;

    private ChrDirection direction;
    private SkillAnimation skillAnimation;
    // private bool isInstantiate;
    private Vector3 initialPosition;



    void Start()
    {
        player = GameObject.FindWithTag("Player");
        direction = player.GetComponent<PlayerController>().direction;
        skillAnimation = GetComponent<SkillAnimation>();
        skill = GetComponent<SkillController>().skill;

        // isInstantiate = true;

        SetPosition();
        initialPosition = transform.position;
        print("initial pos:" + transform.position);

    }

    // Update is called once per frame
    void Update()
    {


        if (
            skillAnimation != null &&
            skillAnimation.skill.HitType == SkillHitType.Once &&
            !skillAnimation.isAttacking
        )
        {
            return;
        }

        switch (type)
        {
            case SkillMovementType.Linear:
                LinearMovement();

                break;

            case SkillMovementType.OnPlayer:
                OnPlayerMovement();

                // transform.position = player.transform.position;
                break;

            case SkillMovementType.OnCamera:
                OnCameraMovement();
                break;

            case SkillMovementType.Locking:
                LockingMovement();
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
            case SkillMovementType.Locking:

                switch (direction)
                {
                    case ChrDirection.Right:
                        transform.position = player.transform.position + offsetRight;

                        if (flipRight)
                        {
                            FlipHorizontal();
                        }
                        transform.rotation = transform.rotation * Quaternion.Euler(0, 0, rotationRight);

                        break;

                    case ChrDirection.Left:
                        transform.position = player.transform.position + offsetLeft;
                        if (flipLeft)
                        {
                            FlipHorizontal();
                        }
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

    private void LinearMovement()
    {
        switch (direction)
        {
            // kanan
            case ChrDirection.Right:
                transform.position += Vector3.right * skill.MovementSpeed * Time.deltaTime * (oppositeDirection ? -1 : 1);
                break;
            // kiri
            case ChrDirection.Left:
                transform.position += Vector3.left * skill.MovementSpeed * Time.deltaTime * (oppositeDirection ? -1 : 1);
                break;
            // depan
            case ChrDirection.Front:
                transform.position += Vector3.down * skill.MovementSpeed * Time.deltaTime * (oppositeDirection ? -1 : 1);
                break;
            // belakang
            case ChrDirection.Back:
                transform.position += Vector3.up * skill.MovementSpeed * Time.deltaTime * (oppositeDirection ? -1 : 1);
                break;
        }
        float distance = Vector3.Distance(initialPosition, transform.position);
        // print(distance);
        if (distance > skill.MovementRange)
        {
            Destroy(gameObject);
        }
    }

    private void OnPlayerMovement()
    {
        direction = player.GetComponent<PlayerController>().direction;
        switch (direction)
        {
            // kanan
            case ChrDirection.Right:
                transform.position = player.transform.position + offsetRight;
                break;
            // kiri
            case ChrDirection.Left:
                transform.position = player.transform.position + offsetLeft;
                break;
            // depan
            case ChrDirection.Front:
                transform.position = player.transform.position + offsetFront;
                break;
            // belakang
            case ChrDirection.Back:
                transform.position = player.transform.position + offsetBack;
                break;
        }
    }

    private void OnCameraMovement()
    {
        transform.position = Camera.main.transform.position + new Vector3(0, 0, 10);
    }

    private void LockingMovement()
    {
        if (skill.LockedEnemy == null)
        {
            print("masuk sini di locking");
            return;
        }
        print("now pos1:" + transform.position);

        transform.position = Vector3.MoveTowards(transform.position, skill.LockedEnemy.position, skill.MovementSpeed * 0.01f);

        // print("musuh: " + skill.LockedEnemy.position);
        // rotasikan arah hadap skill --------------------------------

        // Menghitung arah vektor dari titik pivot ke targetObject
        Vector2 directionToTarget = skill.LockedEnemy.position - (transform.position + offsetPivot);

        // Menghitung rotasi untuk menghadap ke arah targetObject (dalam 2D, hanya rotasi pada sumbu Z)
        float angle = Mathf.Atan2(directionToTarget.y, directionToTarget.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);

        // Terapkan rotasi pada titik pivot
        transform.rotation = targetRotation;
        print("now pos2:" + transform.position);

        // if (transform.position == skill.LockedEnemy.position)
        // {
        //     // print("Hilangkan skill");
        //     Destroy(gameObject);
        // }

    }


}
