using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;

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
    private bool rewardShowed = false;
    [HideInInspector] public float score;
    private float extraScore, extraAerus, extraExp;

    // pause ------------------------------
    [HideInInspector] private StageState state;
    [SerializeField] private GameObject stateText, blackScreen;


    [HideInInspector] public bool validSkill;
    private bool onFinal, hasSavedReward;


    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        time = 0;
        score = 0;
        extraScore = 0;
        extraAerus = 0;
        extraExp = 0;
        state = StageState.Play;
        stateText.SetActive(false);
        blackScreen.SetActive(false);

        onFinal = false;
        hasSavedReward = false;
        // rewardPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        switch (state)
        {
            case StageState.Play:
                Play();

                if (min >= 10 && !onFinal)
                {
                    onFinal = true;

                    List<EnemySpawner> spawners = GameObject.FindObjectsOfType<EnemySpawner>().ToList();
                    Vector3 spawnPos = spawners[UnityEngine.Random.Range(0, spawners.Count)].transform.position;
                    Instantiate(SpawnHolder.instance.bossPref, spawnPos, Quaternion.identity);
                }

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

    public void ChangeCurrentState(StageState state)
    {
        this.state = state;
    }

    private void ActivateTimer()
    {
        time += Time.deltaTime;

        min = Mathf.FloorToInt(time / 60f);
        sec = Mathf.FloorToInt(time % 60f);

        timeText.text = string.Format("{0:00}:{1:00}", min, sec);
    }

    public void ResumeTime()
    {
        Time.timeScale = 1f;
    }

    public void PauseTime()
    {
        Time.timeScale = 0f;
    }

    private void Play()
    {
        ChangeCurrentState(StageState.Play);
        ResumeTime();
        ActivateTimer();

        stateText.SetActive(false);
        blackScreen.SetActive(false);


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
        PauseTime();

        stateText.SetActive(true);
        stateText.GetComponent<Text>().text = "PAUSE";
        blackScreen.SetActive(true);

        switch (Input.inputString)
        {
            case " ":
                ToggleState(StageState.Pause, StageState.Play);
                break;
            case "\b":
                SceneManager.LoadScene("DeveloperZone");
                break;
        }
    }

    private void Lose()
    {

        PauseTime();

        if (!rewardShowed)
        {
            ShowReward();
            blackScreen.SetActive(true);
            rewardShowed = true;
        }
    }

    private void Win()
    {
        PauseTime();
        if (!rewardShowed)
        {
            ShowReward();
            blackScreen.SetActive(true);
            rewardShowed = true;
        }
    }

    private void ShowReward()
    {
        GameObject reward = Instantiate(rewardPanel, GameObject.Find("UI").transform);

        PlayerController playerController = player.GetComponent<PlayerController>();
        RewardPanel rewardPanelBehav = reward.GetComponent<RewardPanel>();

        rewardPanelBehav.SetType(state);

        // finalisasi reward
        FinalizeReward();
        score = GetScore(player.GetComponent<PlayerController>().player);

        rewardPanelBehav.SetScore(Mathf.FloorToInt(score + extraScore));
        rewardPanelBehav.SetEndTime(timeText.text);

        rewardPanelBehav.SetEndAerus(Mathf.FloorToInt(playerController.player.aerus), Mathf.FloorToInt(extraAerus));
        rewardPanelBehav.SetEndExp(Mathf.FloorToInt(playerController.player.exp), Mathf.FloorToInt(extraExp));

        if (!hasSavedReward)
        {
            SaveReward(playerController.player);
            hasSavedReward = true;
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

        score = player.aerus + extraAerus + player.exp + extraExp + timeScore;
        return score;
    }

    private void FinalizeReward()
    {
        // !!!!!!!!!!!!!!!!!!!!!!!!
        // !!!!NANTI UBAH WOIII!!!!
        // !!!!!!!!!!!!!!!!!!!!!!!!
        // List<Item> rewardItem = GameManager.selectedItems.FindAll(item => item is MultiplyReward);
        List<Item> rewardItem = CumaBuatDebug.instance.selectedItems.FindAll(item => item is RewardMultiplier);

        if (rewardItem.Count == 0)
        {
            return;
        }

        foreach (Item item in rewardItem)
        {
            item.Activate(this.player);

            RewardMultiplier itemReward = (RewardMultiplier)item;
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

    private void SaveReward(Player player)
    {
        GameManager.player.Collect(RewardType.Aerus, Mathf.FloorToInt(player.aerus + extraAerus));
        GameManager.player.Collect(RewardType.ExpOrb, Mathf.FloorToInt(player.exp + extraExp));

        DataManager.SavePlayer(GameManager.player);
        GameManager.player.LoadData(DataManager.LoadPlayer());
    }
}
