using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    // movement ------------------------------------------------------
    public float speed = 5f;
    public float hp, mana, shield;

    public GameObject healtBar, manaBar, shieldBar;

    public float minX, maxX, minY, maxY;
    public SpriteRenderer[] spriteRenderers;
    public Rigidbody2D rb;

    public Animator animate;

    Vector2 movement;

    public string direction;

    public bool canMove = true;

    // attack -------------------------------------------------

    public List<GameObject> skillPrefs = new List<GameObject>();

    void Start()
    {
        direction = "front";
        // ObjectToSpawn = GameObject.Find("basic_stab");

        hp = GameManager.player.maxHp;
        mana = GameManager.player.maxMana;
        shield = GameManager.player.maxShield;

    }

    // Update is called once per frame
    void Update()
    {
        if (hp <= 0)
        {
            Die();
        }

        if (!canMove)
        {
            return;
        }
        // print(skillPrefs.Count);


        PlayerAttack();

        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        animate.SetFloat("Horizontal", movement.x);
        animate.SetFloat("Vertical", movement.y);
        animate.SetFloat("Speed", movement.sqrMagnitude);

        if (movement.x == 1)
        {
            animate.SetFloat("Face", 1);
            direction = "right";
        }
        else if (movement.x == -1)
        {
            animate.SetFloat("Face", 3);
            direction = "left";

        }
        if (movement.y == 1)
        {

            animate.SetFloat("Face", 2);
            direction = "back";

        }
        else if (movement.y == -1)
        {
            animate.SetFloat("Face", 0);
            direction = "front";

        }


    }

    void FixedUpdate()
    {
        // if (
        //     transform.position.x < maxX &&
        //     transform.position.x > minX &&
        //     transform.position.y < maxY &&
        //     transform.position.y > minY
        // )
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);

        Vector3 position = rb.position;
        position.x = Mathf.Clamp(position.x, minX, maxX);
        position.y = Mathf.Clamp(position.y, minY, maxY);

        transform.position = position;

        // Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y, -10);
        //Camera.main.transform.Translate(movement.x * speed * Time.fixedDeltaTime, movement.y * speed * Time.fixedDeltaTime, 0);
    }


    void PlayerAttack()
    {
        switch (Input.inputString)
        {
            case "1":
            case "2":
            case "3":
            case "4":
            case "5":
            case "6":
            case "7":
                int index = int.Parse(Input.inputString) - 1;

                if (mana > 0 && index < GameManager.selectedSkills.Count)
                {
                    Skill skill = GameManager.selectedSkills[index];

                    if (skill != null && !skill.isCooldown && mana >= skill.manaUsage)
                    {
                        GameObject prefab = skillPrefs.Find(prefab => prefab.name == skill.name);
                        Instantiate(prefab, transform.position, Quaternion.identity);
                        skill.isCooldown = true;

                        mana -= skill.manaUsage;
                        UpdateManaBar();
                    }
                }
                break;


            case "q":
                animate.SetTrigger("BasicAttack");
                Instantiate(skillPrefs[0], gameObject.transform);

                break;

            case "=":
                SceneManager.LoadScene("MainMenu");
                break;

        }
    }


    public void UpdateHealthBar()
    {
        healtBar.GetComponent<Image>().fillAmount = hp / GameManager.player.maxHp;
    }
    public void UpdateManaBar()
    {
        manaBar.GetComponent<Image>().fillAmount = mana / GameManager.player.maxMana;
    }
    public void UpdateShieldBar()
    {
        shieldBar.GetComponent<Image>().fillAmount = shield / GameManager.player.maxShield;
    }

    private void Die()
    {
        StageManager.Instance.ChangeGameState(GameState.Lose);

    }
}
