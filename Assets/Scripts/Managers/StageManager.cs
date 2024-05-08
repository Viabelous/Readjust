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

    public PlayerController playerController;
    public Text timeText;


    [HideInInspector] public float time;

    private int min, sec;

    [HideInInspector] public float minTime;

    [HideInInspector] public GameState gameState;
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
                // PauseGame();
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

    public void PlayerActivatesSkill(Skill skill)
    {
        playerController.Pay(skill.CostType, skill.Cost);
        int index = GameManager.selectedSkills.FindIndex(skillId => skill.Id == skillId);
        // ubah state slot skill
        GameObject.Find("slot_" + (index + 1)).GetComponent<SkillSlot>().ChangeState(SkillState.Active);
    }

    // public void PlayerCancelSkill(GameObject)
    // {
    //     skill.Cancel();
    // }

    // public void CancelSkill(GameObject skillPref)
    // {
    //     Destroy(skillPref);
    //     validSkill = false;

    //     // Skill skill = skillPref.GetComponent<SkillController>().skill;

    //     // SkillSlot[] slots = FindObjectsOfType<SkillSlot>();
    //     // foreach (SkillSlot slot in slots)
    //     // {
    //     //     if (slot.skill.name == skill.Name)
    //     //     {
    //     //         slot.state = SkillState.Ready;
    //     //         player.GetComponent<PlayerController>().player.mana += skill.Cost;
    //     //         break;
    //     //     }
    //     // }
    // }


}

