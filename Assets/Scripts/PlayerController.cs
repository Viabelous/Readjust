using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    // movement ------------------------------------------------------
    public float speed = 5f;

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
        switch (Input.inputString)
        {
            case "p":
                animate.SetTrigger("BasicAttack");
                GameObject Spawn = Instantiate(ObjectToSpawn, GameObject.FindWithTag("Player").transform);
                Destroy(Spawn, 1);
                break;

            case "1":

                GameManager.playerNow.selectedSkills[0].Attack();
                break;

            case "2":
                GameManager.playerNow.selectedSkills[1].Attack();
                break;

            case "3":
                GameManager.playerNow.selectedSkills[2].Attack();
                break;

            case "4":
                GameManager.playerNow.selectedSkills[3].Attack();
                break;

            case "=":
                SceneManager.LoadScene("MainMenu");
                break;

        }
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
}
