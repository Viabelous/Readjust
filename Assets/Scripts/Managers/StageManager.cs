using System;
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
    [HideInInspector] public Score score;
    private float extraScore, extraAerus, extraExp;


    // pause ------------------------------
    [HideInInspector] private StageState state;
    [SerializeField] private GameObject[] popUps;
    private NotifPopUp popUp;
    [SerializeField] private GameObject blackScreen, suddenDeath;

    // sound
    [SerializeField] private AudioSource pauseAudio, winAudio, loseAudio, basicAttAudio;


    [HideInInspector] public bool onPopUp;
    private bool onFinal, hasSavedReward;


    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        time = 0;
        extraScore = 0;
        extraAerus = 0;
        extraExp = 0;
        state = StageState.Play;
        blackScreen.SetActive(false);
        suddenDeath.SetActive(false);

        onFinal = false;
        hasSavedReward = false;
        onPopUp = false;
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

        if (time > 14 * 60)
        {
            suddenDeath.SetActive(true);

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

        blackScreen.SetActive(false);

        switch (Input.inputString)
        {
            case "q":
                player.GetComponent<Animator>().SetTrigger("BasicAttack");
                if (!GameObject.Find(basicStab.name + "(Clone)"))
                {
                    // basicAttAudio.Play();
                    Instantiate(basicStab);
                }
                break;
            case " ":
                // pauseAudio.Play();
                ToggleState(StageState.Pause, StageState.Play);
                break;
        }
    }

    private void Pause()
    {
        PauseTime();
        blackScreen.SetActive(true);

        if (popUp == null)
        {
            CreatePopUp(
                "pause",
                PopUpType.OKCancel,
                "Apakah Anda ingin meninggalkan stage dan kembali ke Developer Zone? (Perhatian: Hadiah yang telah terkumpul tidak akan tersimpan.)"
            );
        }

        switch (Input.inputString)
        {
            case " ":
                // pauseAudio.Play();
                ToggleState(StageState.Pause, StageState.Play);
                Destroy(popUp.gameObject);
                break;
        }

        if (popUp.id == "pause")
        {
            if (popUp.GetComponent<NotifPopUp>().GetClickedBtn() == PopUpBtnType.OK)
            {
                Destroy(popUp.gameObject);
                SceneManager.LoadScene("DeveloperZone");
            }
            else if (popUp.GetComponent<NotifPopUp>().GetClickedBtn() == PopUpBtnType.Cancel)
            {
                ToggleState(StageState.Pause, StageState.Play);
                Destroy(popUp.gameObject);
            }
        }

    }

    private void Lose()
    {

        PauseTime();

        if (!rewardShowed)
        {
            ShowReward(false);
            blackScreen.SetActive(true);
            rewardShowed = true;
        }
    }

    private void Win()
    {
        PauseTime();
        if (!rewardShowed)
        {
            ShowReward(true);
            blackScreen.SetActive(true);
            rewardShowed = true;
        }
    }

    private void ShowReward(bool status)
    {
        // if (status == true)
        // {
        //     winAudio.Play();
        // }
        // else
        // {
        //     loseAudio.Play();
        // }

        GameObject reward = Instantiate(rewardPanel, GameObject.Find("UI").transform);

        PlayerController playerController = player.GetComponent<PlayerController>();
        RewardPanel rewardPanelBehav = reward.GetComponent<RewardPanel>();

        rewardPanelBehav.SetType(state);


        // finalisasi reward
        score = GetScore(player.GetComponent<PlayerController>().player, status);

        rewardPanelBehav.SetScore(score.GetScore());
        rewardPanelBehav.SetEndTime(timeText.text);

        rewardPanelBehav.SetEndAerus(Mathf.FloorToInt(playerController.player.aerus), Mathf.FloorToInt(extraAerus));
        rewardPanelBehav.SetEndExp(Mathf.FloorToInt(playerController.player.exp), Mathf.FloorToInt(extraExp));

        if (!hasSavedReward)
        {
            if (status == true)
            {
                GameManager.player.IncreaseProgress(Player.Progress.Story, 1);
            }

            SaveReward(playerController.player, score);
            hasSavedReward = true;
        }
    }

    private Score GetScore(Player player, bool isWin)
    {

        float timeScore = 0;

        if (time > 600)
        {
            timeScore = 840 - time;
            timeScore = (timeScore <= 0) ? 0 : timeScore * 50;
        }

        float scoreResult = player.aerus + player.exp + timeScore;

        Score score = new Score(
            DateTime.Now,
            GameManager.selectedMap,
            (int)scoreResult,
            (int)time,
            (int)player.aerus,
            (int)player.exp,
            (int)player.venetia,
            isWin
        );

        // cek apakah ada item reward
        List<Item> rewardItem = GameManager.selectedItems.FindAll(item => item is RewardMultiplier);

        if (rewardItem.Count > 0)
        {
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

        score.IncreaseScore((int)extraScore);
        score.IncreaseAerus((int)extraAerus);
        score.IncreaseExp((int)extraExp);

        return score;
    }

    // private void FinalizeReward()
    // {
    // }

    private void SaveReward(Player player, Score score)
    {
        GameManager.player.Collect(RewardType.Aerus, Mathf.FloorToInt(player.aerus + extraAerus));
        GameManager.player.Collect(RewardType.ExpOrb, Mathf.FloorToInt(player.exp + extraExp));
        GameManager.SaveHistory(score);
    }

    public void CreatePopUp(string id, PopUpType type, string info)
    {
        GameObject newpopUp = Instantiate(
            popUps[type == PopUpType.OK ? 0 : 1],
            GameObject.Find("UI").transform
        );

        popUp = newpopUp.GetComponent<NotifPopUp>();
        popUp.id = id;
        popUp.info = info;
    }

    void OnDestroy()
    {
        if (GameManager.player.GetProgress(Player.Progress.Story) == 0)
        {
            GameManager.player.IncreaseProgress(Player.Progress.Story, 1);
        }

    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        switch (scene.name)
        {
            case "Stage1":
                GameManager.selectedMap = Map.Stage1;
                break;
            case "Stage2":
                GameManager.selectedMap = Map.Stage2;
                break;
            case "Stage3":
                GameManager.selectedMap = Map.Stage3;
                break;
            case "Stage4":
                GameManager.selectedMap = Map.Stage4;
                break;
            case "Stage5":
                GameManager.selectedMap = Map.Stage5;
                break;
        }
    }
}
