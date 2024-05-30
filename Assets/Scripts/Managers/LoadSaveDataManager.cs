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
        // GameManager.ResetData();
        LoadGameData();
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
            // GameManager.player.LoadData(DataManager.LoadPlayer());
            GameManager.player.JsonToPlayer(DataManager.LoadPlayer());
            GameManager.scores = Score.JsonToScores(DataManager.LoadScores());
            GameManager.unlockedSkills = DataManager.LoadSkills();
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
        DataManager.SaveSkills(GameManager.unlockedSkills);
        DataManager.SaveScores(Score.ScoresToJson());
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
        // Pastikan ini hanya dijalankan saat berhenti play mode di editor
        if (!Application.isPlaying)
        {
            return;
        }

        SaveData();
    }
}