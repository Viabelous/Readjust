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

    public SpriteRenderer[] spriteRenderers;
    public Rigidbody2D rb;

    public Animator animate;

    Vector2 movement;

    public string direction;

    public bool canMove = true;

    // attack -------------------------------------------------
    public GameObject ObjectToSpawn;

    // public GameObject[] skillObjs;
    public List<GameObject> skillPrefs = new List<GameObject>();

    void Start()
    {
        direction = "front";
        ObjectToSpawn = GameObject.Find("basic_stab");

        hp = GameManager.playerNow.maxHp;
        mana = GameManager.playerNow.maxMana;
        shield = GameManager.playerNow.maxShield;

        // // hubungkan gameobject skill dengan object skill
        // for (int i = 0; i < TotalSelectedSkills(); i++)
        // {
        //     string name = GameManager.playerNow.selectedSkills[i].name;
        //     GameManager.playerNow.selectedSkills[i].skillObj = skillObjs[SkillIndex(name)];
        // }

        // Object[] loadedAssets = AssetDatabase.LoadAllAssetsAtPath("Assets/Prefabs/Skills/");
        // for (int i = 0; i < GameManager.playerNow.selectedSkills.Count; i++)
        // {
        //     string name = GameManager.playerNow.selectedSkills[i].name;
        //     GameObject prefab = System.Array.Find(loadedAssets, obj => obj is GameObject && obj.name == name) as GameObject;
        //     skillPrefs[i] = prefab;
        // }

        // skillPrefs = System.Array.FindAll(loadedAssets, obj => obj is GameObject && obj.name == "") as GameObject[];

    }

    // Update is called once per frame
    void Update()
    {
        if (!canMove)
        {
            return;
        }
        // print(skillPrefs.Count);


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

    void PlayerAttack(){
        switch (Input.inputString){
            case "1" :
            case "2" :
            case "3" :
            case "4" :
            case "5" :
            case "6" :
            case "7" :
                if (mana > 0){
                    int index = int.Parse(Input.inputString) - 1;
                    Skill skill = GameManager.playerNow.selectedSkills[index];

                    if (skill != null && !skill.isCooldown && mana >= skill.manaUsage){
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
        healtBar.GetComponent<Image>().fillAmount = hp / GameManager.playerNow.maxHp;
    }
    public void UpdateManaBar()
    {
        manaBar.GetComponent<Image>().fillAmount = mana / GameManager.playerNow.maxMana;
    }
    public void UpdateShieldBar()
    {
        shieldBar.GetComponent<Image>().fillAmount = shield / GameManager.playerNow.maxShield;
    }

    // int SkillIndex(string name)
    // {
    //     for (int i = 0; i < skillPrefs.Length; i++)
    //     {

    //         if (skillPrefs[i].name == name)
    //         {
    //             return i;
    //         }
    //     }
    //     return -1;
    // }


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
