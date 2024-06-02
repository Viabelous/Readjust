using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// digunakan dalam stage 
public class LoadSaveDataManager : MonoBehaviour
{
    public static LoadSaveDataManager instance;
    [SerializeField] private Player playerBasic;

    void Awake()
    {
        instance = this;
    }

    public void CreateNewData()
    {
        GameManager.ResetData();
        GameManager.player = playerBasic.Clone();
        GameManager.unlockedSkills = new Dictionary<string, int>();
    }

    public void LoadGameData()
    {
        if (DataManager.CheckPath())
        {
            GameManager.player = playerBasic.Clone();

            GameManager.player.JsonToPlayer(DataManager.LoadPlayer());
            GameManager.scores = Score.JsonToScores(DataManager.LoadScores());
            GameManager.unlockedSkills = DataManager.LoadSkills();
            GameManager.unlockedItems = DataManager.LoadItems();
            GameManager.firstEncounter = DataManager.LoadNPCData();
            print("Data loaded");
        }
        else
        {
            CreateNewData();
        }
    }

    private void SaveData()
    {
        DataManager.SavePlayer(GameManager.player);
        DataManager.SaveScores(Score.ScoresToJson(GameManager.scores));
        DataManager.SaveSkills(GameManager.unlockedSkills);
        DataManager.SaveItems(GameManager.unlockedItems);
        DataManager.SaveNPCData(GameManager.firstEncounter);

        Debug.Log("Data saved.");
    }

    // // saat aplikasi ditutup (setelah selesai di-build)
    // void OnApplicationQuit()
    // {
    //     SaveData();

    // }

    // saat gameobject dihancurkan (saat run di unity editor dimatikan)
    void OnDestroy()
    {
        SaveData();
    }
}