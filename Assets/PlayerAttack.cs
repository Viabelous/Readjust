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
        for (int i = 0; i < TotalSelectedSkills(); i++)
        {
            string name = GameManager.playerNow.selectedSkills[i].name;
            GameManager.playerNow.selectedSkills[i].skillObject = skillObjects[SkillIndexInSkillObjects(name)];
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

    int TotalSelectedSkills()
    {
        for (int i = 0; i < GameManager.playerNow.selectedSkills.Length; i++)
        {

            if (GameManager.playerNow.selectedSkills[i] == null)
            {
                return i;
            }
            else
            {
                Debug.Log(GameManager.playerNow.selectedSkills[i].name);

            }
        }
        return 7;
    }

}
