using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// digunakan dalam stage 
public class StageManager : MonoBehaviour
{
    public static StageManager Instance;

    public float time = 0, aerus = 0, exp = 0;
    private Text aerusText, expText;
    public GameState gameState;

    // Start is called before the first frame update

    void Start()
    {
        Instance = this;
        aerusText = GameObject.Find("aerus_text").GetComponent<Text>();
        expText = GameObject.Find("exp_orb_text").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {

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

    public void CollectAerus(float num)
    {
        aerus += num;
        aerusText.text = aerus.ToString();
    }

    public void CollectExp(float num)
    {
        exp += num;
        expText.text = exp.ToString();
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

        EnemySpawner[] spawners = FindObjectsOfType<EnemySpawner>();
        foreach (EnemySpawner spawner in spawners)
        {
            spawner.spawnEnabled = false;
        }

    }
}

public enum GameState
{
    Boss,
    Pause,
    Victory,
    Lose
}
