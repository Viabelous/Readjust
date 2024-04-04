using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageMenu : MonoBehaviour
{
    public void Stage1()
    {
        Debug.Log("Buka stage 1");
        SceneManager.LoadScene("Stage1_Coba");
    }
    // public void Stage2()
    // {
    //     Debug.Log("Buka stage 2");
    //     SceneManager.LoadScene("Stage2_coba");
    // }
}
