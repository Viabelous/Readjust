using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum StageState
{
    Play, Pause, Victory, Lose
}

// digunakan dalam stage 
public class StageManager : MonoBehaviour
{
    public static StageManager instance;

    [Header("Player")]
    public GameObject player;
    [SerializeField] private GameObject basicStab;

    [Header("Stage")]

    public Text timeText;

    public Vector2 minMap, maxMap;

    [HideInInspector] public float time;

    private int min, sec;

    [HideInInspector] public float minTime;

    [HideInInspector] private StageState state;
    [SerializeField] private GameObject stateText;

    [HideInInspector] public bool validSkill;

    // [HideInInspector] public List<string> killedEnemies;

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        time = Time.time;
        state = StageState.Play;
    }

    // Update is called once per frame
    void Update()
    {
        KeyboardInteraction();



        // if (min == 10)
        // {
        //     StageManager.instance.gameState = GameState.Boss;
        // }

        switch (state)
        {
            case StageState.Play:
                Play();
                break;
            case StageState.Pause:
                Pause();
                break;
            case StageState.Victory:
                break;
            case StageState.Lose:
                Lose();
                break;
        }
    }

    public void ToggleState(StageState initState, StageState currState)
    {
        this.state = state == initState ? currState : initState;
    }

    public StageState CurrentState()
    {
        return this.state;
    }

    private void KeyboardInteraction()
    {
        switch (Input.inputString)
        {
            case "q":

                if (state == StageState.Play)
                {
                    player.GetComponent<Animator>().SetTrigger("BasicAttack");
                    if (!GameObject.Find(basicStab.name + "(Clone)"))
                    {
                        Instantiate(basicStab);
                    }
                }
                break;


            case "\b":
                SceneManager.LoadScene("MainMenu");
                break;

            case " ":
                ToggleState(StageState.Pause, StageState.Play);
                break;
        }
    }

    private void RunTime()
    {
        Time.timeScale = 1f;
        time = Time.time;

        min = Mathf.FloorToInt(time / 60f);
        sec = Mathf.FloorToInt(time % 60f);

        timeText.text = string.Format("{0:00}:{1:00}", min, sec);
    }

    private void PauseTime()
    {
        Time.timeScale = 0f;
    }

    private void Play()
    {
        state = StageState.Play;
        RunTime();

        stateText.SetActive(false);
    }

    private void Pause()
    {
        state = StageState.Pause;
        PauseTime();

        stateText.SetActive(true);
        stateText.GetComponent<Text>().text = "PAUSE";

        // MobController[] mobs = FindObjectsOfType<MobController>();
        // foreach (MobController mob in mobs)
        // {
        //     mob.movementEnabled = false;
        // }

        // PlayerController player = FindObjectOfType<PlayerController>();
        // player.movementEnabled = false;

        // EnemySpawner[] spawners = FindObjectsOfType<EnemySpawner>();
        // foreach (EnemySpawner spawner in spawners)
        // {
        //     spawner.spawnEnabled = false;
        // }

    }

    private void Lose()
    {
        state = StageState.Lose;
        PauseTime();
        stateText.SetActive(true);
        stateText.GetComponent<Text>().text = "LOSE";
    }

}

