using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerAttack : MonoBehaviour
{

    public Animator animate;
    // Update is called once per frame

    public GameObject ObjectToSpawn;

    public GameObject[] skillObjects;


    void Start()
    {
        // hubungkan gameobject skill dengan kelasnya
        for (int i = 0; i < GameManager.playerNow.usedSkills.Length; i++)
        {
            string name = GameManager.playerNow.usedSkills[i].name;
            GameManager.playerNow.usedSkills[i].skillObject = skillObjects[SkillIndexInSkillObjects(name)];
        }
    }

    void Update()
    {

        switch (Input.inputString)
        {
            case "p":
                animate.SetTrigger("BasicAttack");
                GameObject Spawn = Instantiate(ObjectToSpawn, GameObject.FindWithTag("Player").transform);
                Destroy(Spawn, 1);
                break;

            case "1":

                GameManager.playerNow.usedSkills[0].Attack();
                break;

            case "2":
                GameManager.playerNow.usedSkills[1].Attack();
                break;
            case "3":
                GameManager.playerNow.usedSkills[2].Attack();
                break;

            case "=":
                SceneManager.LoadScene("MainMenu");
                break;

        }


        //animate.ResetTrigger("BasicAttack");
    }

    int SkillIndexInSkillObjects(string name)
    {
        for (int i = 0; i < skillObjects.Length; i++)
        {

            if (skillObjects[i].name == name)
            {
                return i;
            }
        }
        return -1;
    }


}
