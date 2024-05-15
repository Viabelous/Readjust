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

    // timer -------------------------------
    public Text timeText;
    [HideInInspector] public float time;
    private int min, sec;
    [HideInInspector] public float minTime;

    // batasan map --------------------------
    public Vector2 minMap, maxMap;

    // panel reward --------------------------
    public GameObject rewardPanel;
    [HideInInspector] public float score;
    private float extraScore, extraAerus, extraExp;

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
        score = 0;
        extraScore = 0;
        extraAerus = 0;
        extraExp = 0;
        state = StageState.Play;
        rewardPanel.SetActive(false);
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

        PlayerController playerController = player.GetComponent<PlayerController>();

        rewardPanel.SetActive(true);
        RewardPanel rewardPanelBehav = rewardPanel.GetComponent<RewardPanel>();

        // finalisasi reward
        FinalizeReward();

        rewardPanelBehav.SetScore(Mathf.FloorToInt(score + extraScore));
        rewardPanelBehav.SetEndTime(timeText.text);

        rewardPanelBehav.SetEndAerus(Mathf.FloorToInt(playerController.player.aerus), Mathf.FloorToInt(extraAerus));
        rewardPanelBehav.SetEndExp(Mathf.FloorToInt(playerController.player.exp), Mathf.FloorToInt(extraExp));

        playerController.player.Collect(RewardType.Aerus, Mathf.FloorToInt(extraAerus));
        playerController.player.Collect(RewardType.ExpOrb, Mathf.FloorToInt(extraExp));

        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("DeveloperZone");
        }
    }

    private float GetScore(Player player)
    {
        float timeScore = 0;

        if (time > 600)
        {
            timeScore = 840 - time;
            timeScore = (timeScore <= 0) ? 0 : timeScore * 50;
        }

        score = player.aerus + player.exp + timeScore;
        return score;
    }

    private void FinalizeReward()
    {

        score = GetScore(player.GetComponent<PlayerController>().player);

        // !!!!!!!!!!!!!!!!!!!!!!!!
        // !!!!NANTI UBAH WOIII!!!!
        // !!!!!!!!!!!!!!!!!!!!!!!!
        List<Item> rewardItem = GameManager.selectedItems.FindAll(item => item is MultiplyReward);
        // List<Item> rewardItem = CumaBuatDebug.instance.selectedItems.FindAll(item => item is MultiplyReward);

        if (rewardItem.Count == 0)
        {
            return;
        }

        foreach (Item item in rewardItem)
        {
            item.Activate(this.player);

            MultiplyReward itemReward = (MultiplyReward)item;
            switch (itemReward.rewardType)
            {
                case RewardType.Aerus:
                    extraAerus += itemReward.result;
                    break;
                case RewardType.ExpOrb:
                    extraExp += itemReward.result;
                    break;
                case RewardType.Score:
                    extraScore += itemReward.result;
                    break;
            }
        }
    }

}
