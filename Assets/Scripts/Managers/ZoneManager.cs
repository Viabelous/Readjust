using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum ZoneState
{
    Idle,
    OnDialog
}

// digunakan dalam stage 
public class ZoneManager : MonoBehaviour
{

    public static ZoneManager instance;

    [Header("Player")]
    public GameObject player;

    [Header("Developer Zone")]
    // batasan map --------------------------
    public Vector2 minMap;
    public Vector2 maxMap;

    public GameObject dialogPanel;

    private ZoneState state;


    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        Time.timeScale = 1f;
    }

    void Update()
    {
        switch (state)
        {
            case ZoneState.Idle:
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    SceneManager.LoadScene("MainMenu");
                }

                // else if (Input.GetKeyDown(KeyCode.Space))
                // {
                //     SaveData.SavePlayer(GameManager.player);
                // }

                if (dialogPanel.activeInHierarchy)
                {
                    ChangeCurrentState(ZoneState.OnDialog);
                }
                break;

            case ZoneState.OnDialog:
                if (!dialogPanel.activeInHierarchy)
                {
                    ChangeCurrentState(ZoneState.Idle);
                }
                break;
        }
    }

    public ZoneState CurrentState()
    {
        return state;
    }

    public void ChangeCurrentState(ZoneState state)
    {
        this.state = state;
    }

}