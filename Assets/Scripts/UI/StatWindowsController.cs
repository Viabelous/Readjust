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
        // UpdateStatBtnHover();
    }

    void Update()
    {
        UpdateStatBtnHover();
        UpdateStatValue();
        SetStatDescription();
    }

    void SetStatDescription()
    {
        switch (focusedStat.type)
        {
            case Player.Progress.MaxHP:
                statAerusCost.text = GameManager.player.GetAerusUpCost(Player.Progress.MaxHP).ToString();
                statAerusCost.text = GameManager.player.GetExpUpCost(Player.Progress.MaxHP).ToString();
                statName.text = "Max Health Point (HP)";
                statDescription.text = "Jumlah maksimal darah yang bisa didapatkan pada saat stage berlangsung stage.";
                break;
            case Player.Progress.MaxMana:
                statAerusCost.text = GameManager.player.GetAerusUpCost(Player.Progress.MaxMana).ToString();
                statAerusCost.text = GameManager.player.GetExpUpCost(Player.Progress.MaxMana).ToString();
                statName.text = "Max Mana Point (MP)";
                statDescription.text = "Jumlah maksimal mana yang bisa digunakan pada saat stage berlangsung stage.";
                break;
            case Player.Progress.ATK:
                statAerusCost.text = GameManager.player.GetAerusUpCost(Player.Progress.ATK).ToString();
                statAerusCost.text = GameManager.player.GetExpUpCost(Player.Progress.ATK).ToString();
                statName.text = "Attack (ATK)";
                statDescription.text = "Kerusakan yang akan dikeluarkan oleh player";
                break;
            case Player.Progress.DEF:
                statAerusCost.text = GameManager.player.GetAerusUpCost(Player.Progress.DEF).ToString();
                statAerusCost.text = GameManager.player.GetExpUpCost(Player.Progress.DEF).ToString();
                statName.text = "Defense (DEF)";
                statDescription.text = "Sebagai tameng player.";
                break;
            case Player.Progress.FOC:
                statAerusCost.text = GameManager.player.GetAerusUpCost(Player.Progress.FOC).ToString();
                statAerusCost.text = GameManager.player.GetExpUpCost(Player.Progress.FOC).ToString();
                statName.text = "... (FOC)";
                statDescription.text = "Kemungkinan dikeluarkannya kerusakan yang tinggi pada saat menyerang.";
                break;
            case Player.Progress.AGI:
                statAerusCost.text = GameManager.player.GetAerusUpCost(Player.Progress.AGI).ToString();
                statAerusCost.text = GameManager.player.GetExpUpCost(Player.Progress.AGI).ToString();
                statName.text = "Agility (AGI)";
                statDescription.text = "Penambahan kecepatan pergerakan player.";
                break;
        }
    }

    void UpdateStatBtnHover()
    {
        if (WindowsController.FocusedButton == null)
        {
            focusedStat = WindowsController.HoveredButton.GetComponent<StatSelection>();
            print(WindowsController.HoveredButton.GetComponent<StatSelection>() == null);
            print(focusedStat == null);

        }
        else
        {
            focusedStat = WindowsController.FocusedButton.GetComponent<StatSelection>();
        }
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