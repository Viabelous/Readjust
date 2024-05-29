using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class StatWindowsController : MonoBehaviour
{

    [SerializeField] private windowsController WindowsController;
    [SerializeField] private GameObject upgradeBtn;
    [SerializeField] private Text statName, statDescription, statAerusCost, statExpCost;
    [SerializeField] private Text[] statsText = new Text[6];
    StatSelection focusedStat;

    void Start()
    {
        UpdateStatBtnHover();
        UpdateStatValue();
        SetStatDescription();
    }

    void Update()
    {
        if (WindowsController.HoveredButton.GetComponent<StatSelection>() != null)
        {
            UpdateStatBtnHover();
            SetStatDescription();
            UpdateStatValue();
        }
    }

    void SetStatDescription()
    {
        int level = GameManager.player.GetProgress(focusedStat.type);

        if (GameManager.player.CanBeUpgraded(focusedStat.type))
        {
            statAerusCost.text = GameManager.player.GetAerusUpCost(focusedStat.type).ToString();
            statExpCost.text = GameManager.player.GetExpUpCost(focusedStat.type).ToString();

        }
        else
        {
            statAerusCost.text = "-";
            statExpCost.text = "-";
        }


        switch (focusedStat.type)
        {
            case Player.Progress.MaxHP:
                statName.text = "Max Health Point (HP) Lv. " + level;
                statDescription.text = "Jumlah maksimal darah yang bisa didapatkan pada saat stage berlangsung stage.";
                break;
            case Player.Progress.MaxMana:
                statName.text = "Max Mana Point (MP) Lv. " + level;
                statDescription.text = "Jumlah maksimal mana yang bisa digunakan pada saat stage berlangsung stage.";
                break;
            case Player.Progress.ATK:
                statName.text = "Attack (ATK) Lv. " + level;
                statDescription.text = "Kerusakan yang akan dikeluarkan oleh player";
                break;
            case Player.Progress.DEF:
                statName.text = "Defense (DEF) Lv. " + level;
                statDescription.text = "Sebagai tameng player.";
                break;
            case Player.Progress.FOC:
                statName.text = "Focus (FOC) Lv. " + level;
                statDescription.text = "Kemungkinan dikeluarkannya kerusakan yang tinggi pada saat menyerang.";
                break;
            case Player.Progress.AGI:
                statName.text = "Agility (AGI) Lv. " + level;
                statDescription.text = "Penambahan kecepatan pergerakan player.";
                break;
        }
    }

    void UpdateStatBtnHover()
    {
        focusedStat = WindowsController.HoveredButton.GetComponent<StatSelection>();
    }

    public void UpdateStatValue()
    {
        statsText[0].text = GameManager.player.GetMaxHP().ToString();
        statsText[1].text = GameManager.player.GetMaxMana().ToString();
        statsText[2].text = GameManager.player.GetATK().ToString();
        statsText[3].text = GameManager.player.GetDEF().ToString();
        statsText[4].text = GameManager.player.GetFOC().ToString();
        statsText[5].text = GameManager.player.GetAGI().ToString();
    }

}