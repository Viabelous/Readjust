using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerAttack : MonoBehaviour
{

    public Animator animate;
    // Update is called once per frame

    public GameObject ObjectToSpawn;

    public GameObject[] skills;

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
                skills[0].GetComponent<IgniteSkill>().Active();
                break;

            case "2":
                skills[1].GetComponent<WaterwallSkill>().Active();
                break;
            case "3":
                skills[2].GetComponent<HighTideSkill>().Active();
                break;

            case "=":
                SceneManager.LoadScene("MainMenu");
                break;

        }


        //animate.ResetTrigger("BasicAttack");
    }
}
