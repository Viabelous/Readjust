using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public enum ChrDirection
{
    Right, Left,
    Front, Back
}

public class PlayerController : MonoBehaviour
{
    public GameState gameState;

    // [SerializeField] private Text aerusText, expText;

    [SerializeField] private Vector2 minMap, maxMap;

    public SpriteRenderer[] spriteRenderers;

    [SerializeField] private Rigidbody2D rb;

    [SerializeField] private Animator animate;

    private Vector2 movement;

    public Player player;

    [HideInInspector] public ChrDirection direction;

    [HideInInspector] public bool movementEnabled;

    [HideInInspector] public bool nearInteractable;

    // attack -------------------------------------------------

    // public List<GameObject> skillPrefs = new List<GameObject>();

    void Start()
    {
        direction = ChrDirection.Front;
        player = player.Clone();

        movementEnabled = true;
        nearInteractable = false;

    }

    // Update is called once per frame
    void Update()
    {
        switch (gameState)
        {
            case GameState.OnStage:
                minMap = StageManager.instance.minMap;
                maxMap = StageManager.instance.maxMap;
                break;
            case GameState.OnDeveloperZone:
                minMap = ZoneManager.instance.minMap;
                maxMap = ZoneManager.instance.maxMap;
                break;
        }

        // print("MaxHP: " + player.maxHp);
        // print("MaxMana: " + player.maxMana);
        // print("HP: " + player.hp);
        // print("MANA: " + player.mana);
        // print("ATK: " + player.atk);
        // print("DEF: " + player.def);
        // print("SHIELD: " + player.shield);
        // print("AGI: " + player.agi);
        // print("FOC: " + player.foc);
        // print("Aerus: " + player.aerus);
        // print("Exp: " + player.exp);
        // print(Time.time);

        switch (gameState)
        {
            case GameState.OnStage:
                DoIfOnlyOnStage();
                break;
        }

        if (movementEnabled)
        {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");

            animate.SetFloat("Horizontal", movement.x);
            animate.SetFloat("Vertical", movement.y);
            animate.SetFloat("Speed", movement.sqrMagnitude);

            if (movement.x == 1)
            {
                animate.SetFloat("Face", 1);
                direction = ChrDirection.Right;
            }
            else if (movement.x == -1)
            {
                animate.SetFloat("Face", 3);
                direction = ChrDirection.Left;

            }
            if (movement.y == 1)
            {

                animate.SetFloat("Face", 2);
                direction = ChrDirection.Back;

            }
            else if (movement.y == -1)
            {
                animate.SetFloat("Face", 0);
                direction = ChrDirection.Front;

            }

        }
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * player.MovementSpeed * Time.fixedDeltaTime);

        Vector3 position = rb.position;
        position.x = Mathf.Clamp(position.x, minMap.x, maxMap.x);
        position.y = Mathf.Clamp(position.y, minMap.y, maxMap.y);

        transform.position = position;
    }

    public void Damaged()
    {
        for (int i = 0; i < spriteRenderers.Length; i++)
        {
            spriteRenderers[i].color = Color.red;
        }
    }

    public void Undamaged()
    {
        for (int i = 0; i < spriteRenderers.Length; i++)
        {
            spriteRenderers[i].color = Color.white;
        }
    }

    // public void PayWithAerus(float num)
    // {
    //     player.Pay(CostType.Aerus, num);
    //     aerusText.text = player.aerus.ToString();
    // }
    // public void PayWithExp(float num)
    // {
    //     player.Pay(CostType.Exp, num);
    //     expText.text = player.exp.ToString();
    // }

    // public void CollectAerus(float num)
    // {
    //     print("Tambah aerus");
    //     player.Collect(RewardType.Aerus, num);
    //     aerusText.text = player.aerus.ToString();
    // }

    // public void CollectExp(float num)
    // {
    //     player.Collect(RewardType.ExpOrb, num);
    //     expText.text = player.exp.ToString();
    // }

    private void DoIfOnlyOnStage()
    {

        if (StageManager.instance.CurrentState() != StageState.Lose && player.hp <= 0)
        {
            Die();
        }

        movementEnable(StageManager.instance.CurrentState() == StageState.Play ? true : false);

    }

    private void Die()
    {
        Damaged();
        StageManager.instance.ToggleState(StageState.Play, StageState.Lose);
    }

    public void movementEnable(bool state)
    {
        movementEnabled = state;
    }

    public void interactableNearby(bool state)
    {
        nearInteractable = state;
    }
}


