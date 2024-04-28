using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GameState
{
    Boss, Pause, Victory, Lose
}
// digunakan dalam stage 
public class StageManager : MonoBehaviour
{
    public static StageManager instance;

    [Header("Timer")]
    public Text timeText;


    [HideInInspector] public float time;
    private int min, sec;


    [HideInInspector] public GameState gameState;

    [HideInInspector] public List<string> killedEnemies;

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;

    }

    void Start()
    {
        time = Time.time;
        killedEnemies = new List<string>();
    }

    // Update is called once per frame
    void Update()
    {
        time = Time.time;

        min = Mathf.FloorToInt(time / 60f);
        sec = Mathf.FloorToInt(time % 60f);

        timeText.text = string.Format("{0:00}:{1:00}", min, sec);

        if (min == 10)
        {
            StageManager.instance.gameState = GameState.Boss;
        }

        switch (gameState)
        {
            case GameState.Boss:
                break;
            case GameState.Pause:
                break;
            case GameState.Victory:
                break;
            case GameState.Lose:
                PauseGame();
                break;
        }
    }


    public void ChangeGameState(GameState gameState)
    {
        this.gameState = gameState;
    }

    private void PauseGame()
    {
        MobController[] mobs = FindObjectsOfType<MobController>();
        foreach (MobController mob in mobs)
        {
            mob.movementEnabled = false;
        }

        PlayerController player = FindObjectOfType<PlayerController>();
        player.movementEnabled = false;

        EnemySpawner[] spawners = FindObjectsOfType<EnemySpawner>();
        foreach (EnemySpawner spawner in spawners)
        {
            spawner.spawnEnabled = false;
        }

    }

    public void PlayerKill(string enemyId)
    {
        killedEnemies.Add(enemyId);
        // print(killedEnemies);
    }
}

