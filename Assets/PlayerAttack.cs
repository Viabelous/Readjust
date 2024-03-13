using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerAttack : MonoBehaviour
{
    
    public Animator animate;
    // Update is called once per frame

    public GameObject ObjectToSpawn;

    void Update()
    {

        switch(Input.inputString){
            case "p":
            animate.SetTrigger("BasicAttack");
            GameObject Spawn = Instantiate(ObjectToSpawn, GameObject.FindWithTag("Player").transform);
            Destroy(Spawn, 1);
            break;

            case "=":
            SceneManager.LoadScene("MainMenu");
            break;

        }

        
        //animate.ResetTrigger("BasicAttack");
    }
}
