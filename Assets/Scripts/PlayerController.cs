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
    right, left,
    front, back
}

public class PlayerController : MonoBehaviour
{
    // movement ------------------------------------------------------

    public Text aerusText, expText;

    public float minX, maxX, minY, maxY;

    public SpriteRenderer[] spriteRenderers;
    public Rigidbody2D rb;

    public Animator animate;

    Vector2 movement;

    public Player player;

    [HideInInspector]
    public ChrDirection direction;

    [HideInInspector]
    public bool movementEnabled;

    private DefenseSystem defenseSystem;

    // attack -------------------------------------------------

    // public List<GameObject> skillPrefs = new List<GameObject>();

    void Start()
    {
        direction = ChrDirection.front;
        defenseSystem = GetComponent<DefenseSystem>();
        player = player.CloneObject();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.hp <= 0)
        {
            Die();
        }


        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        animate.SetFloat("Horizontal", movement.x);
        animate.SetFloat("Vertical", movement.y);
        animate.SetFloat("Speed", movement.sqrMagnitude);

        if (movement.x == 1)
        {
            animate.SetFloat("Face", 1);
            direction = ChrDirection.right;
        }
        else if (movement.x == -1)
        {
            animate.SetFloat("Face", 3);
            direction = ChrDirection.left;

        }
        if (movement.y == 1)
        {

            animate.SetFloat("Face", 2);
            direction = ChrDirection.back;

        }
        else if (movement.y == -1)
        {
            animate.SetFloat("Face", 0);
            direction = ChrDirection.front;

        }

        // interaksi dengan keyboard
        switch (Input.inputString)
        {
            case "q":
                animate.SetTrigger("BasicAttack");

                GameObject prefab = SkillHolder.Instance.skillPrefs[0];
                Instantiate(prefab);
                break;

            case "=":
                SceneManager.LoadScene("MainMenu");
                break;

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

    public void UseSkill(Skill skill)
    {
        // atur kalo ada pengurangan penggunaan mana dari item kah apa
        switch (skill.costType)
        {
            case SkillCost.mana:
                player.mana -= skill.cost;
                break;
            case SkillCost.hp:
                player.hp -= skill.cost;
                break;

        }
    }

    public void AddShield(float shield)
    {
        player.shield += shield;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Damaged();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Undamaged();
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
        StageManager.instance.ChangeGameState(GameState.lose);
    }
}


