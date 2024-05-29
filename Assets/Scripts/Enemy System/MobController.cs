using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public enum CharacterState
{
    Alive,
    Dead
}

public class MobController : MonoBehaviour
{

    public Enemy enemy;
    [HideInInspector]
    public bool movementEnabled = true;

    [HideInInspector]
    public float speed;

    private CrowdControlSystem crowdControlSystem;
    private CharacterState state;

    private GameObject player;
    private Vector2 movement;
    private Vector3 initialScale;
    [HideInInspector] public Animator animate;
    public SpriteRenderer spriteRenderer;
    private PlayerController playerController;
    private bool gainSpeed = false, isBoss, randomMovement;
    private Vector3 targetPos;


    [HideInInspector] public bool onSkillTrigger = false; // tanda apakah sedang berada di dalam collider skill

    // private bool thorned;


    void Start()
    {
        animate = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        crowdControlSystem = GetComponent<CrowdControlSystem>();

        player = GameObject.Find("Player");
        playerController = player.GetComponent<PlayerController>();

        initialScale = transform.localScale;

        enemy = enemy.Clone();
        speed = enemy.MovementSpeed;

        state = CharacterState.Alive;
        isBoss = GetComponent<BossController>() != null;

        movementEnabled = true;
        targetPos = player.transform.position;
        randomMovement = false;
    }

    void Update()
    {
        switch (state)
        {
            case CharacterState.Alive:
                if (!isBoss && !gainSpeed && StageManager.instance.time >= 10 * 60)
                {
                    speed += 0.5f;
                    gainSpeed = true;
                }

                if (enemy.hp <= 0)
                {
                    state = CharacterState.Dead;
                    playerController.player.Collect(RewardType.Aerus, enemy.GetAerus());
                    playerController.player.Collect(RewardType.ExpOrb, enemy.GetExp());
                }

                targetPos = randomMovement ? targetPos : player.transform.position;
                if (targetPos.x != transform.position.x)
                {
                    movement.x = targetPos.x < transform.position.x ? -1 : 1;
                }
                else
                {
                    movement.x = 0;
                }

                if (targetPos.y != transform.position.y)
                {
                    movement.y = targetPos.y < transform.position.y ? -1 : 1;
                }
                else
                {
                    movement.y = 0;
                }

                animate.SetFloat("Vertical", movement.y);
                animate.SetFloat("Speed", movement.sqrMagnitude);

                break;

            case CharacterState.Dead:
                Damaged();
                if (isBoss)
                {
                    Destroy(gameObject);
                    StageManager.instance.ChangeCurrentState(StageState.Win);
                }

                Destroy(gameObject, 0.1f);
                break;
        }

    }

    // pergerakan mob ------------------------------------------------------------------------
    void FixedUpdate()
    {
        if (state == CharacterState.Dead)
        {
            return;
        }

        if (movementEnabled)
        {
            if (
                !crowdControlSystem.CheckCC(CrowdControlType.Slide) &&
                !crowdControlSystem.CheckCC(CrowdControlType.KnockBack)
            )
            {
                Vector3 direction = (targetPos - transform.position).normalized;
                transform.Translate(direction * speed * Time.deltaTime);
            }

            if (movement.x != 0 && movement.y != 0)
            {
                // gameObject.transform.localScale = new Vector3((movement.x > 0.5) ? 1 : -1, 1, 1);
                gameObject.transform.localScale = (movement.x > 0.5) ? initialScale : new Vector3(-initialScale.x, initialScale.y, initialScale.z);
            }

        }


    }


    public void Damaged()
    {
        spriteRenderer.color = Color.red;

    }

    public void Undamaged()
    {
        spriteRenderer.color = Color.white;
    }

    public void Effected(string effect)
    {
        switch (effect)
        {
            case "breezewheel":
            case "thorn":
            case "nexus":
                StartCoroutine(EffectedCoroutine());
                break;
        }
    }

    IEnumerator EffectedCoroutine()
    {
        Damaged();
        yield return new WaitForSeconds(0.2f);
        Undamaged();
    }

    public void SetTargetPos(Vector3 targetPos)
    {
        this.targetPos = targetPos;
    }

    public void SetRandomMovement(bool value)
    {
        this.randomMovement = value;
    }

}
