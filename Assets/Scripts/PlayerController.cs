using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    // movement ------------------------------------------------------
    public float speed = 5f;
    public float maxHp = 500, hp;
    public float maxMana = 100, mana;
    public GameObject healtBar, manaBar;

    public bool attacked = false;



    public SpriteRenderer[] spriteRenderers;
    public Rigidbody2D rb;

    public Animator animate;

    Vector2 movement;

    public string direction;

    public bool canMove = true;

    // attack -------------------------------------------------
    public GameObject ObjectToSpawn;

    public GameObject[] skillObjs;

    void Start()
    {
        direction = "front";
        ObjectToSpawn = GameObject.Find("basic_stab");
        hp = maxHp;
        mana = maxMana;

        // hubungkan gameobject skill dengan object skill
        for (int i = 0; i < TotalSelectedSkills(); i++)
        {
            string name = GameManager.playerNow.selectedSkills[i].name;
            GameManager.playerNow.selectedSkills[i].skillObj = skillObjs[SkillIndex(name)];
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (!canMove)
        {
            return;
        }


        PlayerAttack();

        PlayerDirection();


    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
        Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y, -10);
        //Camera.main.transform.Translate(movement.x * speed * Time.fixedDeltaTime, movement.y * speed * Time.fixedDeltaTime, 0);
    }

    // private void OnTriggerEnter2D(Collider2D other)
    // {
    //     if (other.CompareTag("Enemy") && hp >= 0)
    //     {

    //     }
    // }

    // private void OnCollisionEnter2D(Collision2D collision)
    // {
    //     // Cek apakah collider yang bertabrakan adalah child collider
    //     if (!IsChildCollider(collision.collider) && collision.gameObject.CompareTag("Enemy"))
    //     {
    //         MobController enemy = collision.gameObject.GetComponent<MobController>();
    //         hp -= enemy.attack;
    //         UpdateHp();
    //     }
    // }


    void PlayerDirection()
    {
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

    void PlayerAttack()
    {
        if (
            (Input.inputString == "1" ||
            Input.inputString == "2" ||
            Input.inputString == "3" ||
            Input.inputString == "4" ||
            Input.inputString == "5" ||
            Input.inputString == "6" ||
            Input.inputString == "7") && mana > 0
        )

        {
            int index = int.Parse(Input.inputString) - 1;
            Skill skill = GameManager.playerNow.selectedSkills[index];

            if (skill != null && !skill.isCooldown && mana >= skill.manaUsage)
            {
                skill.Attack();
                mana -= skill.manaUsage;
                UpdateManaBar();
            }
        }

        switch (Input.inputString)
        {
            case "p":
                animate.SetTrigger("BasicAttack");
                GameObject Spawn = Instantiate(ObjectToSpawn, GameObject.FindWithTag("Player").transform);
                Destroy(Spawn, 1);
                break;


            case "=":
                SceneManager.LoadScene("MainMenu");
                break;

        }
    }


    public void UpdateHealthBar()
    {
        healtBar.GetComponent<Image>().fillAmount = hp / maxHp;
    }
    public void UpdateManaBar()
    {
        manaBar.GetComponent<Image>().fillAmount = mana / maxMana;
    }

    int SkillIndex(string name)
    {
        for (int i = 0; i < skillObjs.Length; i++)
        {

            if (skillObjs[i].name == name)
            {
                return i;
            }
        }
        return -1;
    }

    int TotalSelectedSkills()
    {
        int total = GameManager.playerNow.selectedSkills.Length;
        for (int i = 0; i < total; i++)
        {

            if (GameManager.playerNow.selectedSkills[i] == null)
            {
                return i;
            }

        }
        return total;
    }

    private bool IsChildCollider(Collider2D collider)
    {
        // Loop melalui semua collider child
        foreach (Collider childCollider in GetComponentsInChildren<Collider>())
        {
            // Periksa apakah collider yang diberikan adalah collider child
            if (childCollider == collider)
            {
                return true;
            }
        }
        return false;
    }
}
