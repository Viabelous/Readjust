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

    // [Header("Movement (Linear)")]
    // [SerializeField] private float range;

    private ChrDirection direction;
    private SkillAnimation skillAnimation;
    private bool isInstantiate;
    private Vector3 initialPosition;

    // locking
    // private Vector3 initialPosTarget;
    private float initialDistance;
    // private List<MobController> lockedTarget;


    void Start()
    {
        player = GameObject.FindWithTag("Player");
        direction = player.GetComponent<PlayerController>().direction;
        skillAnimation = GetComponent<SkillAnimation>();
        skill = GetComponent<SkillController>().skill;

        isInstantiate = true;

        initialPosition = transform.position;

        if (type == SkillMovementType.Locking)
        {
            if (skill.LockedEnemy == null)
            {
                Destroy(gameObject);
                return;
            }

            initialDistance = Vector3.Distance(initialPosition, skill.LockedEnemy.transform.position);
        }

        SetPosition();
    }

    // Update is called once per frame
    void Update()
    {

        if (isInstantiate)
        {
            isInstantiate = false;
        }

        if (skillAnimation != null && skillAnimation.skill.HitType != SkillHitType.Temporary && !skillAnimation.isAttacking)
        {
            return;
        }

        switch (type)
        {
            case SkillMovementType.Linear:
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

                if (Vector3.Distance(initialPosition, transform.position) > skill.MovementRange)
                {
                    Destroy(gameObject);
                }

                break;

            case SkillMovementType.OnPlayer:
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

                // transform.position = player.transform.position;
                break;

            case SkillMovementType.OnCamera:
                transform.position = Camera.main.transform.position + new Vector3(0, 0, 10);
                break;

            case SkillMovementType.Locking:

                if (Vector3.Distance(transform.position, skill.LockedEnemy.position) <= 0)
                {
                    Destroy(gameObject);
                    return;
                }

                float distCovered = (Time.time - 0) * skill.MovementSpeed;
                float fracJourney = distCovered / initialDistance;

                // Menghitung posisi objek berdasarkan kurva Bézier (melengkung)
                Vector3 curvePosition = Vector2.Lerp(transform.position, skill.LockedEnemy.position, fracJourney);
                curvePosition.y += Mathf.Sin(fracJourney * Mathf.PI) * 2f; // Menambahkan ketinggian kurva (opsional)

                // Pindahkan objek ke posisi kurva
                transform.position = curvePosition;

                // Putar objek agar menghadap ke arah tujuan

                // Menghitung arah ke titik tujuan
                Vector3 directionToTarget = skill.LockedEnemy.position - transform.position;
                directionToTarget.y = 0f; // Memastikan pergerakan hanya pada bidang horizontal

                // Menghitung rotasi objek agar menghadap ke arah tujuan
                Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);

                // Mengabaikan rotasi pada sumbu X dan Z
                targetRotation.x = 0f;
                targetRotation.z = 0f;

                // Menerapkan rotasi secara smooth pada sumbu Y saja
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime);


                // Quaternion targetRotation = Quaternion.LookRotation(skill.LockedEnemy.position - transform.position);
                // transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5f);

                // Vector3 targetPos = skill.LockedEnemy.transform.position;
                // float curveHeight = 2f;

                // // Hitung waktu perjalanan (0 sampai 1) berdasarkan pergerakan objek
                // float distCovered = Vector3.Distance(initialPosition, transform.position);
                // if (distCovered <= 0)
                // {
                //     Destroy(gameObject);
                //     return;
                // }

                // float fractionOfJourney = distCovered / initialDistance;

                // // Hitung kurva (lengkungan) berdasarkan ketinggian dan waktu perjalanan
                // float curveValue = Mathf.Sin(fractionOfJourney * Mathf.PI) * curveHeight;

                // // Hitung posisi objek berdasarkan kurva (lengkungan)
                // Vector3 curvePosition = Vector3.Lerp(initialPosition, targetPos, fractionOfJourney);
                // curvePosition.y += curveValue; // Terapkan ketinggian kurva

                // // Pindahkan objek ke posisi kurva
                // transform.position = curvePosition;

                // // Putar objek agar menghadap ke arah tujuan
                // Quaternion targetRotation = Quaternion.LookRotation(targetPos - transform.position);
                // transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * skill.MovementSpeed);
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

    // private void CurvedMovement(Vector3 targetPos)
    // {

    // }

    // private void GetLockedEnemies()
    // {
    //     GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

    //     // Inisialisasi variabel untuk menyimpan jarak terdekat dan musuh terdekat
    //     float closestDistance = Mathf.Infinity;

    //     // Looping untuk mencari musuh terdekat
    //     foreach (GameObject enemy in enemies)
    //     {
    //         // Menghitung jarak antara objek ini dengan musuh dalam loop
    //         float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);

    //         // Memeriksa apakah musuh saat ini memiliki jarak lebih dekat
    //         if (distanceToEnemy < closestDistance)
    //         {
    //             closestDistance = distanceToEnemy;
    //             lockedTarget = enemy;
    //         }
    //     }
    // }

}
