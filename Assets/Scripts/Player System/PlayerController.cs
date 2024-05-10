using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum ChrDirection
{
    Right, Left,
    Front, Back
}

public class PlayerController : MonoBehaviour
{
    public GameState state;

    // movement ------------------------------------------------------


    [SerializeField] private GameObject basicStab;
    [SerializeField] private Text aerusText, expText;

    [SerializeField] private float minX, maxX, minY, maxY;

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
        // print("ATK di Start: " + player.atk);

        movementEnabled = true;
        nearInteractable = false;
    }

    // Update is called once per frame
    void Update()
    {

        print("ATK: " + player.atk);
        print("DEF: " + player.def);
        print("SHIELD: " + player.shield);
        print("AGI: " + player.agi);
        print("FOC: " + player.foc);

        if (player.hp <= 0)
        {
            Die();
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

            switch (state)
            {
                case GameState.OnStage:
                    // interaksi dengan keyboard
                    switch (Input.inputString)
                    {
                        case "q":
                            animate.SetTrigger("BasicAttack");

                            if (!GameObject.Find(basicStab.name + "(Clone)"))
                            {
                                Instantiate(basicStab);
                            }

                            break;

                        case "=":
                            SceneManager.LoadScene("MainMenu");
                            break;

                    }
                    break;

                case GameState.OnDeveloperZone:
                    break;
            }

        }
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * player.movementSpeed * Time.fixedDeltaTime);

        Vector3 position = rb.position;
        position.x = Mathf.Clamp(position.x, minX, maxX);
        position.y = Mathf.Clamp(position.y, minY, maxY);

        transform.position = position;
    }

    public void Pay(CostType costType, float value)
    {
        switch (costType)
        {
            case CostType.Mana:
                player.mana -= value;
                break;
            case CostType.Hp:
                player.hp -= value;
                break;
            case CostType.Aerus:
                player.aerus -= value;
                break;
            case CostType.Exp:
                player.exp -= value;
                break;
        }
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

    public void CollectAerus(float num)
    {
        player.aerus += num;
        aerusText.text = player.aerus.ToString();
    }

    public void CollectExp(float num)
    {
        player.exp += num;
        expText.text = player.exp.ToString();
    }

    private void Die()
    {
        Damaged();
        // StageManager.instance.ChangeGameState(GameState.Lose);
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


