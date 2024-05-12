using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum StageState
{
    Play, Pause, Win, Lose, Reward
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
    public GameObject rewardPanel;

    [HideInInspector] public float time;

    private int min, sec;

    [HideInInspector] public float minTime;

    [HideInInspector] private StageState state;
    [SerializeField] private GameObject stateText;

    [HideInInspector] public bool validSkill;


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
        switch (state)
        {
            case StageState.Play:
                Play();
                break;
            case StageState.Pause:
                Pause();
                break;
            case StageState.Win:
                Win();
                break;
            case StageState.Lose:
                Lose();
                break;
            case StageState.Reward:
                Reward();
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

        switch (Input.inputString)
        {
            case "q":
                player.GetComponent<Animator>().SetTrigger("BasicAttack");
                if (!GameObject.Find(basicStab.name + "(Clone)"))
                {
                    Instantiate(basicStab);
                }
                break;
            case " ":
                ToggleState(StageState.Pause, StageState.Play);
                break;
        }
    }

    private void Pause()
    {
        state = StageState.Pause;
        PauseTime();

        stateText.SetActive(true);
        stateText.GetComponent<Text>().text = "PAUSE";


        switch (Input.inputString)
        {
            case " ":
                ToggleState(StageState.Pause, StageState.Play);
                break;
        }
    }

    private void Lose()
    {
        state = StageState.Lose;
        PauseTime();

        stateText.SetActive(true);
        stateText.GetComponent<Text>().text = "LOSE";

        switch (Input.inputString)
        {
            case " ":
                Reward();
                break;
        }
    }

    private void Win()
    {
        state = StageState.Win;
        PauseTime();
        stateText.SetActive(true);
        stateText.GetComponent<Text>().text = "WIN";

        switch (Input.inputString)
        {
            case " ":
                Reward();
                break;
        }
    }

    private void Reward()
    {
        state = StageState.Reward;

        rewardPanel.SetActive(true);
        RewardPanel rewardPanelBehav = rewardPanel.GetComponent<RewardPanel>();
        rewardPanelBehav.SetScore(10000);
        rewardPanelBehav.SetEndTime(timeText.text);

        PlayerController playerController = player.GetComponent<PlayerController>();

        rewardPanelBehav.SetEndAerus(playerController.player.aerus, 0);
        rewardPanelBehav.SetEndExp(playerController.player.exp, 0);

        switch (Input.inputString)
        {
            case " ":
                SceneManager.LoadScene("DeveloperZone");
                break;
        }
    }

}

